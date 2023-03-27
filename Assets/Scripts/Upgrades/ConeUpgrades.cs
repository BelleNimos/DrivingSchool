using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(BoxCollider))]
public class ConeUpgrades : MonoBehaviour
{
    [SerializeField] private AdvertisingOperator _advertisingOperator;
    [SerializeField] private AudioSource _departure;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Cone _conePrefab;
    [SerializeField] private MoneyPoint _moneyPoint;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private int _price;

    private Animator _animator;
    private BoxCollider _boxCollider;

    private const string Close = "Close";

    public int Price => _price;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider>();
        _priceText.text = _price.ToString();
    }

    private void StartAnimationClose()
    {
        _animator.SetTrigger(Close);
        _departure.Play();
    }

    public void Unlock()
    {
        if (_moneyPoint.CurrentDollarsCount >= _price)
        {
            _advertisingOperator.ShowInterstitial();
            _moneyPoint.SpendMoney(_price);
            _spawner.ChangeConePrefab(_conePrefab);
            StartAnimationClose();
            _boxCollider.enabled = false;
        }
    }
}
