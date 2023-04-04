using UnityEngine;

public class UpgradeCardPlayer : UpgradeCard
{
    [SerializeField] private PlayerMovement _playerMovement;

    public override void Upgrade()
    {
        if (CashCounter.CountDollars >= InternalPrice)
        {
            Purchase.Play();
            CashCounter.SpendDollars(InternalPrice);
            InternalPrice += SurplusFactor;
            _playerMovement.IncreaseSpeed();
            PriceText.text = InternalPrice.ToString();
        }
        else
        {
            NotEnough.Play();
        }
    }
}
