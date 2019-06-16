using Bubbles.Core.Abstract;
using Zenject;

namespace Bubbles.Core {
    public class BubbleInstaller : MonoInstaller<BubbleInstaller> {
        
        private IBubbleMovement movement;
        private IBubbleCollector collector;
        private IBubbleAnimator animator;
        private IBubbleView view;
        
        public override void InstallBindings() {
            Container.Bind<IBubbleMovement>().FromComponentInChildren(false).AsSingle();
            Container.Bind<IBubbleCollector>().FromInstance(null).AsSingle(); //TODO
            Container.Bind<IBubbleAnimator>().FromComponentInChildren(false).AsSingle();
            Container.Bind<IBubbleView>().FromComponentInChildren(false).AsSingle();
        }
    }
}