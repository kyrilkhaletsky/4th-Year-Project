using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum BrakeCondition {
    NeverBrake,
    TargetDirectionDifference,
    TargetDistance,
}

public class CarAIControl : MonoBehaviour {

    public GameObject[] Points = new GameObject[10];
    public Rigidbody Rigidbody;
    public static float accel;
    public static float steer;
    public double driveTime = 0;
    public float WaitTime = 3.0f;
    ResetPosition position;
    Vector3 Position;

    // **Below is a modified version of the Built in car AI provided by Unity Standard Assets**

    [SerializeField] [Range(0, 1)] private float cautiousSpeedFactor = 0.05f;// percentage of max speed to use when being maximally cautious
    [SerializeField] [Range(0, 180)] private float cautiousMaxAngle = 50f;// angle of approaching corner to treat as warranting maximum caution
    [SerializeField] private float cautiousMaxDistance = 100f;// distance at which distance-based cautiousness begins
    [SerializeField] private float cautiousAngularVelocityFactor = 30f;// how cautious the AI should be when considering its own current angular velocity (i.e. easing off acceleration if spinning!)
    [SerializeField] private float steerSensitivity = 0.05f;// how sensitively the AI uses steering input to turn to the desired direction
    [SerializeField] private float accelSensitivity = 0.04f;// How sensitively the AI uses the accelerator to reach the current desired speed
    [SerializeField] private float brakeSensitivity = 1f;// How sensitively the AI uses the brake to reach the current desired speed
    [SerializeField] private float lateralWanderDistance = 3f;// how far the car will wander laterally towards its target
    [SerializeField] private float lateralWanderSpeed = 0.1f; // how fast the lateral wandering will fluctuate
    [SerializeField] [Range(0, 1)] public float accelWanderAmount = 0.1f;// how much the cars acceleration will wander
    [SerializeField] private float accelWanderSpeed = 0.1f;// how fast the cars acceleration wandering will fluctuate
    [SerializeField] private BrakeCondition brakeCondition = BrakeCondition.TargetDistance;// what should the AI consider when accelerating/braking?
    [SerializeField] public static bool driving = false; // whether the AI is currently actively driving or stopped.
    [SerializeField] private Transform target; // 'target' the target object to aim for.	
    [SerializeField] private bool stopWhenTargetReached; // should we stop driving when we reach the target?
    [SerializeField] private float reachTargetThreshold = 2;// proximity to target to consider we 'reached' it, and stop driving.

    private float randomPerlin;// A random value for the car to base its wander on (so that AI cars don't all wander in the same pattern)
    private CarPhysics carController; // Reference to actual car controller we are controlling

    // if this AI car collides with another car, it can take evasive action for a short duration:
    private float avoidOtherCarTime; // time until which to avoid the car we recently collided with
    private float avoidOtherCarSlowdown; // how much to slow down due to colliding with another car, whilst avoiding
    private float avoidPathOffset;// direction (-1 or 1) in which to offset path to avoid other car, whilst avoiding

    private void Start() {
        Rigidbody = GetComponent<Rigidbody>();
        Rigidbody.GetComponent<CarPhysics>().driver = Driver.UnityAI;
        position = new ResetPosition();
    }

    private void Awake() {
        // get the car controller reference
        carController = GetComponent<CarPhysics>();

        // give the random perlin a random value
        randomPerlin = Random.value * 100;
    }

    public void SetTarget(Transform target) {
        this.target = target;
        driving = true;
    }

    public float Speed() {
        return Rigidbody.velocity.magnitude * 3.6f;
    }

    void Update() {
        driveTime += Time.deltaTime;

        //check if a car has stopped for more than 3 seconds (crashed car) and reset position
        if (driveTime > WaitTime) {
            if (Rigidbody.velocity.magnitude < 0.1 && driving == true) {
                ResetCurrent();
            }
            driveTime = driveTime - WaitTime;
        }
    }

    //Resets car position if the car is stuck
    public void ResetCurrent() {
        GameObject closest = position.ResetCurrentCar(Rigidbody, transform.position, Points);
        Rigidbody.velocity = Vector3.zero;
        transform.position = closest.transform.position;
        transform.rotation = closest.transform.rotation;
        Position = transform.position;
    }

