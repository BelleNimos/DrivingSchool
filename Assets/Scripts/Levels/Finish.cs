using TMPro;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private MoneyPoint _moneyPoint;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private int _price;

    private bool _isUse;

    private void Start()
    {
        _priceText.text = _price.ToString();
        _isUse = false;
    }

    public void Finished()
    {
        if (_isUse == false)
        {
            if (_moneyPoint.CurrentDollarsCount >= _price)
            {
                _moneyPoint.SpendMoney(_price);
                _isUse = true;
            }
        }
    }
}
