using UnityEngine;
using UnityEngine.Audio;

public class PauseAndPlay : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private AudioSource _pauseSound;
    [SerializeField] private AudioSource _playSound;
    [SerializeField] private Pause _pause;
    [SerializeField] private Play _play;

    private const string MasterVolumeText = "MasterVolume";

    private void Start()
    {
        _pause.gameObject.SetActive(true);
    }

    public void Pause()
    {
        _play.gameObject.SetActive(true);
        _pause.gameObject.SetActive(false);
        _pauseSound.Play();
        Time.timeScale = 0;
        _mixer.audioMixer.SetFloat(MasterVolumeText, -80f);
    }

    public void Play()
    {
        _pause.gameObject.SetActive(true);
        _play.gameObject.SetActive(false);
        _playSound.Play();
        Time.timeScale = 1;
        _mixer.audioMixer.SetFloat(MasterVolumeText, 0f);
    }
}
