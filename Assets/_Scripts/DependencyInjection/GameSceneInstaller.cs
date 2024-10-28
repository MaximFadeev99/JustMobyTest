using JustMobyTest.Commerce.View;
using JustMobyTest.Signals;
using UnityEngine;
using Zenject;

namespace JustMobyTest.DI
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private BasicShopView _basicShopView;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.Bind<IShopView>().FromInstance(_basicShopView).AsSingle();

            DeclareShopRelatedSignals();
        }

        private void DeclareShopRelatedSignals() 
        {
            Container.DeclareSignal<EnteringShopSignal>().OptionalSubscriber();
            Container.DeclareSignal<ExitingShopSignal>().OptionalSubscriber();
            Container.DeclareSignal<IncreaseCountSignal>().OptionalSubscriber();
            Container.DeclareSignal<DecreaseCountSignal>().OptionalSubscriber();
            Container.DeclareSignal<ContinueSignal>().OptionalSubscriber();
            Container.DeclareSignal<ReturnSignal>().OptionalSubscriber();
            Container.DeclareSignal<PurchaseSignal>().OptionalSubscriber();
        }
    }
}