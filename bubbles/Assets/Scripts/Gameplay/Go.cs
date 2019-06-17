using Bubbles.Core.Abstract;
using UnityEngine;
using Zenject;

namespace Bubbles.Gameplay {
    public class Go : MonoBehaviour {
        private IGridManager grid;

        [Inject]
        private void Construct(IGridManager grid) {
            this.grid = grid;
        }

        private void Start() {
            grid.FillGrid();
        }

        
        
    }
}