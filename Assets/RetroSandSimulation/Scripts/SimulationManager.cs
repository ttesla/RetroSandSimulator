using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace RetroSim 
{
    public class SimulationManager : MonoBehaviour
    {
        public static SimulationManager Instance;

        public Tilemap Map;

        [Range(0.01f, 1.0f)]
        public float SimulationStepDelay;

        [Range(0.01f, 1.0f)]
        public float TileSpawnDelay;

        private int MaxX;
        private int MaxY;
        private int StartX;
        private int StartY;
     
        private SandSimulation mSandSimulation;
        private WaterSimulation mWaterSimulation;
        private bool mEnableTileFlow;
        private Tile mCurrentSelectedFlowTile;
        private Vector3Int mCurrentFlowCellPosition;
        private Vector3Int mPreviousCellPosition;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            mSandSimulation = new SandSimulation(Map);
            mWaterSimulation = new WaterSimulation(Map);

            StartCoroutine(SimulationRoutine());
            StartCoroutine(TileSpawnRoutine());
        }

        private void Update()
        {
            mEnableTileFlow = false;

            if (Input.GetMouseButton(0)) 
            {
                SetTileFlowPositionWithMousePos();
                mEnableTileFlow = true;
            }
        }

        public void SetCurrentFlowTile(Tile tile) 
        {
            mCurrentSelectedFlowTile = tile;
        }

        private void SetTileFlowPositionWithMousePos() 
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            mCurrentFlowCellPosition = Map.WorldToCell(mouseWorldPos);

            if(mPreviousCellPosition != mCurrentFlowCellPosition) 
            {
                Log("Cell Pos: " + mCurrentFlowCellPosition);
                mPreviousCellPosition = mCurrentFlowCellPosition;
            }
        }

        private IEnumerator SimulationRoutine() 
        {
            while (true)
            {
                RunSimulationStep();
                yield return new WaitForSeconds(SimulationStepDelay);
            }
        }

        private IEnumerator TileSpawnRoutine() 
        {
            while (true)
            {
                if (mEnableTileFlow) 
                {
                    Map.SetTile(mCurrentFlowCellPosition, mCurrentSelectedFlowTile);
                }

                yield return new WaitForSeconds(TileSpawnDelay);
            }
        }

        private void RunSimulationStep() 
        {
            MaxX = Map.size.x / 2;
            MaxY = Map.size.y / 2;

            StartX = -MaxX;
            StartY = -MaxY;

            for (int y = StartY; y < MaxY; y++)
            {
                for (int x = StartX; x < MaxX; x++)
                {
                    var currentPos = new Vector3Int(x, y);
                    var currentTile = Map.GetTile(currentPos);
                    
                    if (currentTile != null) 
                    {
                        switch (currentTile.name) 
                        {
                            case SandSimulation.TileTag:
                                mSandSimulation.ApplySimulationRules(currentTile, currentPos);
                                break;
                            case WaterSimulation.TileTag:
                                mWaterSimulation.ApplySimulationRules(currentTile, currentPos);
                                break;
                        }
                    }
                }
            }
        }

        [Conditional("UNITY_EDITOR")]
        private void Log(string message)
        {
            UnityEngine.Debug.Log(message);
        } 
    }
}
