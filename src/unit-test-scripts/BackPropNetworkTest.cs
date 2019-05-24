using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class BackPropNetworkTest {

    [Test]
    public void Init1() {
        // ARRANGE
        BackPropNetwork backprop = new BackPropNetwork();

        int expectedlength = 3;

        // ACT

        // ASSERT
        Assert.That(backprop.GetWeights().Count, Is.EqualTo(expectedlength));
        Assert.That(backprop.weightsDer.Count, Is.EqualTo(expectedlength));
        Assert.That(backprop.layerInputs.Count, Is.EqualTo(expectedlength));
        Assert.That(backprop.layerOutputs.Count, Is.EqualTo(expectedlength));
        Assert.That(backprop.layerLoss.Count, Is.EqualTo(expectedlength));

        Assert.That(backprop.GetWeights().GetType() == typeof(List<double[][]>));
        Assert.That(backprop.weightsDer.GetType() == typeof(List<double[][]>));
        Assert.That(backprop.layerInputs.GetType() == typeof(List<double[]>));
        Assert.That(backprop.layerOutputs.GetType() == typeof(List<double[]>));
        Assert.That(backprop.layerLoss.GetType() == typeof(List<double[]>));
    }

    [Test]
    public void Random1() {
        // ARRANGE
        BackPropNetwork backprop = new BackPropNetwork();

        // ACT
        double random = backprop.GetRandomWeight();

        // ASSERT
        Assert.That(random, Is.InRange(-1f, 1f));
    }

    [Test]
    public void Random2() {
        // ARRANGE
        BackPropNetwork backprop = new BackPropNetwork();

        // ACT
        double random = backprop.GetRandomWeight();

        // ASSERT
        Assert.That(random, Is.InRange(-1f, 1f));
    }

    [Test]
    public void TanH1() {
        // ARRANGE
        BackPropNetwork backprop = new BackPropNetwork();
        List<double> inputs = new List<double> {
            0.056566161,
            0.3651,
            0.268461,
            0.00005,
            0.00123005,
            0.0,
            0.0000125,
            0.0440005,
            1
        };

        // ACT
        List<double> outputs = new List<double>();
        foreach (var input in inputs) {
            outputs.Add(backprop.TanH(input));
        }
        

        // ASSERT
        foreach (var output in outputs) {
            Assert.That(output, Is.InRange(0f, 1f));
        }
    }

    [Test]
    public void TanH2() {
        // ARRANGE
        BackPropNetwork backprop = new BackPropNetwork();
        List<double> inputs = new List<double> {
            0.0,
            0.7,
            0.4,
            0.1,
            0.2222
        };

        // ACT
        List<double> outputs = new List<double>();
        foreach (var input in inputs) {
            outputs.Add(backprop.TanH(input));
        }


        // ASSERT
        foreach (var output in outputs) {
            Assert.That(output, Is.InRange(0f, 1f));
        }
    }

    [Test]
    public void FeedForward1() {
        // ARRANGE
        BackPropNetwork backprop = new BackPropNetwork();
        double[] inputs = { 0.2, 0, 0, 0, 0.2 };

        int expectedlength = 2;

        // ACT
        double[] outputs = backprop.FeedForward(inputs);

        // ASSERT
        Assert.That(outputs.Length, Is.EqualTo(expectedlength));

        foreach (var output in outputs) {
            Assert.That(output, Is.InRange(-1f, 1f));
        }
    }

    [Test]
    public void FeedForward2() {
        // ARRANGE
        BackPropNetwork backprop = new BackPropNetwork();
        double[] inputs = { 0.6, 0.4, 0.2, 0, 0 };

        int expectedlength = 2;

        // ACT
        double[] outputs = backprop.FeedForward(inputs);

        // ASSERT
        Assert.That(outputs.Length, Is.EqualTo(expectedlength));

        foreach (var output in outputs) {
            Assert.That(output, Is.InRange(-1f, 1f));
        }
    }
}
