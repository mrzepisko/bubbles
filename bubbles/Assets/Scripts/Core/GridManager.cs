using System.Collections.Generic;
using Bubbles.Core.Abstract;

namespace Bubbles.Core {
    public class GridManager : IGridManager {
        private const int InitialRows = 4;


        private IBubbleSpawner spawner;
        private IGridWrapper grid;

        public GridManager(IBubbleSpawner spawner, IGridWrapper grid) {
            this.spawner = spawner;
            this.grid = grid;
        }

        public void FillGrid() {
            for (int r = 0; r > -InitialRows / 2; r--) {
                FillRow(r, r);
                FillRow(r, r - 1);
            }
        }

        private void FillRow(int q, int r) {
            Tile tile = grid.TileAt(q, r);
            while (tile != null) {
                var bubble = spawner.CreateRandom();
                grid.Insert(bubble, tile);
                tile = grid.Neighbour(tile, HexDirection.E);
            }
        }
    }
}