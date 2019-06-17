using System.Collections;
using UnityEngine;

namespace Bubbles.Core.Abstract {
    public class BubbleExploder : IBubbleExploder {
        private const int ExplosionRange = 2;
        private IGridWrapper grid;
        private IBubbleSpawner spawner;

        public BubbleExploder(IGridWrapper grid, IBubbleSpawner spawner) {
            this.grid = grid;
            this.spawner = spawner;
        }

        public void Explode(Bubble bubble) {
            var bubbles = grid.BubblesInRange(bubble, ExplosionRange);
            foreach (var bb in bubbles) {
                bb.Movement.Drop();
                bb.StartCoroutine(DelayDespawn(bb, 3f));
            }
            grid.Detach(bubble);
            spawner.Return(bubble);
            Debug.Log("Exploded!");
        }

        IEnumerator DelayDespawn(Bubble bubble, float time) {
            yield return new WaitForSeconds(time);
            grid.Detach(bubble);
            spawner.Return(bubble);
        }
        
        
    }
}