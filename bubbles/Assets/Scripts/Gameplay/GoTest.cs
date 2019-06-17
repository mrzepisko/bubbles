using Bubbles.Core;
using Bubbles.Core.Abstract;
using UnityEngine;
using Zenject;

namespace Bubbles.Gameplay {
    public class GoTest : MonoBehaviour {
        private IGridManager grid;

        private void Awake() {
            grid = GetComponentInChildren<GridManagerTest>();
        }

        private void Start() {
            grid.FillGrid();
        }
    }
}