using UnityEngine;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    [SerializeField] private PlayAndPauseButtonsSwitcher _pauseAndPlay;
    [SerializeField] private AudioListener _audioListener;
    [SerializeField] private Image _pausePanel;

    private void OnEnable()
    {
        AudioListener.pause = true;
        AudioListener.volume = 0f;
        _pausePanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
