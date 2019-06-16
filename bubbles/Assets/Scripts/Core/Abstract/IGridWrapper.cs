using System.Collections.Generic;

namespace Bubbles.Core.Abstract {
    public interface IGridWrapper {
        Tile Get(Bubble bubble);
        Bubble Get(Tile tile);
        bool Attach(Bubble bubble, Tile tile);
        bool Insert(Bubble bubble, Tile tile);
        void Detach(Bubble bubble);
        Tile TileAt(int x, int z);
        List<Tile> Neighbours(Tile tile);
        Tile Neighbour(Tile tile, HexDirection direction);
        List<Tile> TilesInRange(Tile center, int range);
        
        List<Bubble> Neighbours(Bubble bubble);
    }
}