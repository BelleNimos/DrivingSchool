using Agava.YandexGames;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneLanguage : MonoBehaviour
{
    [SerializeField] private List<Language> _textList;

    private void Start()
    {
        if (PlayerPrefs.HasKey(KeysData.CurrentLanguage) == false)
            SetLanguage(YandexGamesSdk.Environment.i18n.lang);
        else if(PlayerPrefs.HasKey(KeysData.CurrentLanguage) == true)
            SetLanguage(PlayerPrefs.GetString(KeysData.CurrentLanguage));
        else
            SetLanguage("en");
    }

    private void SetLanguage(string language)
    {
        foreach (var item in _textList)
        {
            if (item.ThisLanguage == language)
                item.gameObject.SetActive(true);
            else
                item.gameObject.SetActive(false);
        }
    }
}
