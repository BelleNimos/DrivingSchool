using UnityEngine;
using UnityEngine.Audio;

public class InactiveMusicButton : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixerGroup;

    private const string MusicVolumeText = "MusicVolume";
    private const float Volume = -80f;

    private void OnEnable()
    {
        _mixerGroup.audioMixer.SetFloat(MusicVolumeText, Volume);
    }
}
