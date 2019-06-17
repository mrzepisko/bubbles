using System.Collections.Generic;
using Bubbles.Core;
using Bubbles.Core.Abstract;
using UnityEngine;
using Zenject;

namespace Bubbles.Gameplay {
    [ExecuteInEditMode]
    public class GridManagerTest : MonoBehaviour, IGridManager {
        private const int InitialRows = 4;
        [SerializeField] private int[] values;

        private IBubbleSpawner spawner;
        private IGridWrapper grid;

        [Inject]
        private void Construct(IBubbleSpawner spawner, IGridWrapper grid) {
            this.spawner = spawner;
            this.grid = grid;
        }

        #if UNITY_EDITOR
        

        private void OnEnable() {
            if (Application.isPlaying) return;
            for (int i = 0; i < transform.parent.childCount; i++) {
                var tr = transform.parent.GetChild(i);
                if (tr == transform) continue;
                tr.gameObject.SetActive(false);
            }
            gameObject.SetActive(true);
            transform.SetAsFirstSibling();
        }
        #endif

        public void FillGrid() {
            int i = 0;
            for (int r = 0; i < values.Length; r--) {
                FillRow(r, r, ref i);
                FillRow(r, r - 1, ref i);
            }
        }
        
        public void InsertNewRow() {
            var row = PrepareRow();
            grid.MoveRows(1);
        }
        
        private List<Bubble> PrepareRow() {
            List<Bubble> newRow = new List<Bubble>();
            for (int i = 0; i < 12; i++) {
                newRow.Add(spawner.CreateRandom());
            }

            return newRow;
        }

        private void FillRow(int q, int r, ref int exponent) {
            Tile tile = grid.TileAt(q, r);
            while (tile != null) {
                if (exponent >= values.Length) return;
                var value = values[exponent++];
                if (value > 0) {
                    var bubble = spawner.Create(new BubbleScore(value));
                    grid.Insert(bubble, tile);
                }
                tile = grid.Neighbour(tile, HexDirection.E);
            }
        }
    }
}