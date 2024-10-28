using UnityEngine;

namespace JustMobyTest.Commerce.Goods
{
    [CreateAssetMenu(fileName = "NewCommercialItem", menuName = "ProjectData/CommercialItem", order = 52)]
    public class CommercialItem : ScriptableObject
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public int Quantity { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}