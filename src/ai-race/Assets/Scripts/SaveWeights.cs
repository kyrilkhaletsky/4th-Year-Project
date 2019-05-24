using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//Saves all weights to a text file
public static class SaveWeights {
    
    public static void WriteToFile(string filename, List<double[][]> weights) {
        using (var file = new StreamWriter(filename)) {

            //for each weight in weights
            foreach (var weight in weights) {

                //for each row in weight
                foreach (var i in weight) {

                    //and for each column in row write it to file
                    foreach (var j in i) {
                        file.WriteLine(j.ToString());
                    }
                }
            }
        }
    }
}
