using UnityEngine;
using UnityEngine.Tilemaps;

namespace RetroSim 
{
    public class SandSimulation : SimulationBase, ISimulation
    {
        public const string TileTag = "Sand";

        private bool mTryGoRightFirst; // For Flip - Flop flow logic

        public SandSimulation(Tilemap tilemap) 
            : base(tilemap)
        {

        }

        public void ApplySimulationRules(TileBase tile, Vector3Int currentPos)
        {
            // Try Down First
            if (TryGoDown(tile, currentPos)) { return; }

            // Down Left or Down Right
            if (mTryGoRightFirst)
            {
                if (TryGoDownRight(tile, currentPos)) { return; }
                if (TryGoDownLeft(tile, currentPos)) { return; }
            }
            else
            {
                if (TryGoDownLeft(tile, currentPos)) { return; }
                if (TryGoDownRight(tile, currentPos)) { return; }
            }

            mTryGoRightFirst = !mTryGoRightFirst;
        }
    }
}
