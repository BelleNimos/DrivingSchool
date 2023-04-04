using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionLevel : MonoBehaviour
{
    [SerializeField] private AdvertisingOperator _advertisingOperator;
    [SerializeField] private YandexLeaderboard _yandexLeaderboard;
    [SerializeField] private SavePlayerPrefs _playerPrefsLoader;
    [SerializeField] private SaveYandexJson _yandexJsonLoader;
    [SerializeField] private CashCounter _cashCounter;
    [SerializeField] private NextLevelUI _nextLevelUI;
    [SerializeField] private AudioSource _yesSound;
    [SerializeField] private AudioSource _noSound;
    [SerializeField] private AudioSource _crashSound;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private int _price;

    private void Start()
    {
        _priceText.text = _price.ToString();
    }

    public void OpenPanel()
    {
        if (_nextLevelUI.IsActive == false)
            _nextLevelUI.OpenPanel();
    }

    public void ClosePanel()
    {
        if (_nextLevelUI.IsActive == true)
        {
            _noSound.Play();
            _nextLevelUI.ClosePanel();
        }
    }

    public void GoToNextLevel()
    {
        if (_cashCounter.CountDollars >= _price)
        {
            _yandexLeaderboard.AddPlayerToLeaderboard(_cashCounter.TotalCountDollars);
            _advertisingOperator.ShowInterstitial();

            _yesSound.Play();
            _cashCounter.SpendDollars(_price);

            _playerPrefsLoader.SaveProgress();
            _yandexJsonLoader.SaveProgress();
            SceneManager.LoadScene(1);
        }
        else
        {
            _crashSound.Play();
        }
    }
}
