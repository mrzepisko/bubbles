using System.Collections.Generic;
using Bubbles.Core.Abstract;
using UnityEngine;
using Zenject;

namespace Bubbles.Core {
    public class BubbleCannon : IBubbleCannon {
        const int QueueSize = 3;
        
        private IBubbleSpawner bubbleSpawner;
        private IGridManager grid;

        private Queue<Bubble> queue;

        public BubbleCannon(IBubbleSpawner bubbleSpawner, IGridManager grid) {
            this.bubbleSpawner = bubbleSpawner;
            this.grid = grid;
            queue = new Queue<Bubble>(QueueSize);
            Prepare();
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
            var bubble = GetFromQueue();
            bubble.SetTarget(tile);
        }

        public Bubble Peek() {
            return queue.Peek();
        }
    }
}