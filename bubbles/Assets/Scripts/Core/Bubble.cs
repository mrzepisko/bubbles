using UnityEngine;

namespace Bubbles.Core {
    public class Bubble : MonoBehaviour {
        [SerializeField] private IBubbleMovement movement;
        [SerializeField] private IBubbleScore score;
        [SerializeField] private IBubbleCollector collector;
    }
}