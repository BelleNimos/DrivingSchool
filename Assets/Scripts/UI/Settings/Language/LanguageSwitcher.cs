using Agava.YandexGames;
using UnityEngine;

public class LanguageSwitcher : MonoBehaviour
{
    [SerializeField] private LanguageButton _languageButton;
    [SerializeField] private InternationalText _coneUpgrades;
    [SerializeField] private InternationalText _lessonNorth;
    [SerializeField] private InternationalText _lessonSouth;
    [SerializeField] private InternationalText _upgradesStand;
    [SerializeField] private InternationalText _customerArea;
    [SerializeField] private InternationalText _waitingZoneNorth;
    [SerializeField] private InternationalText _waitingZoneSouth;
    [SerializeField] private InternationalText _nextLevel;
    [SerializeField] private InternationalText _spawnerUpgrade;
    [SerializeField] private InternationalText _bagUpgrade;
    [SerializeField] private InternationalText _speedUpgrade;
    [SerializeField] private InternationalText _pause;
    [SerializeField] private InternationalText _max;
    [SerializeField] private InternationalText _transitionLevel;

    private void Start()
    {
        if (PlayerPrefs.HasKey(KeysData.CurrentLanguage) == false)
            SetLanguage(YandexGamesSdk.Environment.i18n.lang);
        else if (PlayerPrefs.HasKey(KeysData.CurrentLanguage) == true)
            SetLanguage(PlayerPrefs.GetString(KeysData.CurrentLanguage));
        else
            SetLanguage("en");
    }

    public void SetLanguage(string language)
    {
        _languageButton.SetLanguage(language);
        _coneUpgrades.SetLanguage(language);
        _lessonNorth.SetLanguage(language);
        _lessonSouth.SetLanguage(language);
        _upgradesStand.SetLanguage(language);
        _customerArea.SetLanguage(language);
        _waitingZoneNorth.SetLanguage(language);
        _waitingZoneSouth.SetLanguage(language);
        _nextLevel.SetLanguage(language);
        _spawnerUpgrade.SetLanguage(language);
        _bagUpgrade.SetLanguage(language);
        _speedUpgrade.SetLanguage(language);
        _pause.SetLanguage(language);
        _max.SetLanguage(language);
        _transitionLevel.SetLanguage(language);
    }
}
