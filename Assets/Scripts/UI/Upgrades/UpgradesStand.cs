using UnityEngine;

public class UpgradesStand : MonoBehaviour
{
    [SerializeField] private Scale _sliderUpgrades;
    [SerializeField] private Upgrades _upgrades;

    private void Update()
    {
        if (_sliderUpgrades.IsEmpty == true)
        {
            _sliderUpgrades.gameObject.SetActive(false);

            if (_upgrades.IsEnable == false)
                _upgrades.EnablePanel();
        }
    }

    public void EnableSlider()
    {
        _sliderUpgrades.gameObject.SetActive(true);
    }

    public void DisableUpgrades()
    {
        _upgrades.gameObject.SetActive(false);
    }
}
