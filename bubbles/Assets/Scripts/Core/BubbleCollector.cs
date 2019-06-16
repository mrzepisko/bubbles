using System.Collections.Generic;
using System.Linq;
using Bubbles.Core.Abstract;

namespace Bubbles.Core {
    public class BubbleCollector : IBubbleCollector {
        private readonly IGridWrapper grid;

        public BubbleCollector(IGridWrapper grid) {
            this.grid = grid;
        }

        public HashSet<Bubble> ScoreNeighbours(Bubble bubble) {
            return ScoreNeighbours(bubble, new HashSet<Bubble>());
        }

        public HashSet<Bubble> ScoreNeighbours(Bubble bubble, HashSet<Bubble> result) {
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
        
        

        public Tile SelectBestTile(HashSet<Bubble> toJoin, IBubbleScore score) { //TODO
            using (var e = toJoin.GetEnumerator()) {
                e.MoveNext();
                e.MoveNext();
                return grid.Get(e.Current);
            }
        }
    }
}