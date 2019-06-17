using Bubbles.Config;
using Bubbles.Core.Abstract;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Bubbles.Gameplay {
    public class LevelSlider : MonoBehaviour { //TODO move to UI !!
        [SerializeField] private Slider slider;
        [SerializeField] private float speed = 2f;
        [SerializeField] private Image currentLevel, nextLevel;
        [SerializeField] private TMP_Text currentLevelText, nextLevelText;
        
        private BubbleConfig config;
        private IScoreCalculator scoreCalculations;
        private IDataManager dataManager;

        [Inject]
        private void Construct(BubbleConfig config, IScoreCalculator scoreCalculations, IDataManager dataManager) {
            this.config = config;
            this.scoreCalculations = scoreCalculations;
            this.dataManager = dataManager;
        }

        private void LateUpdate() {
            var data = dataManager.Load(); // FIXME
            slider.value = Mathf.MoveTowards(slider.value, scoreCalculations.LevelProgress, Time.deltaTime * speed);
            currentLevel.color = config.Get(data.Level).Background;
            nextLevel.color = config.Get(data.Level + 1).Background;
            currentLevelText.text = data.Level.ToString("0");
            nextLevelText.text = (data.Level + 1).ToString("0");
        }
    }
}