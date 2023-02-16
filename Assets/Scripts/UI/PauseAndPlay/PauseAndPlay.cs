using UnityEngine;

public class PauseAndPlay : MonoBehaviour
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
        Time.timeScale = 0;
    }

    public void Play()
    {
        _pause.gameObject.SetActive(true);
        _play.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
