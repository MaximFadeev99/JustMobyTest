using JustMobyTest.Settings;
using JustMobyTest.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace JustMobyTest.Commerce.Goods
{
    [CreateAssetMenu(fileName = "NewCommercialBundle", menuName = "ProjectData/CommercialBundle", 
        order = 52)]
    public class CommercialBundle : ScriptableObject
    {
        [SerializeField] private List<CommercialItem> _includedItems; 

        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite MainSprite { get; private set; }
        [field: SerializeField] public float Price { get; private set; }
        [field: SerializeField] public float Discount { get; private set; }

        public IReadOnlyList<CommercialItem> IncludedItems => _includedItems;

        private void OnValidate()
        {
            Discount = Mathf.Clamp01(Discount);

            if (_includedItems.Count > GameSettings.MaxItemsInCommercialBundle || 
                _includedItems.Count < GameSettings.MinItemsInCommercialBundle) 
            {
                CustomLogger.Log($"{nameof(CommercialBundle)} {Name}", "You have included a wrong number " +
                    $"of items in this bundle. The item count should be no less than " +
                    $"{GameSettings.MinItemsInCommercialBundle} and no more than " +
                    $"{GameSettings.MaxItemsInCommercialBundle}", 
                    MessageTypes.Warning);
            }
        }
    }
}