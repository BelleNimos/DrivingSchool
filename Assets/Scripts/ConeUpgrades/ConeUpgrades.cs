using TMPro;
using UnityEngine;

public abstract class ConeUpgrades : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Cone _conePrefab;
    [SerializeField] private MoneyPoint _moneyPoint;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private UpgradeConesHorizontalUI _upgradeConesHorizontalUI;

    public int Price { get; protected set; }

    private void Update()
    {
        _priceText.text = Price.ToString();
    }

    public void Unlock()
    {
        _spawner.ChangeConePrefab(_conePrefab);
        _moneyPoint.SpendMoney(Price);

        _upgradeConesHorizontalUI.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
