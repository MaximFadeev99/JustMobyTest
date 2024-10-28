using JustMobyTest.Commerce.Goods;
using System.Collections.Generic;
using UnityEngine;

namespace JustMobyTest.Commerce.View 
{
    public interface IShopView
    {
        public void Initialize();
        public void DrawAssortment(IReadOnlyList<CommercialBundle> availableBundles);
        public void ClearAssortment();
        public void DrawCurrentCount(int currentCount);
        public void DrawBundleWindow(int chosenCount, string header, string description, Sprite mainSprite,
            IReadOnlyList<CommercialItem> itemsInBundle, PriceTag priceTag);
        public void ReturnToAssortment();
    }
}