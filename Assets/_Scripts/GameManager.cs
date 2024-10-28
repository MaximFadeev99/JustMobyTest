using JustMobyTest.Commerce;
using JustMobyTest.Commerce.View;
using UnityEngine;
using Zenject;

namespace JustMobyTest.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Shop _shop;
        [SerializeField] private ShopController _shopController;

        private IShopView _shopView;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus, IShopView shopView) 
        {
            _signalBus = signalBus;
            _shopView = shopView;
        }

        private void Awake()
        {
            _shop.Initialize(_signalBus, _shopView);
            _shopController.Initialize(_signalBus);
        }

        private void OnDestroy()
        {
            _shop.Dispose();
            _shopController.Dispose();
        }
    }
}