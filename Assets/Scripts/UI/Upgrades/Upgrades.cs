using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public class Upgrades : MonoBehaviour
{
    [SerializeField] private TMP_Text _spawnerPriceText;
    [SerializeField] private TMP_Text _speedPriceText;
    [SerializeField] private TMP_Text _bagPriceText;
    [SerializeField] private MoneyPoint _moneyPoint;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private CharacterMovement _characterMovement;
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
        if (SceneData.SpawnerUpgradePrice > 0 && SceneData.SpeedUpgradePrice > 0 && SceneData.BagUpgradePrice > 0)
        {
            _spawnerPrice = SceneData.SpawnerUpgradePrice;
            _speedPrice = SceneData.SpeedUpgradePrice;
            _bagPrice = SceneData.BagUpgradePrice;
        }
        else
        {
            _spawnerPrice = 20;
            _speedPrice = 20;
            _bagPrice = 20;
        }
    }

    private void Update()
    {
        _spawnerPriceText.text = _spawnerPrice.ToString();
        _speedPriceText.text = _speedPrice.ToString();
        _bagPriceText.text = _bagPrice.ToString();
    }

    private void Upgrade(ref int price)
    {
        _moneyPoint.SpendMoney(price);
        price += SurplusFactor;
    }

    public void OpenPanel()
    {
        _animator.SetTrigger(Open);
    }

    public void ClosePanel()
    {
        _animator.SetTrigger(Close);
    }

    public void UpgradeSpawner()
    {
        if (_moneyPoint.CurrentDollarsCount >= _spawnerPrice)
        {
            Upgrade(ref _spawnerPrice);
            _spawner.IncreaseCountWaves();
        }
    }

    public void UpgradeSpeed()
    {
        if (_moneyPoint.CurrentDollarsCount >= _speedPrice)
        {
            Upgrade(ref _speedPrice);
            _characterMovement.IncreaseSpeed();
        }
    }

    public void UpgradeBag()
    {
        if (_moneyPoint.CurrentDollarsCount >= _bagPrice)
        {
            Upgrade(ref _bagPrice);
            _bag.IncreaseCapacity();
        }
    }
}
