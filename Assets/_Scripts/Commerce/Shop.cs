using JustMobyTest.Commerce.Goods;
using JustMobyTest.Commerce.View;
using JustMobyTest.Settings;
using JustMobyTest.Signals;
using JustMobyTest.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace JustMobyTest.Commerce
{
    [Serializable]
    public class Shop
    {
        [SerializeField] private List<CommercialBundle> _availableBundles;
        
        private SignalBus _signalBus;
        private IShopView _shopView;

        private CommercialBundle _chosenBundle;
        private int _currentCount;

        public void Initialize(SignalBus signalBus, IShopView shopView) 
        {
            _signalBus = signalBus;
            _shopView = shopView;

            _shopView.Initialize();

            _signalBus.Subscribe<EnteringShopSignal>(OnEnteringShop);
            _signalBus.Subscribe<ExitingShopSignal>(OnExitingShop);
            _signalBus.Subscribe<IncreaseCountSignal>(OnIncreasingCount);
            _signalBus.Subscribe<DecreaseCountSignal>(OnDescreasingCount);
            _signalBus.Subscribe<ContinueSignal>(OnContinuing);
            _signalBus.Subscribe<ReturnSignal>(OnReturning);
            _signalBus.Subscribe<PurchaseSignal>(OnPurchasing);
        }

        public void Dispose() 
        {
            _signalBus.TryUnsubscribe<EnteringShopSignal>(OnEnteringShop);
            _signalBus.TryUnsubscribe<ExitingShopSignal>(OnExitingShop);
            _signalBus.TryUnsubscribe<IncreaseCountSignal>(OnIncreasingCount);
            _signalBus.TryUnsubscribe<DecreaseCountSignal>(OnDescreasingCount);
            _signalBus.TryUnsubscribe<ContinueSignal>(OnContinuing);
            _signalBus.TryUnsubscribe<ReturnSignal>(OnReturning);
            _signalBus.TryUnsubscribe<PurchaseSignal>(OnPurchasing);
        }

        private void OnEnteringShop(EnteringShopSignal _) 
        {
            _chosenBundle = null;
            _currentCount = 1;
            _shopView.DrawCurrentCount(_currentCount);
            _shopView.DrawAssortment(_availableBundles);
        }

        private void OnExitingShop(ExitingShopSignal _) 
        {
            _shopView.ClearAssortment();
        }

        private void OnIncreasingCount(IncreaseCountSignal _) 
        {
            if (_currentCount >= GameSettings.MaxPurchaseCount)
                return;

            _shopView.DrawCurrentCount(++_currentCount);
        }

        private void OnDescreasingCount(DecreaseCountSignal _)
        {
            if (_currentCount <= GameSettings.MinPurchaseCount)
                return;

            _shopView.DrawCurrentCount(--_currentCount);
        }

        private void OnContinuing(ContinueSignal continueSignal) 
        {
            _chosenBundle = _availableBundles.FirstOrDefault
                (bundle => bundle.Name == continueSignal.CommercialBundleName);

            if (_chosenBundle == null) 
            {
                CustomLogger.Log(nameof(Shop), $"The name of {nameof(CommercialBundle)} " +
                    $"<{continueSignal.CommercialBundleName}> does not exist among registered {_availableBundles}." +
                    $"You are trying to continue the purchase of an unknown item. This is not allowed, " +
                    $"the purchase is terminated", MessageTypes.Error);

                return;
            }

            PriceTag priceTag = CompilePriceTag();

            _shopView.DrawBundleWindow(_currentCount, _chosenBundle.Name, _chosenBundle.Description, 
                _chosenBundle.MainSprite, _chosenBundle.IncludedItems, priceTag);
        }

        private void OnReturning(ReturnSignal _) 
        {
            _shopView.ReturnToAssortment();
        }

        private void OnPurchasing(PurchaseSignal _) 
        {
            if (_chosenBundle == null) 
            {
                CustomLogger.Log(nameof(Shop), $"There is no {nameof(CommercialBundle)} chosen for " +
                    $"purchase. This is not allowed, you must choose a {nameof(CommercialBundle)} first. " +
                    $"The purchase is terminated", MessageTypes.Error);

                return;
            }

            float deductedAmount = _chosenBundle.Discount == 0f ? _chosenBundle.Price :
                _chosenBundle.Price * _chosenBundle.Discount;
            deductedAmount *= _currentCount;

            CustomLogger.Log(nameof(Shop), $"You are purchasing {_currentCount} of {_chosenBundle.Name} " +
                $"for ${deductedAmount}", MessageTypes.Message);
            
            //Process the payment
            //Add all the items in the bundle to the user's account
        }

        private PriceTag CompilePriceTag() 
        {
            if (_chosenBundle.Discount == 0f)
            {
                return new PriceTag(_chosenBundle.Price * _currentCount, 0, 0f);
            }
            else 
            {
                float priceWithDiscount = 
                    _chosenBundle.Price - _chosenBundle.Price * _chosenBundle.Discount;
                int discountInPercents = Convert.ToInt32(_chosenBundle.Discount * 100);

                return new PriceTag(priceWithDiscount * _currentCount, discountInPercents, 
                    _chosenBundle.Price * _currentCount);
            }        
        }
    }
}
