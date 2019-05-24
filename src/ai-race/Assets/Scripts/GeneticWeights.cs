using System.Collections.Generic;
using System.Globalization;
using System.IO;

//Inherits class GeneticNetwork
public class GeneticWeights : GeneticNetwork {

    //Loades weights from text file
    public List<double[][]> LoadWeights(string fileName) {
        List<double[][]> weights = new List<double[][]>();

        using (var file = new StreamReader(fileName)) {

            //for each layer
            for (int i = 0; i < lengthLayers - 1; i++) {
                double[][] layerWeights = new double[layers[i]][];

                //for each row
                for (int j = 0; j < layers[i]; j++) {
                    layerWeights[j] = new double[layers[i + 1]];

                    //and for each column read a line and add it to layerweights
                    for (int k = 0; k < layers[i + 1]; k++) {
                        layerWeights[j][k] = double.Parse(file.ReadLine());
                    }
                }
                //add that layerweight to weights list
                weights.Add(layerWeights);
            }
        }
        return weights;
    }
}
