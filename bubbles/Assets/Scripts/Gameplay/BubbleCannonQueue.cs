using System.Collections.Generic;
using Bubbles.Core;
using Bubbles.Core.Abstract;
using UnityEngine;
using Zenject;

namespace Bubbles.Gameplay {
    public class BubbleCannonQueue : MonoBehaviour, IBubbleCannon {
        const int QueueSize = 3;
        [SerializeField] private Transform queueHook;
        
        private IBubbleSpawner bubbleSpawner;
        private IGridWrapper grid;

        private Queue<Bubble> queue;
        private Bubble nextBubble;

        [Inject]
        private void Construct(IBubbleSpawner bubbleSpawner, IGridWrapper grid) {
            this.bubbleSpawner = bubbleSpawner;
            this.grid = grid;
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
            if (grid.Attach(nextBubble, tile)) {
                LoadCannon();   
            }
        }

        public Bubble LoadedBubble() {
            return nextBubble;
        }

        void LoadCannon() {
            var fromQueue = GetFromQueue();
            if (nextBubble == null) {
                fromQueue.Movement.Teleport(transform.position); //first bubble
            } else {
                fromQueue.Animator.LoadedOnCannon();
            }
            nextBubble = fromQueue;
            nextBubble.Movement.MoveTowards(transform.position);
            var nextFromQueue = queue.Peek();
            nextFromQueue.Movement.Teleport(queueHook.position);
            nextFromQueue.Animator.EnterQueue();
        }
    }
}