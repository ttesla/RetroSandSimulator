using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RetroSim 
{
    public static class DensityMap
    {
        public static Dictionary<string, int> Map = new Dictionary<string, int> 
        { 
            { WaterSimulation.TileTag, 0 },
            { SandSimulation.TileTag , 5 },
            { "Rock" , 10 },
        };
    }
}
