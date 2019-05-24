using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class GeneticNetwork {

    public int[] layers = { 5, 8, 2 };
    public int lengthLayers = 3;
    public int mFactor = 4;
    public List<double[][]> weights;

    //Gets List of weights
    public List<double[][]> GetWeights() {
        return weights;
    }

    //Sets List of weights
    public void SetWeights(List<double[][]> weights) {
        this.weights = weights;
    }

    //returns random weight between -1 and 1
    public double GetRandomWeight() {
        return Random.Range(-1.0f, 1.0f);
    }

    //Genetic Network constructor, initiates weights based on population number
    public GeneticNetwork() {
        this.weights = new List<double[][]>();

        //for each layer create new matrix
        for (int i = 0; i < lengthLayers - 1; i++) {
            double[][] layerWeights = new double[layers[i]][];

            //for each row
            for (int j = 0; j < layers[i]; j++) {
                layerWeights[j] = new double[layers[i + 1]];

                //and for each column
                for (int k = 0; k < layers[i + 1]; k++) {
                    //set the current layer to a random weight
                    layerWeights[j][k] = GetRandomWeight();
                }
            }
            //add that weight to the list of weights
            weights.Add(layerWeights);
        }
    }

    //Crossover & mutation of weights based on selected probability at end of generation
    public GeneticNetwork(GeneticNetwork Father, GeneticNetwork Mother, float probability) {
        System.Random randomBool = new System.Random();
        this.weights = new List<double[][]>();
        
        //For each layer create new matrix
        for (int i = 0; i < lengthLayers - 1; i++) {
            double[][] layerWeights = new double[layers[i]][];

            //for each row
            for (int j = 0; j < layers[i]; j++) {
                layerWeights[j] = new double[layers[i + 1]];

                //and for each column
                for (int k = 0; k < layers[i + 1]; k++) {

                    //randomly crossover either mother or father
                    if (randomBool.Next(2) == 0) {
                        layerWeights[j][k] = Father.weights[i][j][k];
                    } else {
                        layerWeights[j][k] = Mother.weights[i][j][k];
                    }

                    //mutate that layer to a random weight if it falls under the probability
                    if (Random.Range(0f, 1f) < probability) {
                        layerWeights[j][k] = GetRandomWeight();
                    }
                }
            }
            //Add that new weight to the new list of weights (children)
            weights.Add(layerWeights);
        }
    }

    //Calculate outputs based on given inputs
    public List<double> FeedForward(double[] inputs) {

        //Create list of outputs and populate it with inputs
        List<double> outputs = new List<double>();
        for (int i = 0; i < inputs.Length; i++) {
            outputs.Add(inputs[i]);
        }

        //for each layer create new list of 0s
        for (int i = 0; i < lengthLayers - 1; i++) {
            List<double> sums = new List<double>(new double[layers[i + 1]]);

            //for each output
            for (int j = 0; j < outputs.Count; j++) {

                //and for each hidden and output layer
                for (int k = 0; k < layers[i + 1]; k++) {
                    //multiply inputs by the weights of the current car
                    sums[k] += outputs[j] * weights[i][j][k];
                }
            }
            //Apply sigmoid function on each sum
            outputs = Sigmoid(sums);       
        }
        return outputs;
    }

    //create new list of outputs, and set value between 0 and 1
    public List<double> Sigmoid(List<double> sums) {
        List<double> outputs = new List<double>();
        foreach (double sum in sums) {
            outputs.Add(1 / (1 + Mathf.Exp(-(float) (sum * mFactor))));
        }
        return outputs;
    }
}
