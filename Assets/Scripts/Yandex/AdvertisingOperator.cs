using Agava.YandexGames;
using UnityEngine;

public class AdvertisingOperator : MonoBehaviour
{
    [SerializeField] private PlayAndPauseButtonsSwitcher _playAndPauseButtonsSwitcher;

    private void StopGame()
    {
        _playAndPauseButtonsSwitcher.Pause();
    }

    private void ContinueGame()
    {
        _playAndPauseButtonsSwitcher.Play();
    }

    private void ContinueGameState(bool state)
    {
        if (state == true)
            _playAndPauseButtonsSwitcher.Play();
    }

    public void ShowInterstitial()
    {
        InterstitialAd.Show(StopGame, ContinueGameState);
    }

    public void ShowVideo()
    {
        VideoAd.Show(StopGame, StopGame, ContinueGame);
    }

    public void ShowSticky()
    {
        StickyAd.Show();
    }
}
