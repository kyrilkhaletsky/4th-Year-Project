using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPropNetwork {

    public int[] layers = { 5, 10, 10, 2 };
    public int lengthLayers = 4;
    public double learningRate;

    public List<double[][]> weights;
    public List<double[][]> weightsDer;
    public List<double[]> layerOutputs;
    public List<double[]> layerInputs;  
    public List<double[]> layerLoss;

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
        return UnityEngine.Random.Range(-1.0f, 1.0f);
    }

    //activates value using the tanh formula
    public double TanH(double value) {
        return 1 - (value * value);
    }

    //Backpropogation Network constructor, initiate variables
    public BackPropNetwork() {
        this.weights = new List<double[][]>();
        this.weightsDer = new List<double[][]>();
        this.layerOutputs = new List<double[]>();
        this.layerInputs = new List<double[]>();     
        this.layerLoss = new List<double[]>();
        
        //for each layer create new matrix
        for (int i = 0; i < lengthLayers - 1; i++) {
            double[][] layerWeights = new double[layers[i + 1]][];
            double[][] derWeights = new double[layers[i + 1]][];

            //for each output
            for (int j = 0; j < layers[i + 1]; j++) {
                layerWeights[j] = new double[layers[i]];
                derWeights[j] = new double[layers[i]];

                //and for each input
                for (int k = 0; k < layers[i]; k++) {
                    //set the current layer to a random weight
                    layerWeights[j][k] = GetRandomWeight();
                }
            }
            //add weights to the lists of weights
            weights.Add(layerWeights);
            weightsDer.Add(derWeights);
            //create double arrays of the current layer length
            layerInputs.Add(new double[layers[i]]);
            layerOutputs.Add(new double[layers[i + 1]]);
            layerLoss.Add(new double[layers[i + 1]]);        
        }
    }

    //Calculate outputs based on given inputs
    public double[] FeedForward(double[] inputs) {
        //Create list of outputs and populate it with inputs
        List<double> outputs = new List<double>();
        for (int i = 0; i < inputs.Length; i++) {
            outputs.Add(inputs[i]);
        }

        //for each layer create new list of 0s
        for (int i = 0; i < lengthLayers - 1; i++) {
            List<double> sums = new List<double>(new double[layers[i + 1]]);
            
            //add current layer inputs into inputs list
            this.layerInputs[i] = outputs.ToArray();

            //for each output
            for (int j = 0; j < layers[i + 1]; j++) {

                //and for each input
                for (int k = 0; k < layers[i]; k++) {
                    //multiply inputs by the weights of the current car
                    sums[j] += outputs[k] * weights[i][j][k];
                }
                //hyperbolic tangent returns -1 or 1 if +/- infinity
                sums[j] = (double) Math.Tanh(sums[j]);
            }
            //set sums as new outputs and add outputs to current layer outputs
            outputs = sums;
            this.layerOutputs[i] = outputs.ToArray();
        }
        //return last output of all layers
        return layerOutputs[lengthLayers - 2];
    }

    //Propogate user input through the network
    public void Propogate(double[] inputs, double learningRate) {

        this.learningRate = learningRate;

        //For all layers (run in reverse)
        for (int l = lengthLayers - 2; l >= 0; l--) {
            
            //if its the last layer
            if (l == lengthLayers - 2) {
                for (int i = 0; i < l; i++) {
                    //calculate error (user vs actual) and multiply by activated output to get loss
                    double error = layerOutputs[l][i] - inputs[i];
                    this.layerLoss[l][i] = TanH(layerOutputs[l][i]) * error;
                }
                CalculateDerivative(l);
            
            //if its the hidden layers
            } else {
                for (int i = 0; i < layers[l + 1]; i++) {

                    //calculate current layer loss based on weights and loss of next layer
                    layerLoss[l][i] = 0;
                    for (int j = 0; j < layerLoss[l + 1].Length; j++) {
                        layerLoss[l][i] += weights[l + 1][j][i] * layerLoss[l + 1][j];
                    }
                    //activate loss using the tanh function
                    this.layerLoss[l][i] *= TanH(layerOutputs[l][i]);
                }
                CalculateDerivative(l);
            }

            //for each output layer
            for (int i = 0; i < layers[l + 1]; i++) {
                //and each input layer
                for (int j = 0; j < layers[l]; j++) {
                    //update the weights and smooth them by the learning rate
                    this.weights[l][i][j] -= weightsDer[l][i][j] * learningRate;
                }
            }
        }
    }

    //calculate weightsDer based on layer inputs & loss
    public void CalculateDerivative(int l) {
        for (int i = 0; i < layers[l + 1]; i++) {
            for (int j = 0; j < layers[l]; j++) {
                this.weightsDer[l][i][j] = layerInputs[l][j] * layerLoss[l][i];
            }
        }
    }
}
