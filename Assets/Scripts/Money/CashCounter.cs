using UnityEngine;

public class CashCounter : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private DollarsCountPanel _dollarsCountPanel;
    [SerializeField] private AudioSource _spendMoneySound;

    public int CountDollars => _wallet.CountDollars;
    public int TotalCountDollars => _wallet.TotalCountDollars;

    public void SetDefaultValues()
    {
        _wallet.SetDefaultValues();
        _dollarsCountPanel.SetCountDollars(_wallet.CountDollars);
    }

    public void SetStartValues(int countDollars, int totalCountDollars)
    {
        _wallet.SetStartValues(countDollars, totalCountDollars);
        _dollarsCountPanel.SetCountDollars(_wallet.CountDollars);
    }

    public void SpendDollar()
    {
        _wallet.SpendDollar();
        _dollarsCountPanel.SetCountDollars(_wallet.CountDollars);
    }

    public void AddDollar()
    {
        _wallet.AddDollar();
        _dollarsCountPanel.PlayAnimationAddMoney();
        _dollarsCountPanel.SetCountDollars(_wallet.CountDollars);
    }

    public void AddDollars(int count)
    {
        for (int i = 0; i < count; i++)
            AddDollar();
    }

    public void SpendDollars(int count)
    {
        for (int i = 0; i < count; i++)
            SpendDollar();

        _spendMoneySound.Play();
    }
}
