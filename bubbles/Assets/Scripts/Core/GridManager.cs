using Bubbles.Core.Abstract;

namespace Bubbles.Core {
    public class GridManager : IGridManager {
        private const int InitialRows = 4;
        private HexGrid grid;
        private IBubbleSpawner spawner;

        public GridManager(HexGrid grid, IBubbleSpawner spawner) {
            this.grid = grid;
            this.spawner = spawner;
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
                var bubble = spawner.Create();
                bubble.SetTarget(tile, true);
                tile = grid.Neighbour(tile, HexDirection.E);
            }
        }
    }
}