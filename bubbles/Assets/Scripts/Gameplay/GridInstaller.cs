using Bubbles.Config;
using Bubbles.Core;
using UnityEngine;
using Zenject;

namespace Bubbles.Gameplay {
    public class GridInstaller : MonoInstaller<GridInstaller> {
        private const string NameBubblePool = "__pool_bubbles";
        [SerializeField] private HexGrid hexGrid;
        [SerializeField] private int bubblePoolSize = 50;
        [SerializeField] private BubbleConfig visualConfig;
        [SerializeField] private Bubble bubblePrototype;
        
        
        public override void InstallBindings() {
            Container.Bind<HexGrid>()
                .FromInstance(hexGrid)
                .AsSingle();
            Container.BindMemoryPool<Bubble, Bubble.Pool>()
                .WithInitialSize(bubblePoolSize)
                .FromComponentInNewPrefab(bubblePrototype)
                .UnderTransformGroup(NameBubblePool)
                .AsSingle();
            #if UNITY_ANDROID && !UNITY_EDITOR
            Container.BindInterfacesTo<UserInputTouch>()
                .AsSingle();
            #else
            Container.BindInterfacesTo<UserInputMouse>()
                .AsSingle();
            #endif
            Container.Bind<BubbleCannon>().FromComponentInHierarchy(false).AsSingle();
        }
    }
}