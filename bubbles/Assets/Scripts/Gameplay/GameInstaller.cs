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
            BindLogic();
            BindFromHierarchy();
        }

        private void BindLogic() {
            Container.Bind<IGridWrapper>()
                .To<GridWrapper>()
                .AsSingle();
            Container.Bind<IBubbleCollector>()
                .To<BubbleCollector>()
                .AsSingle();
            Container.Bind<IScoreManager>()
                .To<ScoreManager>()
                .AsSingle();
            Container.Bind<IBubbleSpawner>()
                .To<BubbleSpawner>()
                .AsSingle();
            Container.Bind<IGridManager>()
                .To<GridManager>()
                .AsSingle();
        }

        private void BindFromHierarchy() {
            Container.Bind<Camera>()
                .FromComponentInHierarchy(false)
                .AsSingle();
            Container.Bind<IBubbleCannon>()
                .To<BubbleCannonQueue>()
                .FromComponentInHierarchy(false)
                .AsSingle();
            Container.Bind<HexGrid>()
                .FromComponentInHierarchy(false)
                .AsSingle();
            Container.Bind<Go>()
                .FromComponentInHierarchy(false)
                .AsSingle();
            Container.Bind<AimManager>()
                .FromComponentInHierarchy(false)
                .WhenInjectedInto<AimVisualizer>();
        }

        private void BindConfigs() {
            Container.Bind<BubbleConfig>()
                .FromInstance(visualConfig)
                .AsSingle();
        }

        private void BindPools() {
            GameObject pool = new GameObject(NameBubblePool);
            pool.transform.position = Vector3.right * 200f;
            Container.BindMemoryPool<Bubble, Bubble.Pool>()
                .WithInitialSize(bubblePoolSize)
                .FromComponentInNewPrefab(bubblePrototype)
                .UnderTransform(pool.transform)
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