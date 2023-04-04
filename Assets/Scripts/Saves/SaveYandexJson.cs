using Agava.YandexGames;
using System;
using UnityEngine;

public class SaveYandexJson : MonoBehaviour
{
    [SerializeField] private SavePlayerPrefs _savePlayerPrefs;
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
    [SerializeField] private int _indexNextScene;

    private SaveStruct _saveStruct;
    private string _json = "Save";

    private void Start()
    {
        _indexNextScene += 1;
        LoadProgress();
    }

    private void LoadProgress()
    {
        PlayerAccount.GetPlayerData(OnSuccess, OnError);
    }

    private void OnError(string data)
    {
        if (PlayerPrefs.HasKey(KeysData.IndexScene) == true)
            _savePlayerPrefs.LoadProgress();
        else
            ApplyDefaultOptions();
    }

    private void OnSuccess(string data)
    {
        _json = data;

        SaveStruct saveStructJson = JsonUtility.FromJson<SaveStruct>(_json);

        _saveStruct = new()
        {
            BagConesMaxCount = saveStructJson.BagConesMaxCount,
            SpawnerCountWaves = saveStructJson.SpawnerCountWaves,
            PlayerDollarsCount = saveStructJson.PlayerDollarsCount,
            TotalDollarsCount = saveStructJson.TotalDollarsCount,
            UpgradesSpawnerPrice = saveStructJson.UpgradesSpawnerPrice,
            UpgradesSpeedPrice = saveStructJson.UpgradesSpeedPrice,
            UpgradesBagPrice = saveStructJson.UpgradesBagPrice,
            IndexScene = saveStructJson.IndexScene,
            EffectsVolumeValue = saveStructJson.EffectsVolumeValue,
            MusicVolumeValue = saveStructJson.MusicVolumeValue,
            IndexCone = saveStructJson.IndexCone,
            PlayerMoveSpeed = saveStructJson.PlayerMoveSpeed,
            BagSpeedAnimator = saveStructJson.BagSpeedAnimator,
            PlayerSpeedAnimator = saveStructJson.PlayerSpeedAnimator,
            PlayerRadiusX = saveStructJson.PlayerRadiusX,
            PlayerRadiusY = saveStructJson.PlayerRadiusY,
            PlayerRadiusZ = saveStructJson.PlayerRadiusZ,
            CurrentLanguage = saveStructJson.CurrentLanguage
        };

        ApplyStartOptions();
    }

    private void ApplyDefaultOptions()
    {
        _playerMovement.SetDefaultValues();
        _playerAnimator.SetDefaultValues();
        _cashCounter.SetDefaultValues();
        _spawner.SetDefaultValues();
        _bag.SetDefaultValues();
        _upgradeCardSpawner.SetDefaultValues();
        _upgradeCardPlayer.SetDefaultValues();
        _upgradeCardBag.SetDefaultValues();
        _soundEffects.SetDefaultValues();
        _soundMusic.SetDefaultValues();
        _languageSwitcher.SetLanguage(YandexGamesSdk.Environment.i18n.lang);
    }

    private void ApplyStartOptions()
    {
        _playerMovement.SetStartValues(_saveStruct.PlayerMoveSpeed, new Vector3(_saveStruct.PlayerRadiusX, _saveStruct.PlayerRadiusY, _saveStruct.PlayerRadiusZ));
        _playerAnimator.SetStartValues(_saveStruct.PlayerSpeedAnimator);
        _cashCounter.SetStartValues(_saveStruct.PlayerDollarsCount, _saveStruct.TotalDollarsCount);
        _spawner.SetStartValues(_saveStruct.SpawnerCountWaves, _saveStruct.IndexCone);
        _bag.SetStartValues(_saveStruct.BagConesMaxCount, _saveStruct.BagSpeedAnimator);
        _upgradeCardSpawner.SetStartValues(_saveStruct.UpgradesSpawnerPrice);
        _upgradeCardPlayer.SetStartValues(_saveStruct.UpgradesSpeedPrice);
        _upgradeCardBag.SetStartValues(_saveStruct.UpgradesBagPrice);
        _soundEffects.SetStartValues(_saveStruct.EffectsVolumeValue);
        _soundMusic.SetStartValues(_saveStruct.MusicVolumeValue);
        _languageSwitcher.SetLanguage(_saveStruct.CurrentLanguage);
    }

    public void SaveProgress()
    {
        _saveStruct = new()
        {
            BagConesMaxCount = _bag.MaxConesCount,
            SpawnerCountWaves = _spawner.CountWaves,
            PlayerDollarsCount = _cashCounter.CountDollars,
            TotalDollarsCount = _cashCounter.TotalCountDollars,
            UpgradesSpawnerPrice = _upgradeCardSpawner.Price,
            UpgradesSpeedPrice = _upgradeCardPlayer.Price,
            UpgradesBagPrice = _upgradeCardBag.Price,
            IndexScene = _indexNextScene,
            EffectsVolumeValue = Convert.ToInt32(_soundEffects.IsPlay),
            MusicVolumeValue = Convert.ToInt32(_soundMusic.IsPlay),
            IndexCone = _spawner.IndexCone,
            PlayerMoveSpeed = _playerMovement.MoveSpeed,
            BagSpeedAnimator = _bag.SpeedAnimator,
            PlayerSpeedAnimator = _playerAnimator.SpeedAnimator,
            PlayerRadiusX = _playerMovement.Radius.x,
            PlayerRadiusY = _playerMovement.Radius.y,
            PlayerRadiusZ = _playerMovement.Radius.z,
            CurrentLanguage = _languageSwitcher.CurrentLanguage
        };

        _json = JsonUtility.ToJson(_saveStruct, true);
        PlayerAccount.SetPlayerData(_json);
    }
}
