using TMPro;
using UnityEngine;

public class ConeUpgrades : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Cone _conePrefab;
    [SerializeField] private MoneyPoint _moneyPoint;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private UpgradeConesHorizontalUI _upgradeConesHorizontalUI;
    [SerializeField] private int _price;

    public int Price => _price;

    private void Update()
    {
        _priceText.text = _price.ToString();
    }

    public void Unlock()
    {
        if (_moneyPoint.CurrentDollarsCount >= _price)
        {
            _moneyPoint.SpendMoney(_price);
            _spawner.ChangeConePrefab(_conePrefab);
            _upgradeConesHorizontalUI.StartAnimationClose();
            gameObject.SetActive(false);
        }
    }
}
