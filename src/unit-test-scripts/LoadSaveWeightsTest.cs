using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LoadSaveWeightsTest {

    [Test]
    public void BackPropSaveLoad1() {
        // ARRANGE
        string path = Application.dataPath + "/Test/testWeights1.text";
        List<double[][]> weights = new List<double[][]>();

        double[][] w1 = new double[][] {
            new double[] { 1.0, 3.450, 5.34, 7.0, 9.4 },
            new double[] { 0.2, 2.0, 4.0, 6.0, 11.12}
        };

        weights.Add(w1);

        // ACT
        SaveWeights.WriteToFile(path, weights);

        BackPropWeights prop = new BackPropWeights();
        prop.layers = new int[] {5, 2};
        prop.lengthLayers = 2;

        List<double[][]> weightsSaved = prop.LoadWeights(path);

        // ASSERT
        Assert.That(weightsSaved, Is.EqualTo(weights));
    }

    [Test]
    public void BackPropSaveLoad2() {
        // ARRANGE
        string path = Application.dataPath + "/Test/testWeights2.text";
        List<double[][]> weights = new List<double[][]>();

        double[][] w1 = new double[][] {
            new double[] { 1.77, 3.0, 5.0, 7.67, 9.0 },
            new double[] { 0.0, 2.8, 4.0, 6.0, 11.999},
            new double[] { 1.0, 3.0, 5.0, 7.0, 9.0 },
            new double[] { 0.67, 2.0, 4.0, 6.7, 11.0}
        };

        weights.Add(w1);

        // ACT
        SaveWeights.WriteToFile(path, weights);

        BackPropWeights prop = new BackPropWeights();
        prop.layers = new int[] { 5, 4 };
        prop.lengthLayers = 2;

        List<double[][]> weightsSaved = prop.LoadWeights(path);

        // ASSERT
        Assert.That(weightsSaved, Is.EqualTo(weights));
    }

    [Test]
    public void GeneticSaveLoad1() {
        // ARRANGE
        string path = Application.dataPath + "/Test/testWeights3.text";
        List<double[][]> weights = new List<double[][]>();

        double[][] w1 = new double[][] {
            new double[] { 1.0, 3.5},
            new double[] { 0.1, 2.0},
            new double[] { 0.4, 2.3},
            new double[] { 0.6, 2.0}
        };

        weights.Add(w1);

        // ACT
        SaveWeights.WriteToFile(path, weights);

        GeneticWeights prop = new GeneticWeights();
        prop.layers = new int[] { 4, 2 };
        prop.lengthLayers = 2;

        List<double[][]> weightsSaved = prop.LoadWeights(path);

        // ASSERT
        Assert.That(weightsSaved, Is.EqualTo(weights));
    }

    [Test]
    public void GeneticSaveLoad2() {
        // ARRANGE
        string path = Application.dataPath + "/Test/testWeights4.text";
        List<double[][]> weights = new List<double[][]>();

        double[][] w1 = new double[][] {
            new double[] { 1.0, 3.0, 5.0, 7.0, 9.67 },
            new double[] { 0.0, 2.0, 4.0, 6.0, 11.0},
            new double[] { 0.0, 2.89, 4.0, 6.0, 11.0},
            new double[] { 0.45, 2.0, 4.3, 6.0, 11.65},
            new double[] { 0.0, 2.0, 4.0, 6.67, 11.0},
            new double[] { 0.34, 2.0, 4.0, 6.0, 11.65}
        };

        weights.Add(w1);

        // ACT
        SaveWeights.WriteToFile(path, weights);

        GeneticWeights prop = new GeneticWeights();
        prop.layers = new int[] { 6, 5 };
        prop.lengthLayers = 2;

        List<double[][]> weightsSaved = prop.LoadWeights(path);

        // ASSERT
        Assert.That(weightsSaved, Is.EqualTo(weights));
    }
}
