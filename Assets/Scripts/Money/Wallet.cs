using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _countDollars;
    private int _totalCountDollars;

    public int CountDollars => _countDollars;
    public int TotalCountDollars => _totalCountDollars;

    public void SetDefaultValues()
    {
        _countDollars = 0;
        _totalCountDollars = 0;
    }

    public void SetStartValues(int countDollars, int totalCountDollars)
    {
        _countDollars = countDollars;
        _totalCountDollars = totalCountDollars;
    }

    public void SpendDollar()
    {
        _countDollars--;
    }

    public void AddDollar()
    {
        _countDollars++;
        _totalCountDollars++;
    }
}
