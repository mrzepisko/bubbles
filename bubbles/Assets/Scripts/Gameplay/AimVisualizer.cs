using Bubbles.Core;
using UnityEngine;

namespace Bubbles.Gameplay {
    public class AimVisualizer : MonoBehaviour {
        [SerializeField] private LineRenderer lineRenderer;

        private IUserInput input;
        
        private void OnEnable() {
            input.ButtonDown += InputOnButtonDown;
            input.ButtonHold += InputOnButtonHold;
            input.ButtonUp += InputOnButtonUp;
        }

        private void InputOnButtonUp(Vector3 position) {
            lineRenderer.enabled = false;
        }

        private void InputOnButtonHold(Vector3 position) {
        }

        private void InputOnButtonDown(Vector3 position) {
            lineRenderer.enabled = true;
        }
    }
}