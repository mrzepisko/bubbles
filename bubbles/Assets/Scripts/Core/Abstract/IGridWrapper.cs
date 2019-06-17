using System.Collections.Generic;
using UnityEngine;

namespace Bubbles.Core.Abstract {
    public interface IGridWrapper {
        Tile Get(Bubble bubble);
        Bubble Get(Tile tile);
        bool Attach(Bubble bubble, Tile tile);
        bool Insert(Bubble bubble, Tile tile);
        void Detach(Bubble bubble);
        Tile TileAt(int x, int z);
        Tile Nearest(Vector3 position);
        List<Bubble> Neighbours(Tile tile);
        Tile Neighbour(Tile tile, HexDirection direction);
        Bubble  Neighbour(Bubble bubble, HexDirection direction);
        List<Tile> TilesInRange(Tile center, int range);
        List<Bubble> BubblesInRange(Bubble center, int range);
        List<Bubble> Neighbours(Bubble bubble);
        void MoveRows(int count);
        List<Tile> Tiles { get; }
        List<Bubble> Bubbles { get; }
        int Rows { get; }
    }
}