using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ResetPositionTest {

    [Test]
    public void FindClosestPoint1() {
        // ARRANGE
        var reset = new ResetPosition();
        var body = new Rigidbody();
        var pos = new Vector3(0,0,0);

        var p1 = new GameObject();
        var p2 = new GameObject();

        p1.transform.position = new Vector3(0, 5, 0);
        p2.transform.position = new Vector3(0, 10, 0);

        GameObject[] points = new GameObject[2];

        points[0] = p1;
        points[1] = p2;

        var expected = p1;

        // ACT
        var closest = reset.ResetCurrentCar(body, pos, points);

        // ASSERT
        Assert.That(closest, Is.EqualTo(expected));   
    }

    [Test]
    public void FindClosestPoint2() {
        // ARRANGE
        var reset = new ResetPosition();
        var body = new Rigidbody();
        var pos = new Vector3(1, 4, 1);

        var p1 = new GameObject();
        var p2 = new GameObject();

        p1.transform.position = new Vector3(1, 50, 2);
        p2.transform.position = new Vector3(1, 60, 2);

        GameObject[] points = new GameObject[2];

        points[0] = p1;
        points[1] = p2;

        var expected = p1;

        // ACT
        var closest = reset.ResetCurrentCar(body, pos, points);

        // ASSERT
        Assert.That(closest, Is.EqualTo(expected));
    }

    [Test]
    public void FindClosestPoint3() {
        // ARRANGE
        var reset = new ResetPosition();
        var body = new Rigidbody();
        var pos = new Vector3(1, 1, 1);

        var p1 = new GameObject();
        var p2 = new GameObject();

        p1.transform.position = new Vector3(15, 1, 1);
        p2.transform.position = new Vector3(30, 1, 1);

        GameObject[] points = new GameObject[2];

        points[0] = p1;
        points[1] = p2;

        var expected = p1;

        // ACT
        var closest = reset.ResetCurrentCar(body, pos, points);

        // ASSERT
        Assert.That(closest, Is.EqualTo(expected));
    }

}
