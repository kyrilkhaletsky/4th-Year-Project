using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class CarPhysicsTest {

    [Test]
    public void Acceleration1() {
        // ARRANGE
        float currentSpeed = 100;
        float scaledTorque = 1;
        int dropSpeed = 150;
        int maxSpeed = 300;

        // ACT
        if (currentSpeed < dropSpeed) {
            scaledTorque = Mathf.Lerp(scaledTorque, scaledTorque / 3f, currentSpeed / dropSpeed);
        } else {
            scaledTorque = Mathf.Lerp(scaledTorque / 3f, 0, (currentSpeed - dropSpeed) / (maxSpeed - dropSpeed));
        }

        // LOG
        Debug.Log(scaledTorque);
    }

    [Test]
    public void Acceleration2() {
        // ARRANGE
        float currentSpeed = 50;
        float scaledTorque = 1;
        int dropSpeed = 150;
        int maxSpeed = 300;

        // ACT
        if (currentSpeed < dropSpeed) {
            scaledTorque = Mathf.Lerp(scaledTorque, scaledTorque / 3f, currentSpeed / dropSpeed);
        } else {
            scaledTorque = Mathf.Lerp(scaledTorque / 3f, 0, (currentSpeed - dropSpeed) / (maxSpeed - dropSpeed));
        }

        // LOG
        Debug.Log(scaledTorque);
    }

    [Test]
    public void Acceleration3() {
        // ARRANGE
        float currentSpeed = 100;
        float scaledTorque = 1;
        int dropSpeed = 120;
        int maxSpeed = 250;

        // ACT
        if (currentSpeed < dropSpeed) {
            scaledTorque = Mathf.Lerp(scaledTorque, scaledTorque / 3f, currentSpeed / dropSpeed);
        } else {
            scaledTorque = Mathf.Lerp(scaledTorque / 3f, 0, (currentSpeed - dropSpeed) / (maxSpeed - dropSpeed));
        }

        // LOG
        Debug.Log(scaledTorque);
    }

    [Test]
    public void Acceleration4() {
        // ARRANGE
        float currentSpeed = 150;
        float scaledTorque = 0.8f;
        int dropSpeed = 180;
        int maxSpeed = 250;

        // ACT
        if (currentSpeed < dropSpeed) {
            scaledTorque = Mathf.Lerp(scaledTorque, scaledTorque / 3f, currentSpeed / dropSpeed);
        } else {
            scaledTorque = Mathf.Lerp(scaledTorque / 3f, 0, (currentSpeed - dropSpeed) / (maxSpeed - dropSpeed));
        }

        // LOG
        Debug.Log(scaledTorque);
    }

    [Test]
    public void Acceleration5() {
        // ARRANGE
        float currentSpeed = 220;
        float scaledTorque = 1;
        int dropSpeed = 180;
        int maxSpeed = 350;

        // ACT
        if (currentSpeed < dropSpeed) {
            scaledTorque = Mathf.Lerp(scaledTorque, scaledTorque / 3f, currentSpeed / dropSpeed);
        } else {
            scaledTorque = Mathf.Lerp(scaledTorque / 3f, 0, (currentSpeed - dropSpeed) / (maxSpeed - dropSpeed));
        }

        // LOG
        Debug.Log(scaledTorque);
    }


}
