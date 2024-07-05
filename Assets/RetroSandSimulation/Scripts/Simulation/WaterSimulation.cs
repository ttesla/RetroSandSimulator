using UnityEngine;
using UnityEngine.Tilemaps;

namespace RetroSim 
{
    public class WaterSimulation : SimulationBase, ISimulation
    {
        public const string TileTag = "Water";

        private bool mTryGoRightFirst; // For Flip - Flop flow logic

        public WaterSimulation(Tilemap tilemap)
            : base(tilemap)
        {

        }

        public void ApplySimulationRules(TileBase tile, Vector3Int currentPos)
        {
            // Try Down First
            if (TryGoDown(tile, currentPos)) { return; }

            if (mTryGoRightFirst) 
            {
                if (TryGoRight(tile, currentPos)) { return; }
                if (TryGoLeft(tile, currentPos)) { return; }
            }
            else 
            {
                if (TryGoLeft(tile, currentPos)) { return; }
                if (TryGoRight(tile, currentPos)) { return; }
            }

            //if (TryGoDownRight(tile, currentPos)) { return; }
            //if (TryGoDownLeft(tile, currentPos)) { return; }

            mTryGoRightFirst = !mTryGoRightFirst;
        }
    }
}
