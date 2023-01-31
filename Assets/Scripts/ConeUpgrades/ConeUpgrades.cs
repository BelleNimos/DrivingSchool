using TMPro;
using UnityEngine;

public abstract class ConeUpgrades : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Cone _conePrefab;
    [SerializeField] private MoneyPoint _moneyPoint;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private UpgradeConesHorizontalUI _upgradeConesHorizontalUI;

    private bool _isUse;

    protected int Price;

    private void Start()
    {
        _isUse = false;
    }

    private void Update()
    {
        _priceText.text = Price.ToString();
    }

    public void Unlock()
    {
        if (_moneyPoint.CurrentDollarsCount >= Price && _isUse == false)
        {
            _spawner.ChangeConePrefab(_conePrefab);
            _moneyPoint.SpendMoney(Price);
            _isUse = true;
            _upgradeConesHorizontalUI.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
