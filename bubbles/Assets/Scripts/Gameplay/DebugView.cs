using Bubbles.Core;
using Bubbles.Core.Abstract;
using UnityEngine;
using Zenject;

namespace Bubbles.Gameplay {
    public class DebugView : MonoBehaviour {     
        #if UNITY_EDITOR
        [Inject] private IGridWrapper grid;

        private void OnDrawGizmos() {
            if (Application.isPlaying) {
                foreach (var bubble in grid.Bubbles) {
                    var tile = grid.Get(bubble);
                    DrawTile(tile);
                    DrawBubble(bubble);
                    DrawConnection(tile, bubble);
                }
            }
        }

        private void DrawConnection(Tile tile, Bubble bubble) {
            Gizmos.color = Color.yellow;
           Gizmos.DrawLine(tile.transform.position, bubble.transform.position);
        }

        private void DrawBubble(Bubble bubble) {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(bubble.transform.position, .5f);
        }

        private void DrawTile(Tile tile) {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(tile.transform.position, Vector3.one * 1.7f);
        }
        #endif
    }
}