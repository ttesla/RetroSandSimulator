using UnityEngine;
using UnityEngine.Tilemaps;

namespace RetroSim
{
    public class SimulationBase
    {
        private Tilemap mTilemap;

        public SimulationBase(Tilemap map)
        {
            mTilemap = map;
        }

        protected bool TryGoDown(TileBase tile, Vector3Int currentPos)
        {
            var targetPos = currentPos + Vector3Int.down;

            return TryGotoPos(tile, currentPos, targetPos);
        }

        protected bool TryGoLeft(TileBase tile, Vector3Int currentPos)
        {
            var targetPos = currentPos + Vector3Int.left;
         
            return TryGotoPos(tile, currentPos, targetPos);
        }

        protected bool TryGoRight(TileBase tile, Vector3Int currentPos)
        {
            var targetPos = currentPos + Vector3Int.right;

            return TryGotoPos(tile, currentPos, targetPos);
        }

        protected bool TryGoDownLeft(TileBase tile, Vector3Int currentPos)
        {
            var targetPos = currentPos + new Vector3Int(-1, -1, 0);

            return TryGotoPos(tile, currentPos, targetPos);
        }

        protected bool TryGoDownRight(TileBase tile, Vector3Int currentPos)
        {
            var targetPos = currentPos + new Vector3Int(1, -1, 0);
            
            return TryGotoPos(tile, currentPos, targetPos);
        }

        private bool TryGotoPos(TileBase tile, Vector3Int currentPos, Vector3Int targetPos) 
        {
            bool result = false;
            var targetTile = mTilemap.GetTile(targetPos);

            if (targetTile == null)
            {
                // Empty tile
                ReplaceTile(tile, currentPos, targetPos);
                result = true;
            }
            else 
            {
                // Check density
                int currentDensity = DensityMap.Map[tile.name];
                int density = DensityMap.Map[targetTile.name];

                if (currentDensity > density) 
                {
                    ReplaceTile(tile, currentPos, targetPos);
                    result = true;
                }
            }

            return result;
        }

        private void ReplaceTile(TileBase tile, Vector3Int oldPos, Vector3Int newPos)
        {
            var targetTile = mTilemap.GetTile(newPos);
            mTilemap.SetTile(newPos, tile);
            mTilemap.SetTile(oldPos, targetTile);
        }
    }
}
