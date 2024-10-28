using UnityEngine;

namespace JustMobyTest.Commerce.View
{
    internal class PurchaseButtonDrawer : MonoBehaviour
    {
        [SerializeField] private PurchaseButton _purchaseButton;
        [SerializeField] private PurchaseWithDiscountButton _purchaseWithDiscountButton;

        internal void Initialize() 
        {
            _purchaseButton.Initialize();
            _purchaseWithDiscountButton.Initialize();
        }

        internal void Draw(PriceTag priceTag) 
        {
            if (priceTag.DiscountInPercent == 0)
            {
                _purchaseWithDiscountButton.TurnOff();
                _purchaseButton.DrawPrice(priceTag.Price);
            }
            else 
            {
                _purchaseButton.TurnOff();
                _purchaseWithDiscountButton.DrawPrice(priceTag.Price);
                _purchaseWithDiscountButton.DrawOldPriceAndDiscount(priceTag.OldPrice, 
                    priceTag.DiscountInPercent);
            }
        }
    }
}
