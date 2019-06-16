using System.Collections.Generic;
using UnityEngine;

namespace Bubbles.UI {
    [CreateAssetMenu]
    public class SpriteFont : ScriptableObject {
        [SerializeField] private SpriteFontItem [] items;

        private Dictionary<string, Sprite> map;

        public Sprite Get(string s) {
            if (s != null && map.TryGetValue(s, out var result)) {
                return result;
            } else {
                return null;
            }
        }

        
        private void OnEnable() {
            Init();
        }

        private void OnValidate() {
            Init();
        }

        private void Init() {
            map = new Dictionary<string, Sprite>(items.Length);
            foreach (var item in items) {
                map.Add(item.Name, item.Sprite);
            }
        }
        public Sprite this[string s] => Get(s);
    }
    

    [System.Serializable]
    struct SpriteFontItem {
        public string Name;
        public Sprite Sprite;
    }
}
