using JustMobyTest.Commerce.Goods;
using System.Collections.Generic;
using UnityEngine;

namespace JustMobyTest.Commerce.View 
{
    public class BasicShopView : MonoBehaviour, IShopView
    {
        [SerializeField] private AssortmentWindow _assortmentWindow;
        [SerializeField] private BundleWindow _bundleWindow;

        void IShopView.ClearAssortment()
        {
            _assortmentWindow.Clear();
            _assortmentWindow.SetActive(false);
        }

        void IShopView.DrawAssortment(IReadOnlyList<CommercialBundle> availableBundles)
        {
            _assortmentWindow.Draw(availableBundles);
            _assortmentWindow.SetActive(true);
        }

        void IShopView.DrawBundleWindow(int chosenCount, string header, string description, Sprite mainSprite,
            IReadOnlyList<CommercialItem> itemsInBundle, PriceTag priceTag)
        {
            _assortmentWindow.SetActive(false);
            _bundleWindow.Draw(header, description, mainSprite, itemsInBundle, chosenCount, priceTag);
        }

        void IShopView.DrawCurrentCount(int currentCount)
        {
            _assortmentWindow.DrawCountField(currentCount);
        }

        void IShopView.Initialize()
        {
            _assortmentWindow.Initialize();
            _bundleWindow.Initialize();
        }

        void IShopView.ReturnToAssortment()
        {
            _bundleWindow.TurnOff();
            _assortmentWindow.SetActive(true);
        }
    }
}