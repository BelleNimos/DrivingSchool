using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitcherScene : MonoBehaviour
{
    [SerializeField] private LoadSceneLanguage _loadSceneLanguage;

    private SaveStruct _saveStruct;
    private int _indexFirstLevel = 2;
    private string _json = "Save";

    private void Start()
    {
        LoadLanguage();
        Invoke("LoadNewScene", 1f);
    }

    private void UnzipFile(string data)
    {
        _json = data;

        SaveStruct saveStructJson = JsonUtility.FromJson<SaveStruct>(_json);

        _saveStruct = new()
        {
            IndexScene = saveStructJson.IndexScene,
            CurrentLanguage = saveStructJson.CurrentLanguage
        };
    }

    private void LoadLanguage()
    {
        PlayerAccount.GetPlayerData(OnSuccessLoadLanguage, OnErrorLoadLanguage);
    }

    private void LoadNewScene()
    {
        PlayerAccount.GetPlayerData(OnSuccessLoadScene, OnErrorLoadScene);
    }

    private void OnSuccessLoadLanguage(string data)
    {
        UnzipFile(data);

        _loadSceneLanguage.SetLanguage(_saveStruct.CurrentLanguage);
    }

    private void OnSuccessLoadScene(string data)
    {
        UnzipFile(data);

        SceneManager.LoadScene(_saveStruct.IndexScene);
    }

    private void OnErrorLoadLanguage(string data)
    {
        _loadSceneLanguage.SetLanguage(YandexGamesSdk.Environment.i18n.lang);
    }

    private void OnErrorLoadScene(string data)
    {
        if (PlayerPrefs.HasKey(KeysData.IndexScene) == true)
            SceneManager.LoadScene(PlayerPrefs.GetInt(KeysData.IndexScene));
        else
            SceneManager.LoadScene(_indexFirstLevel);
    }
}
