using UnityEngine;
using TMPro;

public class MoneyPoint : MonoBehaviour
{
    [SerializeField] private TMP_Text _dollarsCount;

    private int _countDollars;

    public int CurrentDollarsCount => _countDollars;

    private void Start()
    {
        _countDollars = 0;

        if (SceneData.MoneyPlayer > 0)
            _countDollars = SceneData.MoneyPlayer;
    }

    private void Update()
    {
        _dollarsCount.text = CurrentDollarsCount.ToString();
    }

    public void AddDollar()
    {
        _countDollars++;
    }

    public void SpendMoney(int price)
    {
        if (_countDollars >= price)
            _countDollars -= price;
    }
}
