using System.Collections.Generic;
using UnityEngine;

public class LoadSceneLanguage : MonoBehaviour
{
    [SerializeField] private List<Language> _textList;

    public void SetLanguage(string language)
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
