using UnityEngine;

public class UpgradesStand : MonoBehaviour
{
    [SerializeField] private Upgrades _upgrades;

    public void EnableUpgrades()
    {
        _upgrades.OpenPanel();
    }

    public void DisableUpgrades()
    {
        _upgrades.ClosePanel();
    }
}
