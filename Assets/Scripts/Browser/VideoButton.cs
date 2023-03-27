using TMPro;
using UnityEngine;

public class VideoButton : MonoBehaviour
{
    [SerializeField] private AdvertisingOperator _advertisingOperator;
    [SerializeField] private MoneyPoint _moneyPoint;
    [SerializeField] private TMP_Text _textCount;
    [SerializeField] private int _count;

    private void Start()
    {
        _textCount.text = "+" + _count.ToString();
    }

    public void OnButtonClick()
    {
        _advertisingOperator.ShowVideo();
        _moneyPoint.AddDollars(_count);
        gameObject.SetActive(false);
    }
}
