using System.Collections.Generic;

namespace Bubbles.Core.Abstract {
    public interface IBubbleCollector {
        HashSet<Bubble> ScoreNeighbours(Bubble bubble);
        Tile SelectBestTile(HashSet<Bubble> toJoin, IBubbleScore score);
    }
}