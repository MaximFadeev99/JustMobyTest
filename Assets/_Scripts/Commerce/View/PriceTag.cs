namespace JustMobyTest.Commerce.View
{
    public readonly struct PriceTag
    {
        public readonly float Price;
        public readonly int DiscountInPercent;
        public readonly float OldPrice;

        public PriceTag(float price, int discountInPercent, float oldPrice)
        {
            Price = price;
            DiscountInPercent = discountInPercent;
            OldPrice = oldPrice;
        }
    }
}
