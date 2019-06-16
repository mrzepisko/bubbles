using System.Collections.Generic;
using System.Linq;
using Bubbles.Core.Abstract;

namespace Bubbles.Core {
    public class BubbleCollector : IBubbleCollector {
        private IGridWrapper grid;
        private IBubbleSpawner spawner;

        public BubbleCollector(IGridWrapper grid, IBubbleSpawner spawner) {
            this.grid = grid;
            this.spawner = spawner;
        }

        public void Attached(Bubble bubble) {
            var toJoin = ScoreNeighbours(bubble);
            if (toJoin.Count <= 1) return;
            Tile bestTile = SelectBestTile(toJoin);
            IBubbleScore score = CalculateScore(bubble.Score, toJoin.Count);
            
            //detach old bubbles
            foreach (var bb in toJoin) {
                bb.Movement.MoveTowards(bestTile.transform.position);
                grid.Detach(bb);
                spawner.Return(bb);
            }

            //create new bubble 
            var newBubble = spawner.Create(score);
            #if UNITY_EDITOR
            UnityEditor.EditorGUIUtility.PingObject(newBubble.gameObject);
            #endif
            grid.Insert(newBubble, bestTile);
            
            Attached(newBubble);
        }

        private IBubbleScore CalculateScore(IBubbleScore score, int count) {
            return new BubbleScore(score.Value + count - 1);
        }

        private Tile SelectBestTile(HashSet<Bubble> toJoin) { //TODO
            using (var e = toJoin.GetEnumerator()) {
                e.MoveNext();
                e.MoveNext();
                return grid.Get(e.Current);
            }
        }

        HashSet<Bubble> ScoreNeighbours(Bubble bubble) {
            return ScoreNeighbours(bubble, new HashSet<Bubble>());
        }

        HashSet<Bubble> ScoreNeighbours(Bubble bubble, HashSet<Bubble> result) {
            result.Add(bubble);
            Bubble[] scoreNeighbours = grid.Neighbours(bubble)
                .Where(n => n != null && !result.Contains(n) && n.Score.Value.Equals(bubble.Score.Value)).ToArray(); 
            foreach (var neighbour in scoreNeighbours) {
                if (result.Contains(neighbour)) continue; // already checked
                foreach (var childNeighbour in ScoreNeighbours(neighbour, result)) {
                    result.Add(childNeighbour);
                }
            }

            return result;
        }
        
        
    }
}