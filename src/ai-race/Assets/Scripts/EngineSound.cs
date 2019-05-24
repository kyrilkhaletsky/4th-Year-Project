using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSound : MonoBehaviour {
    
    public float[] MinRpmTable = {500, 750, 1120, 1669, 2224, 2783, 3335, 3882, 4355, 4833};
    public float[] NormalRpmTable = {720, 930, 1559, 2028, 2670, 3145, 3774, 4239, 4721, 5194};
    public float[] MaxRpmTable = {920, 1360, 1829, 2474, 2943, 3575, 4036, 4525, 4993, 5625};
    public float[] PitchingTable = {0.12f, 0.12f, 0.12f, 0.12f, 0.11f, 0.10f, 0.09f, 0.08f, 0.06f, 0.06f};

    public AudioSource Audio1;
    public AudioSource Audio2;
    public AudioSource Audio3;
    public AudioSource Audio4;
    public AudioSource Audio5;
    public AudioSource Audio6;
    public AudioSource Audio7;
    public AudioSource Audio8;
    public AudioSource Audio9;
    public AudioSource Audio10;
    public AudioSource Brake;

    public float RangeDivider = 4f;
    public float SoundReduce = 10f;
    public Rigidbody Car;
    public float Speed;
    public float RPM;
    public bool IsBraking;

    public int firstGear;
    public int secondGear;
    public int thirdGear;
    public int fourthGear;
    public int fifthGear;

    void Start() {
        Audio1 = Audio1.GetComponent<AudioSource>();
        Audio2 = Audio2.GetComponent<AudioSource>();
        Audio3 = Audio3.GetComponent<AudioSource>();
        Audio4 = Audio4.GetComponent<AudioSource>();
        Audio5 = Audio5.GetComponent<AudioSource>();
        Audio6 = Audio6.GetComponent<AudioSource>();
        Audio7 = Audio7.GetComponent<AudioSource>();
        Audio8 = Audio8.GetComponent<AudioSource>();
        Audio9 = Audio9.GetComponent<AudioSource>();
        Audio10 = Audio10.GetComponent<AudioSource>();
        Brake = Brake.GetComponent<AudioSource>();

        Audio1.volume = 0f;
        Audio2.volume = 0f;
        Audio3.volume = 0f;
        Audio4.volume = 0f;
        Audio5.volume = 0f;
        Audio6.volume = 0f;
        Audio7.volume = 0f;
        Audio8.volume = 0f;
        Audio9.volume = 0f;
        Audio10.volume = 0f;
        Brake.volume = 0f;

        Audio1.Play();
        Audio2.Play();
        Audio3.Play();
        Audio4.Play();
        Audio5.Play();
        Audio6.Play();
        Audio7.Play();
        Audio8.Play();
        Audio9.Play();
        Audio10.Play();
        Brake.Play();
    }

    //Convert speed into RPM (mimic gear changes)
    public void ToRPM() {
        Speed = Car.velocity.magnitude * 3.6f;

        if (Speed <= firstGear) {
            RPM = Speed * 55;
        } else if (Speed <= secondGear) {
            RPM = Speed * 27;
        } else if (Speed <= thirdGear) {
            RPM = Speed * 20;
        } else if (Speed <= fourthGear) {
            RPM = Speed * 18;
        } else if (Speed <= fifthGear) {
            RPM = Speed * 15;
        }
    }

    //Play braking sound if the car is moving
    public void BrakeSound() {
        IsBraking = Car.GetComponent<CarPhysics>().IsBraking;

        if (IsBraking && Speed > 1) {
            Brake.volume = 0.18f;
        } else {
            Brake.volume = 0f;
        }
    }

    void FixedUpdate() {
        ToRPM();
        BrakeSound();

        //Set volume/pitch By Speed
        //Script below is taken from a source online (refer to docs)
        for (int i = 0; i < 10; i++) {
            if (i == 0) {
                //Set Audio1
                if (RPM < MinRpmTable[i]) {
                    Audio1.volume = 1.5f;
                } else if (RPM >= MinRpmTable[i] && RPM < NormalRpmTable[i]) {
                    float Range = NormalRpmTable[i] - MinRpmTable[i];
                    float ReducedRPM = RPM - MinRpmTable[i];
                    Audio1.volume = ReducedRPM / Range;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                    Audio1.pitch = 1f - PitchingTable[i] + PitchMath;
                } else if (RPM >= NormalRpmTable[i] && RPM <= MaxRpmTable[i]) {
                    float Range = MaxRpmTable[i] - NormalRpmTable[i];
                    float ReducedRPM = RPM - NormalRpmTable[i];
                    Audio1.volume = 1f;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                    Audio1.pitch = 1f + PitchMath;
                } else if (RPM > MaxRpmTable[i]) {
                    float Range = (MaxRpmTable[i + 1] - MaxRpmTable[i]) / RangeDivider;
                    float ReducedRPM = RPM - MaxRpmTable[i];
                    Audio1.volume = 1f - ReducedRPM / Range;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                }
            } else if (i == 1) {
                //Set Audio2
                if (RPM < MinRpmTable[i]) {
                    Audio2.volume = 0.0f;
                } else if (RPM >= MinRpmTable[i] && RPM < NormalRpmTable[i]) {
                    float Range = NormalRpmTable[i] - MinRpmTable[i];
                    float ReducedRPM = RPM - MinRpmTable[i];
                    Audio2.volume = ReducedRPM / Range;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                    Audio2.pitch = 1f - PitchingTable[i] + PitchMath;
                } else if (RPM >= NormalRpmTable[i] && RPM <= MaxRpmTable[i]) {
                    float Range = MaxRpmTable[i] - NormalRpmTable[i];
                    float ReducedRPM = RPM - NormalRpmTable[i];
                    Audio2.volume = 1f;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                    Audio2.pitch = 1f + PitchMath;
                } else if (RPM > MaxRpmTable[i]) {
                    float Range = (MaxRpmTable[i + 1] - MaxRpmTable[i]) / RangeDivider;
                    float ReducedRPM = RPM - MaxRpmTable[i];
                    Audio2.volume = 1f - ReducedRPM / Range;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                }
            } else if (i == 2) {
                //Set Audio3
                if (RPM < MinRpmTable[i]) {
                    Audio3.volume = 0.0f;
                } else if (RPM >= MinRpmTable[i] && RPM < NormalRpmTable[i]) {
                    float Range = NormalRpmTable[i] - MinRpmTable[i];
                    float ReducedRPM = RPM - MinRpmTable[i];
                    Audio3.volume = ReducedRPM / Range;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                    Audio3.pitch = 1f - PitchingTable[i] + PitchMath;
                } else if (RPM >= NormalRpmTable[i] && RPM <= MaxRpmTable[i]) {
                    float Range = MaxRpmTable[i] - NormalRpmTable[i];
                    float ReducedRPM = RPM - NormalRpmTable[i];
                    Audio3.volume = 1f;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                    Audio3.pitch = 1f + PitchMath;
                } else if (RPM > MaxRpmTable[i]) {
                    float Range = (MaxRpmTable[i + 1] - MaxRpmTable[i]) / RangeDivider;
                    float ReducedRPM = RPM - MaxRpmTable[i];
                    Audio3.volume = 1f - ReducedRPM / Range;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                }
            } else if (i == 3) {
                //Set Audio4
                if (RPM < MinRpmTable[i]) {
                    Audio4.volume = 0.0f;
                } else if (RPM >= MinRpmTable[i] && RPM < NormalRpmTable[i]) {
                    float Range = NormalRpmTable[i] - MinRpmTable[i];
                    float ReducedRPM = RPM - MinRpmTable[i];
                    Audio4.volume = ReducedRPM / Range;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                    Audio4.pitch = 1f - PitchingTable[i] + PitchMath;
                } else if (RPM >= NormalRpmTable[i] && RPM <= MaxRpmTable[i]) {
                    float Range = MaxRpmTable[i] - NormalRpmTable[i];
                    float ReducedRPM = RPM - NormalRpmTable[i];
                    Audio4.volume = 1f;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                    Audio4.pitch = 1f + PitchMath;
                } else if (RPM > MaxRpmTable[i]) {
                    float Range = (MaxRpmTable[i + 1] - MaxRpmTable[i]) / RangeDivider;
                    float ReducedRPM = RPM - MaxRpmTable[i];
                    Audio4.volume = 1f - ReducedRPM / Range;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                }
            } else if (i == 4) {
                //Set Audio5
                if (RPM < MinRpmTable[i]) {
                    Audio5.volume = 0.0f;
                } else if (RPM >= MinRpmTable[i] && RPM < NormalRpmTable[i]) {
                    float Range = NormalRpmTable[i] - MinRpmTable[i];
                    float ReducedRPM = RPM - MinRpmTable[i];
                    Audio5.volume = ReducedRPM / Range;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                    Audio5.pitch = 1f - PitchingTable[i] + PitchMath;
                } else if (RPM >= NormalRpmTable[i] && RPM <= MaxRpmTable[i]) {
                    float Range = MaxRpmTable[i] - NormalRpmTable[i];
                    float ReducedRPM = RPM - NormalRpmTable[i];
                    Audio5.volume = 1f;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                    Audio5.pitch = 1f + PitchMath;
                } else if (RPM > MaxRpmTable[i]) {
                    float Range = (MaxRpmTable[i + 1] - MaxRpmTable[i]) / RangeDivider;
                    float ReducedRPM = RPM - MaxRpmTable[i];
                    Audio5.volume = 1f - ReducedRPM / Range;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                }
            } else if (i == 5) {
                //Set Audio6
                if (RPM < MinRpmTable[i]) {
                    Audio6.volume = 0.0f;
                } else if (RPM >= MinRpmTable[i] && RPM < NormalRpmTable[i]) {
                    float Range = NormalRpmTable[i] - MinRpmTable[i];
                    float ReducedRPM = RPM - MinRpmTable[i];
                    Audio6.volume = ReducedRPM / Range;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                    Audio6.pitch = 1f - PitchingTable[i] + PitchMath;
                } else if (RPM >= NormalRpmTable[i] && RPM <= MaxRpmTable[i]) {
                    float Range = MaxRpmTable[i] - NormalRpmTable[i];
                    float ReducedRPM = RPM - NormalRpmTable[i];
                    Audio6.volume = 1f;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                    Audio6.pitch = 1f + PitchMath;
                } else if (RPM > MaxRpmTable[i]) {
                    float Range = (MaxRpmTable[i + 1] - MaxRpmTable[i]) / RangeDivider;
                    float ReducedRPM = RPM - MaxRpmTable[i];
                    Audio6.volume = 1f - ReducedRPM / Range;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                }
            } else if (i == 6) {
                //Set Audio7
                if (RPM < MinRpmTable[i]) {
                    Audio7.volume = 0.0f;
                } else if (RPM >= MinRpmTable[i] && RPM < NormalRpmTable[i]) {
                    float Range = NormalRpmTable[i] - MinRpmTable[i];
                    float ReducedRPM = RPM - MinRpmTable[i];
                    Audio7.volume = ReducedRPM / Range;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                    Audio7.pitch = 1f - PitchingTable[i] + PitchMath;
                } else if (RPM >= NormalRpmTable[i] && RPM <= MaxRpmTable[i]) {
                    float Range = MaxRpmTable[i] - NormalRpmTable[i];
                    float ReducedRPM = RPM - NormalRpmTable[i];
                    Audio7.volume = 1f;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                    Audio7.pitch = 1f + PitchMath;
                } else if (RPM > MaxRpmTable[i]) {
                    float Range = (MaxRpmTable[i + 1] - MaxRpmTable[i]) / RangeDivider;
                    float ReducedRPM = RPM - MaxRpmTable[i];
                    Audio7.volume = 1f - ReducedRPM / Range;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                }
            } else if (i == 7) {
                //Set Audio8
                if (RPM < MinRpmTable[i]) {
                    Audio8.volume = 0.0f;
                } else if (RPM >= MinRpmTable[i] && RPM < NormalRpmTable[i]) {
                    float Range = NormalRpmTable[i] - MinRpmTable[i];
                    float ReducedRPM = RPM - MinRpmTable[i];
                    Audio8.volume = ReducedRPM / Range;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                    Audio8.pitch = 1f - PitchingTable[i] + PitchMath;
                } else if (RPM >= NormalRpmTable[i] && RPM <= MaxRpmTable[i]) {
                    float Range = MaxRpmTable[i] - NormalRpmTable[i];
                    float ReducedRPM = RPM - NormalRpmTable[i];
                    Audio8.volume = 1f;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                    Audio8.pitch = 1f + PitchMath;
                } else if (RPM > MaxRpmTable[i]) {
                    float Range = (MaxRpmTable[i + 1] - MaxRpmTable[i]) / RangeDivider;
                    float ReducedRPM = RPM - MaxRpmTable[i];
                    Audio8.volume = 1f - ReducedRPM / Range;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                }
            } else if (i == 8) {
                //Set Audio9
                if (RPM < MinRpmTable[i]) {
                    Audio9.volume = 0.0f;
                } else if (RPM >= MinRpmTable[i] && RPM < NormalRpmTable[i]) {
                    float Range = NormalRpmTable[i] - MinRpmTable[i];
                    float ReducedRPM = RPM - MinRpmTable[i];
                    Audio9.volume = ReducedRPM / Range;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                    Audio9.pitch = 1f - PitchingTable[i] + PitchMath;
                } else if (RPM >= NormalRpmTable[i] && RPM <= MaxRpmTable[i]) {
                    float Range = MaxRpmTable[i] - NormalRpmTable[i];
                    float ReducedRPM = RPM - NormalRpmTable[i];
                    Audio9.volume = 1f;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                    Audio9.pitch = 1f + PitchMath;
                } else if (RPM > MaxRpmTable[i]) {
                    float Range = (MaxRpmTable[i + 1] - MaxRpmTable[i]) / RangeDivider;
                    float ReducedRPM = RPM - MaxRpmTable[i];
                    Audio9.volume = 1f - ReducedRPM / Range;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                }
            } else if (i == 9) {
                //Set Audio10
                if (RPM < MinRpmTable[i]) {
                    Audio10.volume = 0.0f;
                } else if (RPM >= MinRpmTable[i] && RPM < NormalRpmTable[i]) {
                    float Range = NormalRpmTable[i] - MinRpmTable[i];
                    float ReducedRPM = RPM - MinRpmTable[i];
                    Audio10.volume = ReducedRPM / Range;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                    Audio10.pitch = 1f - PitchingTable[i] + PitchMath;
                } else if (RPM >= NormalRpmTable[i] && RPM <= MaxRpmTable[i]) {
                    float Range = MaxRpmTable[i] - NormalRpmTable[i];
                    float ReducedRPM = RPM - NormalRpmTable[i];
                    Audio10.volume = 1f;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                    Audio10.pitch = 1f + PitchMath;
                } else if (RPM > MaxRpmTable[i]) {
                    float Range = (MaxRpmTable[i + 1] - MaxRpmTable[i]) / RangeDivider;
                    float ReducedRPM = RPM - MaxRpmTable[i];
                    Audio10.volume = 1f - ReducedRPM / Range;
                    float PitchMath = (ReducedRPM * PitchingTable[i]) / Range;
                }
            }
        }

        //Reduce the overall volume
        Audio1.volume = Audio1.volume / SoundReduce;
        Audio2.volume = Audio2.volume / SoundReduce;
        Audio3.volume = Audio3.volume / SoundReduce;
        Audio4.volume = Audio4.volume / SoundReduce;
        Audio5.volume = Audio5.volume / SoundReduce;
        Audio6.volume = Audio6.volume / SoundReduce;
        Audio7.volume = Audio7.volume / SoundReduce;
        Audio8.volume = Audio8.volume / SoundReduce;
        Audio9.volume = Audio9.volume / SoundReduce;
        Audio10.volume = Audio10.volume / SoundReduce;

    }

}
