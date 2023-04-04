using UnityEngine;
using UnityEngine.Audio;

public class InactiveEffectsButton : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixerGroup;

    private const string EffectsVolumeText = "EffectsVolume";
    private const float Volume = -80f;

    private void OnEnable()
    {
        _mixerGroup.audioMixer.SetFloat(EffectsVolumeText, Volume);
    }
}
