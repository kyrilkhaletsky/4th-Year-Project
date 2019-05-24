using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class CarSelectTest {

    public List<GameObject> Cars;
    public GameObject EVO;
    public GameObject GTO;
    public GameObject Z66;
    public GameObject GTX;

    [Test]
    public void Select1() {
        // ARRANGE
        var select = new CarSelectController();

        // ACT
        Cars = new List<GameObject> { EVO, GTO, Z66, GTX };

        // ASSERT
        Assert.That(select.Cars, !Is.EqualTo(Cars));
    }

    [Test]
    public void Select2() {
        // ARRANGE
        var select = new CarSelectController();

        // ACT
        Cars = new List<GameObject> { EVO, GTO, Z66 };

        // ASSERT
        Assert.That(select.Cars, !Is.EqualTo(Cars));
    }

    [Test]
    public void Select3() {
        // ARRANGE
        var select = new CarSelectController();

        // ACT
        Cars = new List<GameObject> { EVO, Z66, GTX };

        // ASSERT
        Assert.That(select.Cars, !Is.EqualTo(Cars));
    }
}
