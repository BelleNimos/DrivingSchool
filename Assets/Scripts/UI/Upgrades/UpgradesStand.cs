using UnityEngine;

public class UpgradesStand : MonoBehaviour
{
    [SerializeField] private Upgrades _upgrades;

    public void EnableUpgrades()
    {
        _upgrades.EnablePanel();
    }

    public void DisableUpgrades()
    {
        _upgrades.DisablePanel();
    }
}
