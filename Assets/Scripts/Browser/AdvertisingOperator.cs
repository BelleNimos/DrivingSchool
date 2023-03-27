using Agava.YandexGames;
using UnityEngine;

public class AdvertisingOperator : MonoBehaviour
{
    [SerializeField] private SoundEffects _soundEffects;
    [SerializeField] private SoundMusic _soundMusic;

    private void StopGame()
    {
        _soundEffects.Disable();
        _soundMusic.Disable();
        Time.timeScale = 0;
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
        _soundEffects.Enable();
        _soundMusic.Enable();
    }

    public void ShowInterstitial()
    {
        InterstitialAd.Show(StopGame);
    }

    public void ShowVideo()
    {
        VideoAd.Show(StopGame, ContinueGame);
    }

    public void ShowSticky()
    {
        StickyAd.Show();
    }
}
