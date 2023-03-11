using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public class Upgrades : MonoBehaviour
{
    [SerializeField] private AudioSource _purchase;
    [SerializeField] private AudioSource _notEnough;
    [SerializeField] private AudioSource _departure;
    [SerializeField] private TMP_Text _spawnerPriceText;
    [SerializeField] private TMP_Text _speedPriceText;
    [SerializeField] private TMP_Text _bagPriceText;
    [SerializeField] private MoneyPoint _moneyPoint;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Bag _bag;

    private Animator _animator;
    private int _spawnerPrice;
    private int _speedPrice;
    private int _bagPrice;

    private const string Open = "Open";
    private const string Close = "Close";
    private const int SurplusFactor = 20;

    public int SpawnerPrice => _spawnerPrice;
    public int SpeedPrice => _speedPrice;
    public int BagPrice => _bagPrice;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(KeysData.UpgradesSpawnerPrice) == true)
            _spawnerPrice = PlayerPrefs.GetInt(KeysData.UpgradesSpawnerPrice);
        else
            _spawnerPrice = 20;

        if (PlayerPrefs.HasKey(KeysData.UpgradesSpeedPrice) == true)
            _speedPrice = PlayerPrefs.GetInt(KeysData.UpgradesSpeedPrice);
        else
            _speedPrice = 20;

        if (PlayerPrefs.HasKey(KeysData.UpgradesBagPrice) == true)
            _bagPrice = PlayerPrefs.GetInt(KeysData.UpgradesBagPrice);
        else
            _bagPrice = 20;
    }

    private void Update()
    {
        _spawnerPriceText.text = _spawnerPrice.ToString();
        _speedPriceText.text = _speedPrice.ToString();
        _bagPriceText.text = _bagPrice.ToString();
    }

    private void Upgrade(ref int price)
    {
        if (_moneyPoint.CurrentDollarsCount >= price)
        {
            _purchase.Play();
            _moneyPoint.SpendMoney(price);
            price += SurplusFactor;
        }
        else
        {
            _notEnough.Play();
        }
    }

    public void UpgradeSpawner()
    {
        Upgrade(ref _spawnerPrice);

        if (_moneyPoint.CurrentDollarsCount >= _spawnerPrice)
            _spawner.IncreaseCountWaves();
    }

    public void UpgradeSpeed()
    {
        Upgrade(ref _speedPrice);

        if (_moneyPoint.CurrentDollarsCount >= _speedPrice)
            _playerMovement.IncreaseSpeed();
    }

    public void UpgradeBag()
    {
        Upgrade(ref _bagPrice);

        if (_moneyPoint.CurrentDollarsCount >= _bagPrice)
            _bag.IncreaseCapacity();
    }

    public void OpenPanel()
    {
        _departure.Play();
        _animator.SetTrigger(Open);
    }

    public void ClosePanel()
    {
        _animator.SetTrigger(Close);
    }
}
