using UnityEngine;

public class UpgradesStand : MonoBehaviour
{
    [SerializeField] private UpgradesPanel _upgrades;

    public void EnableUpgradesPanel()
    {
        _upgrades.OpenPanel();
    }

    public void DisableUpgradesPanel()
    {
        _upgrades.ClosePanel();
    }
}
