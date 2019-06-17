using System.Collections.Generic;
using UnityEngine;

namespace Bubbles.UI {
    [CreateAssetMenu]
    public class SpriteFont : ScriptableObject {
        [SerializeField] private SpriteFontItem [] items;

        private Dictionary<string, SpriteFontItem> map;

        public SpriteFontItem Get(string s) {
            if (s != null && map.TryGetValue(s, out var result)) {
                return result;
            } else {
                return default;
            }
        }

        
        private void OnEnable() {
            Init();
        }

        private void OnValidate() {
            Init();
        }

        private void Init() {
            map = new Dictionary<string, SpriteFontItem>(items.Length);
            foreach (var item in items) {
                map.Add(item.Name, item);
            }
        }
        public SpriteFontItem this[string s] => Get(s);
    }
    

    [System.Serializable]
    public struct SpriteFontItem {
        public string Name;
        public Sprite Sprite;
        public float Width => Sprite ? (Sprite.rect.width / Sprite.pixelsPerUnit) : 0f;
    }
}
