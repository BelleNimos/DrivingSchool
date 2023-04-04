using UnityEngine;

public class PlayAndPauseButtonsSwitcher : MonoBehaviour
{
    [SerializeField] private Pause _pause;
    [SerializeField] private Play _play;

    private void Start()
    {
        _pause.gameObject.SetActive(true);
    }

    public void Pause()
    {
        _play.gameObject.SetActive(true);
        _pause.gameObject.SetActive(false);
    }

    public void Play()
    {
        _pause.gameObject.SetActive(true);
        _play.gameObject.SetActive(false);
    }
}
