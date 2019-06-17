using UnityEngine;

namespace Bubbles.Config {
    [CreateAssetMenu]
    public class BubbleConfig : ScriptableObject {
        [Tooltip("Max exponent difference between bubble values")]
        [SerializeField] private int maxDistance = 10;
        [SerializeField] private BubbleConfigItem[] items;

        public int MaxDistance => maxDistance;

        public BubbleConfigItem Get(int exponent) {
            return items[exponent % 10];
        }
    }
}