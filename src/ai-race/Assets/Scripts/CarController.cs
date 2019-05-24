using System.Collections;
using System.Collections.Generic;

//Sets and Gets Steering & Engine output values of algorithms
public class CarController {

    public static double _GeneticSteering;
    public static double _GeneticEngine;

    public static double _BackPropSteering;
    public static double _BackPropEngine;

    public double GeneticSteering {
        get {return _GeneticSteering; }
        set { _GeneticSteering = value; }
    }

    public double GeneticEngine {
        get { return _GeneticEngine; }
        set { _GeneticEngine = value; }
    }

    public double BackPropSteering {
        get { return _BackPropSteering; }
        set { _BackPropSteering = value; }
    }

    public double BackPropEngine {
        get { return _BackPropEngine; }
        set { _BackPropEngine = value; }
    }
}