    private void FixedUpdate() {
        if (target == null || !driving) {
            // Car should not be moving,
            // (so use accel/brake to get to zero speed)
            float accel = Mathf.Clamp(-carController.Speed(), -1, 1);

        } else {
            Vector3 fwd = transform.forward;
            if (Rigidbody.velocity.magnitude > carController.maxSpeed * 0.1f) {
                fwd = Rigidbody.velocity;
            }

            float desiredSpeed = carController.maxSpeed;

            // now it's time to decide if we should be slowing down...
            switch (brakeCondition) {

                case BrakeCondition.TargetDirectionDifference: {
                        // check out the angle of our target compared to the current direction of the car
                        float approachingCornerAngle = Vector3.Angle(target.forward, fwd);

                        // also consider the current amount we're turning, multiplied up and then compared in the same way as an upcoming corner angle
                        float spinningAngle = Rigidbody.angularVelocity.magnitude * cautiousAngularVelocityFactor;

                        // if it's different to our current angle, we need to be cautious (i.e. slow down) a certain amount
                        float cautiousnessRequired = Mathf.InverseLerp(0, cautiousMaxAngle,
                                                                        Mathf.Max(spinningAngle,
                                                                                    approachingCornerAngle));
                        desiredSpeed = Mathf.Lerp(carController.maxSpeed, carController.maxSpeed * cautiousSpeedFactor,
                                                    cautiousnessRequired);
                        break;
                    }
                case BrakeCondition.TargetDistance: {
                        // check out the distance to target
                        Vector3 delta = target.position - transform.position;
                        float distanceCautiousFactor = Mathf.InverseLerp(cautiousMaxDistance, 0, delta.magnitude);

                        // also consider the current amount we're turning, multiplied up and then compared in the same way as an upcoming corner angle
                        float spinningAngle = Rigidbody.angularVelocity.magnitude * cautiousAngularVelocityFactor;

                        // if it's different to our current angle, we need to be cautious (i.e. slow down) a certain amount
                        float cautiousnessRequired = Mathf.Max(
                            Mathf.InverseLerp(0, cautiousMaxAngle, spinningAngle), distanceCautiousFactor);
                        desiredSpeed = Mathf.Lerp(carController.maxSpeed, carController.maxSpeed * cautiousSpeedFactor,
                                                    cautiousnessRequired);
                        break;
                    }
                case BrakeCondition.NeverBrake:
                    break;
            }

            // our target position starts off as the 'real' target position
            Vector3 offsetTargetPos = target.position;

            // if are we currently taking evasive action to prevent being stuck against another car:
            if (Time.time < avoidOtherCarTime) {
                // slow down if necessary (if we were behind the other car when collision occured)
                desiredSpeed *= avoidOtherCarSlowdown;
                // and veer towards the side of our path-to-target that is away from the other car
                offsetTargetPos += target.right * avoidPathOffset;
            } else {
                // no need for evasive action, we can just wander across the path-to-target in a random way
                offsetTargetPos += target.right *
                                    (Mathf.PerlinNoise(Time.time * lateralWanderSpeed, randomPerlin) * 2 - 1) *
                                    lateralWanderDistance;
            }

            // use different sensitivity depending on whether accelerating or braking:
            float accelBrakeSensitivity = (desiredSpeed < Speed())
                                                ? brakeSensitivity
                                                : accelSensitivity;

            // decide the actual amount of accel/brake input to achieve desired speed.
            accel = Mathf.Clamp((desiredSpeed - Speed()) * accelBrakeSensitivity, -1, 1);

            // add acceleration 'wander', which also prevents AI from seeming too uniform and robotic in their driving
            accel *= (1 - accelWanderAmount) +
                        (Mathf.PerlinNoise(Time.time * accelWanderSpeed, randomPerlin) * accelWanderAmount);

            // calculate the local-relative position of the target, to steer towards
            Vector3 localTarget = transform.InverseTransformPoint(offsetTargetPos);

            // work out the local angle towards the target
            float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

            // get the amount of steering needed to aim the car towards the target
            steer = Mathf.Clamp(targetAngle * steerSensitivity, -1, 1) * Mathf.Sign(Speed());

            // feed input to the car controller.
            carController.Steering(steer);
            carController.Torque(accel);

            // if appropriate, stop driving when we're close enough to the target.
            if (stopWhenTargetReached && localTarget.magnitude < reachTargetThreshold) {
                driving = false;
            }
        }
    }

    private void OnCollisionStay(Collision col) {
        // detect collision against other cars, so that we can take evasive action
        if (col.rigidbody != null) {
            var otherAI = col.rigidbody.GetComponent<CarAIControl>();
            if (otherAI != null) {
                // we'll take evasive action for 1 second
                avoidOtherCarTime = Time.time + 1;

                // but who's in front?...
                if (Vector3.Angle(transform.forward, otherAI.transform.position - transform.position) < 90) {
                    // the other ai is in front, so it is only good manners that we ought to brake...
                    avoidOtherCarSlowdown = 0.5f;
                } else {
                    // we're in front! ain't slowing down for anybody...
                    avoidOtherCarSlowdown = 1;
                }

                // both cars should take evasive action by driving along an offset from the path centre, away from car
                var otherCarLocalDelta = transform.InverseTransformPoint(otherAI.transform.position);
                float otherCarAngle = Mathf.Atan2(otherCarLocalDelta.x, otherCarLocalDelta.z);
                avoidPathOffset = lateralWanderDistance * -Mathf.Sign(otherCarAngle);
            }
        }
    }
}
