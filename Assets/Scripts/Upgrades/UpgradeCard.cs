using TMPro;
using UnityEngine;

public abstract class UpgradeCard : MonoBehaviour
{
    [SerializeField] protected TMP_Text PriceText;
    [SerializeField] protected CashCounter CashCounter;
    [SerializeField] protected AudioSource Purchase;
    [SerializeField] protected AudioSource NotEnough;

    protected int InternalPrice;

    protected const int SurplusFactor = 20;

    public int Price => InternalPrice;

    public abstract void Upgrade();

    public void SetDefaultValues()
    {
        InternalPrice = 20;
        PriceText.text = InternalPrice.ToString();
    }

    public void SetStartValues(int price)
    {
        InternalPrice = price;
        PriceText.text = InternalPrice.ToString();
    }
}
