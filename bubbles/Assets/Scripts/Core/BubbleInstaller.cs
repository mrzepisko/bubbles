using Bubbles.Core.Abstract;
using Zenject;

namespace Bubbles.Core {
    public class BubbleInstaller : MonoInstaller<BubbleInstaller> {
        
        public override void InstallBindings() {
            Container.Bind<IBubbleMovement>().FromComponentInChildren(false).AsSingle();
            Container.Bind<IBubbleAnimator>().FromComponentInChildren(false).AsSingle();
            Container.Bind<IBubbleView>().FromComponentInChildren(false).AsSingle();
        }
    }
}