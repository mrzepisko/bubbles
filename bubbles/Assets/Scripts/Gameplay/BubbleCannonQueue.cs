using System.Collections.Generic;
using Bubbles.Core;
using Bubbles.Core.Abstract;
using UnityEngine;
using Zenject;

namespace Bubbles.Gameplay {
    public class BubbleCannonQueue : MonoBehaviour, IBubbleCannon {
        const int QueueSize = 3;
        
        private IBubbleSpawner bubbleSpawner;

        private Queue<Bubble> queue;
        private Bubble nextBubble;

        [Inject]
        private void Construct(IBubbleSpawner bubbleSpawner) {
            this.bubbleSpawner = bubbleSpawner;
            queue = new Queue<Bubble>(QueueSize);
            Prepare();
        }

        private void OnEnable() {
            LoadCannon();
        }


        private void Prepare() {
            for (int i = queue.Count; i < QueueSize; i++) {
                queue.Enqueue(bubbleSpawner.Create());
            }
        }

        Bubble GetFromQueue() {
            queue.Enqueue(bubbleSpawner.Create());
            return queue.Dequeue();
        }
        
        public void ShootAt(Tile tile) {
            nextBubble.SetTarget(tile);
            LoadCannon();
        }

        public Bubble Peek() {
            return queue.Peek();
        }

        void LoadCannon() {
            nextBubble = GetFromQueue();
            nextBubble.Teleport(transform.position);
        }
    }
}