using Bubbles.Core;
using Bubbles.Core.Abstract;
using UnityEngine;
using Zenject;

namespace Bubbles.Gameplay {
    public class AimVisualizer : MonoBehaviour {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private SpriteRenderer bubble;
        [SerializeField] private float bubbleAlpha = .5f;

        private IBubbleCannon cannon;
        private AimManager aimManager;
        private IUserInput input;
        private float PredictionSpeed;

        private void OnEnable() {
            input.ButtonDown += InputOnButtonDown;
            input.ButtonHold += InputOnButtonHold;
            input.ButtonUp += InputOnButtonUp;
        }

        private void InputOnButtonDown(Vector3 obj) {
            Show();
        }

        private void InputOnButtonHold(Vector3 obj) {
            Refresh();
        }

        private void InputOnButtonUp(Vector3 obj) {
            Hide();
        }

        [Inject]
        private void Construct(AimManager aimManager, IUserInput input, IBubbleCannon cannon) {
            this.aimManager = aimManager;
            this.input = input;
            this.cannon = cannon;
        }

        public void Show() {
            lineRenderer.gameObject.SetActive(true);
            bubble.transform.position = aimManager.FutureTile.transform.position;
            bubble.gameObject.SetActive(true);
        }

        public void Hide() {
            lineRenderer.gameObject.SetActive(false);
            bubble.gameObject.SetActive(false);
        }

        public void Refresh() {
            lineRenderer.SetPositions(aimManager.Steps);
            var color = cannon.Peek().Config.Background;
            color.a = bubbleAlpha;
            bubble.color = color;

        }

        private void Update() {
            if (bubble.enabled && aimManager.FutureTile) {
                PredictionSpeed = 50f;
                bubble.transform.position = Vector3.MoveTowards(bubble.transform.position,
                    aimManager.FutureTile.transform.position, Time.deltaTime * PredictionSpeed);
            }
        }
    }
}