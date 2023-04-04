using UnityEngine;
using UnityEngine.Audio;

public class ActiveEffectsButton : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _mixerGroup;

    private const string EffectsVolumeText = "EffectsVolume";
    private const float Volume = 0f;

    private void OnEnable()
    {
        _mixerGroup.audioMixer.SetFloat(EffectsVolumeText, Volume);
    }
}
