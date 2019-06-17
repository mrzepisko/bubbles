using UnityEngine;

namespace Bubbles.Config {

    [System.Serializable]
    public struct BubbleConfigItem {
        [SerializeField] private Color background;

        public Color Background => background;
    }
}