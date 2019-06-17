using Bubbles.Core.Abstract;
using UnityEngine;

namespace Bubbles.Core {
    public class DataManagerPrefs : IDataManager {
        private const string PrefMultiplier = "multiplier";
        private const string PrefScoreExp = "exponent";
        private const string PrefLevel = "level";
        
        public void Save(PlayerData data) {
            PlayerPrefs.SetInt(PrefMultiplier, data.Multiplier);   
            PlayerPrefs.SetInt(PrefLevel, data.Level);   
            PlayerPrefs.SetString(PrefScoreExp, data.Points.ToString("0"));   
            PlayerPrefs.Save();
        }

        public PlayerData Load() {
            return new PlayerData() {
                Multiplier = PlayerPrefs.GetInt(PrefMultiplier, 1),
                Level = PlayerPrefs.GetInt(PrefLevel, 1),
                Points = long.Parse(PlayerPrefs.GetString(PrefScoreExp, "0")),
            };
        }
    }
}