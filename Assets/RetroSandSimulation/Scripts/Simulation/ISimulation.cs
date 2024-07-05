using UnityEngine;
using UnityEngine.Tilemaps;

namespace RetroSim 
{
    public interface ISimulation
    {
        void ApplySimulationRules(TileBase tile, Vector3Int currentPos);
    }
}

