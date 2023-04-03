using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeaderboardElement : MonoBehaviour
{
    [SerializeField] private List<Sprite> _icons;
    [SerializeField] private Image _playerIcon;
    [SerializeField] private TMP_Text _playerNick;
    [SerializeField] private TMP_Text _playerResult;

    public void Initialize(string nick, int playerResult)
    {
        _playerNick.text = nick;
        _playerResult.text = playerResult.ToString();
        _playerIcon.sprite = GetRandomSprite();
    }

    private Sprite GetRandomSprite()
    {
        int spriteIndex = Random.Range(0, _icons.Count);

        return _icons[spriteIndex];
    }
}
