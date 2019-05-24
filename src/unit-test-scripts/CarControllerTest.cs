using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class CarControllerTest {

    [Test]
    public void GetValues1() {
        // ARRANGE
        var controller = new CarController();

        controller.GeneticEngine = 0.5;
        controller.GeneticSteering = 0.5;
        controller.BackPropEngine = 0.5;
        controller.BackPropSteering = 0.5;

        var expectedGE = 0.5;
        var expectedGS = 0.5;
        var expectedBE = 0.5;
        var expectedBS = 0.5;

        // ACT
        var GE = controller.GeneticEngine;
        var GS = controller.GeneticSteering;
        var BE = controller.BackPropEngine;
        var BS = controller.BackPropSteering;

        // ASSERT
        Assert.That(expectedGE, Is.EqualTo(GE));
        Assert.That(expectedGS, Is.EqualTo(GS));
        Assert.That(expectedBE, Is.EqualTo(BE));
        Assert.That(expectedBS, Is.EqualTo(BS));
    }

    [Test]
    public void GetValues2() {
        // ARRANGE
        var controller = new CarController();

        controller.GeneticEngine = 1;
        controller.GeneticSteering = 0.8;
        controller.BackPropEngine = 0.1;
        controller.BackPropSteering = 0.4;

        var expectedGE = 1;
        var expectedGS = 0.8;
        var expectedBE = 0.1;
        var expectedBS = 0.4;

        // ACT
        var GE = controller.GeneticEngine;
        var GS = controller.GeneticSteering;
        var BE = controller.BackPropEngine;
        var BS = controller.BackPropSteering;

        // ASSERT
        Assert.That(expectedGE, Is.EqualTo(GE));
        Assert.That(expectedGS, Is.EqualTo(GS));
        Assert.That(expectedBE, Is.EqualTo(BE));
        Assert.That(expectedBS, Is.EqualTo(BS));
    }

    [Test]
    public void GetValues3() {
        // ARRANGE
        var controller = new CarController();

        controller.GeneticEngine = 0;
        controller.GeneticSteering = 0.05;
        controller.BackPropEngine = 1;
        controller.BackPropSteering = 0.8;

        var expectedGE = 0;
        var expectedGS = 0.05;
        var expectedBE = 1;
        var expectedBS = 0.8;

        // ACT
        var GE = controller.GeneticEngine;
        var GS = controller.GeneticSteering;
        var BE = controller.BackPropEngine;
        var BS = controller.BackPropSteering;

        // ASSERT
        Assert.That(expectedGE, Is.EqualTo(GE));
        Assert.That(expectedGS, Is.EqualTo(GS));
        Assert.That(expectedBE, Is.EqualTo(BE));
        Assert.That(expectedBS, Is.EqualTo(BS));
    }
}
