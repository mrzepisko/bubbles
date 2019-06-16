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
        private IScoreManager scoreManager;

        private Queue<Bubble> queue;
        private Bubble nextBubble;

        [Inject]
        private void Construct(IBubbleSpawner bubbleSpawner, IGridWrapper grid, IScoreManager scoreManager) {
            this.bubbleSpawner = bubbleSpawner;
            this.grid = grid;
            this.scoreManager = scoreManager;
            queue = new Queue<Bubble>(QueueSize);
            Prepare();
        }

        private void OnEnable() {
            LoadCannon();
        }


        private void Prepare() {
            for (int i = queue.Count; i < QueueSize; i++) {
                queue.Enqueue(bubbleSpawner.CreateRandom());
            }
        }

        Bubble GetFromQueue() {
            queue.Enqueue(bubbleSpawner.CreateRandom());
            return queue.Dequeue();
        }
        
        public void ShootAt(Tile tile) {
            if (grid.Attach(nextBubble, tile)) {
                scoreManager.Attached(nextBubble);
                LoadCannon();   
            }
        }

        public Bubble LoadedBubble() {
            return nextBubble;
        }

        void LoadCannon() {
            var fromQueue = GetFromQueue();
            if (nextBubble != null) {
                fromQueue.Animator.LoadedOnCannon();
            }

            nextBubble = fromQueue;
            nextBubble.Movement.MoveTowards(queueHook.position, transform.position);
            var nextFromQueue = queue.Peek();
            nextFromQueue.Movement.Teleport(queueHook.position);
            nextFromQueue.Animator.EnterQueue();
        }
    }
}