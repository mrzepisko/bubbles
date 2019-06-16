using System.Collections.Generic;

namespace Bubbles.Core.Abstract {
    public interface IBubbleCollector {
        HashSet<Bubble> ScoreNeighbours(Bubble bubble);
        HashSet<Bubble> ScoreNeighbours(Bubble bubble, HashSet<Bubble> result);
        Tile SelectBestTile(HashSet<Bubble> toJoin, IBubbleScore score);
    }
}