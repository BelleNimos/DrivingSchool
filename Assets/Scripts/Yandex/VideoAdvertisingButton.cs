using TMPro;
using UnityEngine;

public class VideoAdvertisingButton : MonoBehaviour
{
    [SerializeField] private AdvertisingOperator _advertisingOperator;
    [SerializeField] private CashCounter _cashCounter;
    [SerializeField] private TMP_Text _textCount;
    [SerializeField] private int _count;

    private void Start()
    {
        _textCount.text = "+" + _count.ToString();
    }

    public void OnButtonClick()
    {
        _advertisingOperator.ShowVideo();
        _cashCounter.AddDollars(_count);
        gameObject.SetActive(false);
    }
}
