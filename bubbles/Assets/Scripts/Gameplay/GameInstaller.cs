using Bubbles.Config;
using Bubbles.Core;
using Bubbles.Core.Abstract;
using UnityEngine;
using Zenject;

namespace Bubbles.Gameplay {
    public class GameInstaller : MonoInstaller<GameInstaller> {
        private const string NameBubblePool = "__pool_bubbles";
        [SerializeField] private int bubblePoolSize = 50, fakeBubblePoolSize = 20;
        [SerializeField] private BubbleConfig visualConfig;
        [SerializeField] private Bubble bubblePrototype;
        [SerializeField] private FakeBubble fakeBubblePrototype;
        [SerializeField] private int startingExponent = 2, maxDistance = 8;
        
        
        public override void InstallBindings() {
            BindConfigs();
            BindControls();
            BindPools();
            BindLogic();
            BindFromHierarchy();
        }

        private void BindLogic() {
            Container.Bind<IGridWrapper>()
                .To<GridWrapper>()
                .AsSingle();
            Container.Bind<IScoreCalculator>()
                .To<ScoreCalculator>()
                .AsSingle();
            Container.Bind<IBubbleCollector>()
                .To<BubbleCollector>()
                .AsSingle();
            Container.Bind<IBubbleExploder>()
                .To<BubbleExploder>()
                .AsSingle();
            Container.Bind<IScoreManager>()
                .To<ScoreManager>()
                .AsSingle();
            Container.Bind<IBubbleSpawner>()
                .To<BubbleSpawner>()
                .AsSingle();
            Container.Bind<IGridManager>()
                .To<GridManager>()
                .AsSingle()
                .WhenInjectedInto(typeof(Go), typeof(ScoreManager));
            Container.Bind<IGridManager>()
                .To<GridManagerTest>()
                .AsSingle()
                .WhenInjectedInto<GoTest>();
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
            Container.Bind<AimManager>()
                .FromComponentInHierarchy(false)
                .WhenInjectedInto<AimVisualizer>();
            Container.Bind<Go>()
                .FromComponentInHierarchy(false)
                .AsSingle();
        }

        private void BindConfigs() {
            Container.Bind<BubbleConfig>()
                .FromInstance(visualConfig)
                .AsSingle();
            Container.Bind<ScoreRange>()
                .FromNew()
                .AsSingle()
                .WithArguments(startingExponent, maxDistance);
        }

        private void BindPools() {
            GameObject pool = new GameObject(NameBubblePool); 
            pool.transform.position = Vector3.right * 200f;
            Container.BindMemoryPool<Bubble, Bubble.Pool>()
                .WithInitialSize(bubblePoolSize)
                .FromComponentInNewPrefab(bubblePrototype)
                .UnderTransform(pool.transform)
                .AsSingle();
//            GameObject pool2 = new GameObject(NameBubblePool);
//            pool2.transform.position = pool.transform.position;
//            Container.BindMemoryPool<FakeBubble, FakeBubble.Pool>()
//                .WithInitialSize(fakeBubblePoolSize)
//                .FromComponentInNewPrefab(fakeBubblePrototype)
//                .UnderTransform(pool2.transform)
//                .AsSingle().WithConcreteId("#fakeBubbles");
//            Container.BindMemoryPool<Dupa, Dupa.Pool>()
//                .AsSingle();
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