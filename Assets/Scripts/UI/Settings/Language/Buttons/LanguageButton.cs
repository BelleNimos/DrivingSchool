using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LanguageButton : MonoBehaviour
{
    [SerializeField] private LanguageSwitcher _switcher;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private List<Language> _textList;

    private Animator _animator;

    private const string Open = "Open";
    private const string Close = "Close";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

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

    public void SwitchLanguage()
    {
        _audio.Play();
        string language;

        for (int i = 0; i < _textList.Count; i++)
        {
            if (_textList[i].gameObject.activeSelf == true)
            {
                if (i == _textList.Count - 1)
                {
                    _textList[i].gameObject.SetActive(false);
                    _textList[0].gameObject.SetActive(true);
                    language = _textList[0].ThisLanguage;
                }
                else
                {
                    _textList[i].gameObject.SetActive(false);
                    _textList[i + 1].gameObject.SetActive(true);
                    language = _textList[i + 1].ThisLanguage;
                }

                PlayerPrefs.SetString(KeysData.CurrentLanguage, language);
                _switcher.SetLanguage(language);

                break;
            }
        }
    }

    public void PlayOpen()
    {
        _animator.SetTrigger(Open);
    }

    public void PlayClose()
    {
        _animator.SetTrigger(Close);
    }
}
