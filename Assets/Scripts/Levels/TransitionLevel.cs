using TMPro;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionLevel : MonoBehaviour
{
    [Header("Using")]
    [SerializeField] private NextLevelUI _nextLevelUI;
    [SerializeField] private AudioSource _yesSound;
    [SerializeField] private AudioSource _noSound;
    [SerializeField] private AudioSource _crashSound;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private int _price;
    [SerializeField] private int _idNextLevel;

    [Header("Transitions")]
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private MoneyPoint _moneyPoint;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Bag _bag;
    [SerializeField] private Upgrades _upgrades;
    [SerializeField] private Sound _sound;

    private void Start()
    {
        _priceText.text = _price.ToString();
        _idNextLevel += 1;
    }

    private void ChangeOptionsNextLevel()
    {
        PlayerPrefs.SetInt(KeysData.BagConesMaxCount, _bag.MaxConesCount);
        PlayerPrefs.SetInt(KeysData.SpawnerCountWaves, _spawner.CountWaves);
        PlayerPrefs.SetInt(KeysData.PlayerDollarsCount, _moneyPoint.CurrentDollarsCount);
        PlayerPrefs.SetInt(KeysData.UpgradesSpawnerPrice, _upgrades.SpawnerPrice);
        PlayerPrefs.SetInt(KeysData.UpgradesSpeedPrice, _upgrades.SpeedPrice);
        PlayerPrefs.SetInt(KeysData.UpgradesBagPrice, _upgrades.BagPrice);
        PlayerPrefs.SetInt(KeysData.IndexScene, _idNextLevel);
        PlayerPrefs.SetInt(KeysData.SoundVolumeValue, Convert.ToInt32(_sound.IsActive));
        PlayerPrefs.SetInt(KeysData.IndexCone, _spawner.IndexCone);
        PlayerPrefs.SetFloat(KeysData.PlayerMoveSpeed, _playerMovement.MoveSpeed);
        PlayerPrefs.SetFloat(KeysData.BagSpeedAnimator, _bag.SpeedAnimator);
        PlayerPrefs.SetFloat(KeysData.PlayerSpeedAnimator, _playerAnimator.SpeedAnimator);
        PlayerPrefs.SetFloat(KeysData.PlayerRadiusX, _playerMovement.Radius.x);
        PlayerPrefs.SetFloat(KeysData.PlayerRadiusY, _playerMovement.Radius.y);
        PlayerPrefs.SetFloat(KeysData.PlayerRadiusZ, _playerMovement.Radius.z);
        PlayerPrefs.Save();
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
        if (_moneyPoint.CurrentDollarsCount >= _price)
        {
            _yesSound.Play();
            _moneyPoint.SpendMoney(_price);
            ChangeOptionsNextLevel();
            SceneManager.LoadScene(1);
            Time.timeScale = 1;
        }
        else
        {
            _crashSound.Play();
        }
    }
}
