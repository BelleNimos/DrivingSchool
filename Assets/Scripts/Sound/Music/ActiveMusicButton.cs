using UnityEngine;
using UnityEngine.Audio;

public class ActiveMusicButton : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixerGroup;

    private const string MusicVolumeText = "MusicVolume";
    private const float Volume = 0f;

    private void OnEnable()
    {
        _mixerGroup.audioMixer.SetFloat(MusicVolumeText, Volume);
    }
}
