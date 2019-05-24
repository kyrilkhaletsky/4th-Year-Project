using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GeneticNetworkTest {

    [Test]
    public void Init1() {
        // ARRANGE
        int population = 10;
        GeneticNetwork[] genetic = new GeneticNetwork[population];
        int expectedlength = 10;

        // ACT
        for (int i = 0; i < population; i++) {
            genetic[i] = new GeneticNetwork();
        }

        // ASSERT
        Assert.That(genetic.Length, Is.EqualTo(expectedlength));

        for (int i = 0; i < population; i++) {
            Assert.That(genetic[i].GetWeights().GetType() == typeof(List<double[][]>));
        }      
    }

    [Test]
    public void Init2() {
        // ARRANGE
        int population = 30;
        GeneticNetwork[] genetic = new GeneticNetwork[population];
        int expectedlength = 30;

        // ACT
        for (int i = 0; i < population; i++) {
            genetic[i] = new GeneticNetwork();
        }

        // ASSERT
        Assert.That(genetic.Length, Is.EqualTo(expectedlength));

        for (int i = 0; i < population; i++) {
            Assert.That(genetic[i].GetWeights().GetType() == typeof(List<double[][]>));
        }
    }

    [Test]
    public void Random1() {
        // ARRANGE
        GeneticNetwork genetic = new GeneticNetwork();

        // ACT
        double random = genetic.GetRandomWeight();

        // ASSERT
        Assert.That(random, Is.InRange(-1f, 1f));
    }

    [Test]
    public void Random2() {
        // ARRANGE
        GeneticNetwork genetic = new GeneticNetwork();

        // ACT
        double random = genetic.GetRandomWeight();

        // ASSERT
        Assert.That(random, Is.InRange(-1f, 1f));
    }

    [Test]
    public void Sigmoid1() {
        // ARRANGE
        GeneticNetwork genetic = new GeneticNetwork();
        List<double> sums = new List<double> {
            1.0,
            6.3,
            2.2,
            0.00005
        };

        // ACT
        List<double> outputs = genetic.Sigmoid(sums);

        // ASSERT
        foreach (var output in outputs) {
            Assert.That(output, Is.InRange(0f, 1f));
        }     
    }

    [Test]
    public void Sigmoid2() {
        // ARRANGE
        GeneticNetwork genetic = new GeneticNetwork();
        List<double> sums = new List<double> {
            11.0,
            6.33333,
            22.2,
            6.00005,
            0.005005,
            0.00005,
            7.00005
        };

        // ACT
        List<double> outputs = genetic.Sigmoid(sums);

        // ASSERT
        foreach (var output in outputs) {
            Assert.That(output, Is.InRange(0f, 1f));
        }
    }

    [Test]
    public void FeedForward1() {
        // ARRANGE
        GeneticNetwork genetic = new GeneticNetwork();
        double[] inputs = {0.2, 0, 0, 0, 0.2 };

        int expectedlength = 2;

        // ACT
        List<double> outputs = genetic.FeedForward(inputs);

        // ASSERT
        Assert.That(outputs.Count, Is.EqualTo(expectedlength));

        foreach (var output in outputs) {
            Assert.That(output, Is.InRange(0f, 1f));
        }
    }

    [Test]
    public void FeedForward2() {
        // ARRANGE
        GeneticNetwork genetic = new GeneticNetwork();
        double[] inputs = { 0.6, 0.4, 0.2, 0, 0 };

        int expectedlength = 2;

        // ACT
        List<double> outputs = genetic.FeedForward(inputs);

        // ASSERT
        Assert.That(outputs.Count, Is.EqualTo(expectedlength));

        foreach (var output in outputs) {
            Assert.That(output, Is.InRange(0f, 1f));
        }
    }

    [Test]
    public void Mutate1() {
        // ARRANGE
        List<string> parent = new List<string>();
        List<string> prob = new List<string>();
        System.Random randomBool = new System.Random();

        double probability = 0.2;

        // ACT
        int i = 0;
        while (i < 100) {
            if (randomBool.Next(2) == 0) {
                parent.Add("A");
            } else {
                parent.Add("B");
            }

            if (Random.Range(0f, 1f) < probability) {
                prob.Add("A");
            }
            i++;
        }

        // ASSERT
        Assert.That(parent.Where(x => x.Equals("A")).Count(), Is.InRange(40, 60));
        Assert.That(parent.Where(x => x.Equals("B")).Count(), Is.InRange(40, 60));
        Assert.That(prob.Count, Is.InRange(0, 30));

        //CHECK ACTUAL COUNTS
        Debug.Log(parent.Where(x => x.Equals("A")).Count());
        Debug.Log(parent.Where(x => x.Equals("B")).Count());
        Debug.Log(prob.Count);      
    }

    [Test]
    public void Mutate2() {
        // ARRANGE
        List<string> parent = new List<string>();
        List<string> prob = new List<string>();
        System.Random randomBool = new System.Random();

        double probability = 0.05;

        // ACT
        int i = 0;
        while (i < 100) {
            if (randomBool.Next(2) == 0) {
                parent.Add("A");
            } else {
                parent.Add("B");
            }

            if (Random.Range(0f, 1f) < probability) {
                prob.Add("A");
            }
            i++;
        }

        // ASSERT
        Assert.That(parent.Where(x => x.Equals("A")).Count(), Is.InRange(40, 60));
        Assert.That(parent.Where(x => x.Equals("B")).Count(), Is.InRange(40, 60));
        Assert.That(prob.Count, Is.InRange(0, 10));

        //CHECK ACTUAL COUNTS
        Debug.Log(parent.Where(x => x.Equals("A")).Count());
        Debug.Log(parent.Where(x => x.Equals("B")).Count());
        Debug.Log(prob.Count);
    }

}
