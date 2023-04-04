using System;
using UnityEngine;

public class SavePlayerPrefs : MonoBehaviour
{
    [SerializeField] private LanguageSwitcher _languageSwitcher;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private CashCounter _cashCounter;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Bag _bag;
    [SerializeField] private UpgradeCardSpawner _upgradeCardSpawner;
    [SerializeField] private UpgradeCardPlayer _upgradeCardPlayer;
    [SerializeField] private UpgradeCardBag _upgradeCardBag;
    [SerializeField] private SoundEffects _soundEffects;
    [SerializeField] private SoundMusic _soundMusic;
    [SerializeField] private int _idNextLevel;

    private void Awake()
    {
        _idNextLevel += 1;
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt(KeysData.BagConesMaxCount, _bag.MaxConesCount);
        PlayerPrefs.SetInt(KeysData.SpawnerCountWaves, _spawner.CountWaves);
        PlayerPrefs.SetInt(KeysData.PlayerDollarsCount, _cashCounter.CountDollars);
        PlayerPrefs.SetInt(KeysData.TotalDollarsCount, _cashCounter.TotalCountDollars);
        PlayerPrefs.SetInt(KeysData.UpgradesSpawnerPrice, _upgradeCardSpawner.Price);
        PlayerPrefs.SetInt(KeysData.UpgradesSpeedPrice, _upgradeCardPlayer.Price);
        PlayerPrefs.SetInt(KeysData.UpgradesBagPrice, _upgradeCardBag.Price);
        PlayerPrefs.SetInt(KeysData.IndexScene, _idNextLevel);
        PlayerPrefs.SetInt(KeysData.EffectsVolumeValue, Convert.ToInt32(_soundEffects.IsPlay));
        PlayerPrefs.SetInt(KeysData.MusicVolumeValue, Convert.ToInt32(_soundMusic.IsPlay));
        PlayerPrefs.SetInt(KeysData.IndexCone, _spawner.IndexCone);
        PlayerPrefs.SetFloat(KeysData.PlayerMoveSpeed, _playerMovement.MoveSpeed);
        PlayerPrefs.SetFloat(KeysData.BagSpeedAnimator, _bag.SpeedAnimator);
        PlayerPrefs.SetFloat(KeysData.PlayerSpeedAnimator, _playerAnimator.SpeedAnimator);
        PlayerPrefs.SetFloat(KeysData.PlayerRadiusX, _playerMovement.Radius.x);
        PlayerPrefs.SetFloat(KeysData.PlayerRadiusY, _playerMovement.Radius.y);
        PlayerPrefs.SetFloat(KeysData.PlayerRadiusZ, _playerMovement.Radius.z);
        PlayerPrefs.SetString(KeysData.CurrentLanguage, _languageSwitcher.CurrentLanguage);
        PlayerPrefs.Save();
    }

    public void LoadProgress()
    {
        int bagConesMaxCount = PlayerPrefs.GetInt(KeysData.BagConesMaxCount);
        int spawnerCountWaves = PlayerPrefs.GetInt(KeysData.SpawnerCountWaves);
        int playerDollarsCount = PlayerPrefs.GetInt(KeysData.PlayerDollarsCount);
        int totalDollarsCount = PlayerPrefs.GetInt(KeysData.TotalDollarsCount);
        int upgradesSpawnerPrice = PlayerPrefs.GetInt(KeysData.UpgradesSpawnerPrice);
        int upgradesSpeedPrice = PlayerPrefs.GetInt(KeysData.UpgradesSpeedPrice);
        int upgradesBagPrice = PlayerPrefs.GetInt(KeysData.UpgradesBagPrice);
        int effectsVolumeValue = PlayerPrefs.GetInt(KeysData.EffectsVolumeValue);
        int musicVolumeValue = PlayerPrefs.GetInt(KeysData.MusicVolumeValue);
        int indexCone = PlayerPrefs.GetInt(KeysData.IndexCone);
        float playerMoveSpeed = PlayerPrefs.GetFloat(KeysData.PlayerMoveSpeed);
        float bagSpeedAnimator = PlayerPrefs.GetFloat(KeysData.BagSpeedAnimator);
        float playerSpeedAnimator = PlayerPrefs.GetFloat(KeysData.PlayerSpeedAnimator);
        float playerRadiusX = PlayerPrefs.GetFloat(KeysData.PlayerRadiusX);
        float playerRadiusY = PlayerPrefs.GetFloat(KeysData.PlayerRadiusY);
        float playerRadiusZ = PlayerPrefs.GetFloat(KeysData.PlayerRadiusZ);
        string language = PlayerPrefs.GetString(KeysData.CurrentLanguage);

        _playerMovement.SetStartValues(playerMoveSpeed, new Vector3(playerRadiusX, playerRadiusY, playerRadiusZ));
        _playerAnimator.SetStartValues(playerSpeedAnimator);
        _cashCounter.SetStartValues(playerDollarsCount, totalDollarsCount);
        _spawner.SetStartValues(spawnerCountWaves, indexCone);
        _bag.SetStartValues(bagConesMaxCount, bagSpeedAnimator);
        _upgradeCardSpawner.SetStartValues(upgradesSpawnerPrice);
        _upgradeCardPlayer.SetStartValues(upgradesSpeedPrice);
        _upgradeCardBag.SetStartValues(upgradesBagPrice);
        _soundEffects.SetStartValues(effectsVolumeValue);
        _soundMusic.SetStartValues(musicVolumeValue);
        _languageSwitcher.SetLanguage(language);
    }
}
