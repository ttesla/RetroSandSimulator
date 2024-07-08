using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace RetroSim
{
    public class MenuUi : MonoBehaviour
    {
        [Header("UI Buttons")]
        public Button SandButton;
        public Button WaterButton;
        public Button RockButton;
        public Button EmptyButton;
        public Button ResetButton;

        [Header("Tiles")]
        public Tile SandTile;
        public Tile WaterTile;
        public Tile RockTile;

        private void Awake() 
        {
            SandButton.onClick.AddListener(() =>  { SetCurrentFlowTile(SandTile);  });
            WaterButton.onClick.AddListener(() => { SetCurrentFlowTile(WaterTile); });
            RockButton.onClick.AddListener(() =>  { SetCurrentFlowTile(RockTile);  });
            EmptyButton.onClick.AddListener(() => { SetCurrentFlowTile(null);      });
            ResetButton.onClick.AddListener(OnResetClicked);
        }

        private void Update()
        {
            InputUpdate();
        }

        private void InputUpdate() 
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetCurrentFlowTile(SandTile);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetCurrentFlowTile(WaterTile);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetCurrentFlowTile(RockTile);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SetCurrentFlowTile(null);
            }
        }

        private void SetCurrentFlowTile(Tile tile) 
        {
            SimulationManager.Instance.SetCurrentFlowTile(tile);
        }

        private void OnResetClicked() 
        {
            SceneManager.LoadScene(0);
        }
    }
}
