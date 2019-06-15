using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Bubbles.Core {
    public class BubbleCannon : MonoBehaviour {
        [SerializeField] private Transform shootPoint;
        [SerializeField] private int queueSize = 3;
        
        private Bubble.Pool bubblePool;
        private IUserInput input;

        private Queue<Bubble> queue;

        private void Awake() {
            queue = new Queue<Bubble>(queueSize);
        }

        private void OnEnable() {
            input.ButtonUp += InputOnButtonUp;
        }

        private void OnDisable() {
            input.ButtonUp -= InputOnButtonUp;
        }
        
        [Inject]
        private void Construct(IUserInput input, Bubble.Pool bubblePool) {
            this.input = input;
            this.bubblePool = bubblePool;
        }

        private void InputOnButtonUp(Vector3 position) {
            var direction = (position - shootPoint.position).normalized;
            ShootAt(direction);
        }

        public BubbleCannon(Bubble.Pool bubblePool) {
            this.bubblePool = bubblePool;
        }

        public void Prepare() {
            for (int i = queue.Count; i < queueSize; i++) {
                queue.Enqueue(bubblePool.Spawn());
            }
        }
        
        public void ShootAt(Vector3 direction) {
            
            var bubble = bubblePool.Spawn();
            bubble.transform.position = shootPoint.position;
            bubble.SetTarget(null);
        }

        void VectorToCubeDirection(Vector3 direction) {
        }
    }
}