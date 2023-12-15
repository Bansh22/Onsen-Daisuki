using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
public class AudioMixerController : MonoBehaviour
{
    public static AudioMixerController Instance;
    [SerializeField] public AudioMixer m_AudioMixer;
    [SerializeField] public Slider m_MusicBGMSlider;
    [SerializeField] public Slider m_MusicSFXSlider;

    private void Awake()
    {
        m_MusicBGMSlider.onValueChanged.AddListener(SetMusicVolume);
        m_MusicSFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }


    public void SetMusicVolume(float volume)
    {
        m_AudioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        m_AudioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
}