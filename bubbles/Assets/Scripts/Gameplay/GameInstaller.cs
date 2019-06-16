using Bubbles.Config;
using Bubbles.Core;
using Bubbles.Core.Abstract;
using UnityEngine;
using Zenject;

namespace Bubbles.Gameplay {
    public class GameInstaller : MonoInstaller<GameInstaller> {
        private const string NameBubblePool = "__pool_bubbles";
        [SerializeField] private int bubblePoolSize = 50;
        [SerializeField] private BubbleConfig visualConfig;
        [SerializeField] private Bubble bubblePrototype;
        
        
        public override void InstallBindings() {
            BindControls();
            BindPools();
            BindConfigs();
            
            Container.Bind<IScoreManager>()
                .To<ScoreManager>()
                .AsSingle();
            Container.Bind<IBubbleSpawner>()
                .To<BubbleSpawner>()
                .AsSingle();
            Container.Bind<IGridManager>()
                .To<GridManager>()
                .AsSingle();
            Container.Bind<IBubbleCannon>()
                .To<BubbleCannon>()
                .AsSingle();

            Container.Bind<AimManager>()
                .FromComponentInHierarchy(false)
                .WhenInjectedInto<AimVisualizer>();
            
            BindFromHierarchy();
        }

        private void BindFromHierarchy() {
            Container.Bind<Camera>()
                .FromComponentInHierarchy(false)
                .AsSingle();
            Container.Bind<HexGrid>()
                .FromComponentInHierarchy(false)
                .AsSingle();
            Container.Bind<Go>()
                .FromComponentInHierarchy(false)
                .AsSingle();
        }

        private void BindConfigs() {
            Container.Bind<BubbleConfig>()
                .FromInstance(visualConfig)
                .AsSingle();
        }

        private void BindPools() {
            Container.BindMemoryPool<Bubble, Bubble.Pool>()
                .WithInitialSize(bubblePoolSize)
                .FromComponentInNewPrefab(bubblePrototype)
                .UnderTransformGroup(NameBubblePool)
                .AsSingle();
        }

        private void BindControls() {
            #if UNITY_ANDROID && !UNITY_EDITOR
            Container.BindInterfacesTo<UserInputTouch>()
                .AsSingle();
            #else
            Container.BindInterfacesTo<UserInputMouse>()
                .AsSingle();
            #endif
        }
    }
}