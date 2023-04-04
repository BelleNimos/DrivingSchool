using UnityEngine;

public class UpgradeCardBag : UpgradeCard
{
    [SerializeField] private Bag _bag;

    public override void Upgrade()
    {
        if (CashCounter.CountDollars >= InternalPrice)
        {
            Purchase.Play();
            CashCounter.SpendDollars(InternalPrice);
            InternalPrice += SurplusFactor;
            _bag.IncreaseCapacity();
            PriceText.text = InternalPrice.ToString();
        }
        else
        {
            NotEnough.Play();
        }
    }
}
