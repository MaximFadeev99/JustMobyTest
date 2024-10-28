using System.Globalization;
using TMPro;
using UnityEngine;

namespace JustMobyTest.Commerce.View
{
    internal class PurchaseWithDiscountButton : PurchaseButton
    {
        [SerializeField] private TMP_Text _oldPriceField;
        [SerializeField] private TMP_Text _discountField;

        internal void DrawOldPriceAndDiscount(float oldPrice, int discountInPercent) 
        {
            _oldPriceField.text = $"{oldPrice.ToString("C", CultureInfo.CurrentCulture)}";
            _discountField.text = $"{discountInPercent}%";
        }
    }
}
