using System.Collections.Generic;
using Bubbles.Core.Abstract;
using UnityEngine;

namespace Bubbles.Core {
    public class GridManager : IGridManager {
        private const int InitialRows = 4;
        private const int RowWidth = 12;


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

        public void InsertNewRow() {
            var row = PrepareRow();
            grid.MoveRows(1);
                FillRow(0, 0);
                FillRow(0, -1);
        }
        
        private List<Bubble> PrepareRow() {
            List<Bubble> newRow = new List<Bubble>();
            for (int i = 0; i < RowWidth; i++) {
                newRow.Add(spawner.CreateRandom());
            }

            return newRow;
        }
        
    }
}