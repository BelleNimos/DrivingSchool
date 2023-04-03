using Agava.YandexGames;
using UnityEngine;

public class LeaderboardSwitcher : MonoBehaviour
{
    [SerializeField] private YandexLeaderboard _yandexLeaderboard;
    [SerializeField] private CashCounter _cashCounter;

    private bool _isActive = false;

    public void Show()
    {
        if (_isActive == false)
            Open();
        else
            Close();
    }

    public void Open()
    {
        bool test = false;

        if (test == true)
        {
            _yandexLeaderboard.FormListOfTopPlayers();
            _yandexLeaderboard.Expand();
            _isActive = true;

            return;
        }

        PlayerAccount.Authorize();

        if (PlayerAccount.IsAuthorized == true)
        {
            PlayerAccount.RequestPersonalProfileDataPermission();
            _yandexLeaderboard.AddPlayerToLeaderboard(_cashCounter.TotalCountDollars);
            _yandexLeaderboard.FormListOfTopPlayers();
            _yandexLeaderboard.Expand();
            _isActive = true;
        }
    }

    public void Close()
    {
        _yandexLeaderboard.Minimize();
        _isActive = false;
    }
}
