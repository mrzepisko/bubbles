using System.Collections.Generic;
using System.Linq;
using Bubbles.Config;
using Bubbles.Core.Abstract;

namespace Bubbles.Core {
    public class BubbleCollector : IBubbleCollector {
        private readonly IGridWrapper grid;
        private readonly ScoreRange scoreRange;

        public BubbleCollector(IGridWrapper grid, ScoreRange scoreRange) {
            this.grid = grid;
            this.scoreRange = scoreRange;
        }

        public HashSet<Bubble> ScoreNeighbours(Bubble bubble) {
            return ScoreNeighbours(bubble, new HashSet<Bubble>());
        }

        private HashSet<Bubble> ScoreNeighbours(Bubble bubble, HashSet<Bubble> result, IBubbleScore score = null) {
            if (score == null) {
                score = bubble.Score;
            }

            result.Add(bubble);
            Bubble[] scoreNeighbours = grid.Neighbours(bubble)
                .Where(n => n != null && !result.Contains(n) && n.Score.Value.Equals(score.Value)).ToArray();
            foreach (var neighbour in scoreNeighbours) {
                if (result.Contains(neighbour)) continue; // already checked
                foreach (var childNeighbour in ScoreNeighbours(neighbour, result)) {
                    result.Add(childNeighbour);
                }
            }

            return result;
        }


        //TODO
        public Tile SelectBestTile(HashSet<Bubble> toJoin, IBubbleScore score) {
            return BestTile(toJoin, score);
        }

        private Tile BestTile(HashSet<Bubble> toJoin, IBubbleScore score) {
            int bestCount = 0;
            Tile bestTile = null;
            foreach (var bb in toJoin) {
                var newToJoin = new HashSet<Bubble>();
                //candidates with value matching new score
                ScoreNeighbours(bb, newToJoin, score);
                if (newToJoin.Count > bestCount) {
                    bestTile = grid.Get(bb);
                    bestCount = newToJoin.Count;
                }
            }

            return bestTile;
        }
    }
}