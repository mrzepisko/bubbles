using UnityEngine;

namespace Bubbles.Config {
    [CreateAssetMenu]
    public class BubbleConfig : ScriptableObject {
        [Tooltip("Max exponent difference between bubble values")]
        [SerializeField] private int maxDistance = 10;
        [SerializeField] private Item[] items;

        public int MaxDistance => maxDistance;

        public Item Get(int exponent) {
            return items[exponent % 10];
        }

        [System.Serializable]
        public struct Item {
            [SerializeField] private Color background;

            public Color Background => background;
        }
    }
}