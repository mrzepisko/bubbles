using System.Collections.Generic;
using Bubbles.Core.Abstract;

namespace Bubbles.Core {

    public class GridWrapper : IGridWrapper {
        private Dictionary<Tile, Bubble> t2b;
        private Dictionary<Bubble, Tile> b2t;

        public Tile Get(Bubble bubble) => Get(b2t, bubble);
        public Bubble Get(Tile tile) => Get(t2b, tile);

        public GridWrapper() {
            t2b = new Dictionary<Tile, Bubble>();
            b2t = new Dictionary<Bubble, Tile>();
        }

        public V Get<K, V>(Dictionary<K, V> from, K key) where K : class where V : class {
            if (key != null && from.TryGetValue(key, out var value)) {
                return value;
            } else {
                return null;
            }
        }


        public bool Insert(Bubble bubble, Tile tile) { //instant
            if (QueryMaps(bubble, tile)) {
                bubble.SetTarget(tile.transform.position, true);
                return true;
            }

            return false;
        }
        public bool Attach(Bubble bubble, Tile tile) { //move towards
            if (QueryMaps(bubble, tile)) {
                bubble.SetTarget(tile.transform.position);
                return true;
            }

            return false;
        }

        private bool QueryMaps(Bubble bubble, Tile tile) {
            if (t2b.ContainsKey(tile)) {
                return false; //cannot insert, tile taken
            }

            //free old tile
            Detach(bubble);

            b2t.Add(bubble, tile);
            t2b.Add(tile, bubble);
            return true;
        }

        public void Detach(Bubble bubble) {
            if (b2t.TryGetValue(bubble, out var tile)) {
                b2t.Remove(bubble);
                t2b.Remove(tile);
            }
        }
    }
}