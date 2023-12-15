using OVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private Dictionary<Sfx, float> lastPlayTimeDictionary = new Dictionary<Sfx, float>();
    public float defaultCooldown = 0.5f; // 기본 간격
    public AudioMixer Mixer;
    public AudioMixerGroup BGMgroup;
    public AudioMixerGroup SFXgroup;

    [Header("#BGM")]
    public AudioClip bgmClip1;
    public AudioClip bgmClip2;
    public AudioClip bgmClip3;
    public float bgmVolume;
    AudioSource bgmPlayer;
    AudioHighPassFilter bgmEffect;

    [Header("#SFX")]
    public AudioClip[] sfxClip;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex; // channel index


    public enum Sfx
    {
        pistol, 
        start, 
        UI_select, 
        UI_close, 
        monster_hit, 
        monster_dead, 
        helmet_hit, 
        helmet_dead,
        reload,
        pink_voice,
        rabbit_voice,
        lightning
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Init();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        PlayBgm(true, 1);  // Start에서 호출하도록 변경

        bgmPlayer.volume = bgmPlayer.volume;

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index].volume = sfxVolume;
        }
        AudioMixerGroup mixerGroup = Mixer.outputAudioMixerGroup;
    }

    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.outputAudioMixerGroup = BGMgroup;
     

        // 초기 클립 설정
        if (bgmClip1 != null)
            bgmPlayer.clip = bgmClip1;
        else
            Debug.LogError("BgmClip1이 초기화되지 않았습니다.");

        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();

        // 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("sfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];
       

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].bypassListenerEffects = true;
            sfxPlayers[index].outputAudioMixerGroup = SFXgroup;
            
        }


    }

    public void PlayBgm(bool isPlay, int bgmIndex = 1)
    {
        bgmPlayer.volume = bgmVolume;

        if (isPlay)
        {
            if (bgmPlayer != null)  // null 체크 추가
            {
                if (bgmIndex == 1)
                    bgmPlayer.clip = bgmClip1;
                else if (bgmIndex == 2)
                    bgmPlayer.clip = bgmClip2;
                else if (bgmIndex == 3)
                    bgmPlayer.clip = bgmClip3;

                bgmPlayer.Play();
            }
            else
            {
                Debug.LogError("BgmPlayer가 초기화되지 않았습니다.");
            }
        }
        else
        {
            if (bgmPlayer != null)  // null 체크 추가
            {
                bgmPlayer.Stop();
            }
            else
            {
                Debug.LogError("BgmPlayer가 초기화되지 않았습니다.");
            }
        }
    }

    public void EffectBgm(bool isPlay)
    {
        bgmEffect.enabled = isPlay;
    }

    public void Playsfx(Sfx sfx)
    {

        float currentTime = Time.time;

        if (lastPlayTimeDictionary.ContainsKey(sfx) && currentTime - lastPlayTimeDictionary[sfx] < defaultCooldown)
        {
            // 간격이 충족되지 않으면 아무 것도 하지 않음
            return;
        }


        int availableChannelIndex = -1;

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (!sfxPlayers[loopIndex].isPlaying)
            {
                availableChannelIndex = loopIndex;
                break;
            }
        }

        if (availableChannelIndex != -1)
        {
            channelIndex = availableChannelIndex;

            // 볼륨 조절
            float adjustedVolume = sfxVolume;

            if (sfx == Sfx.pink_voice)
            {
                adjustedVolume *= 4f; // 볼륨을 2.5배로 높임
            }

            if (sfx == Sfx.rabbit_voice)
            {
                adjustedVolume *= 8f; // 볼륨을 0.25배로 높임
            }

            sfxPlayers[channelIndex].volume = adjustedVolume;

            sfxPlayers[channelIndex].clip = sfxClip[(int)sfx];
            sfxPlayers[channelIndex].Play();
            lastPlayTimeDictionary[sfx] = currentTime; // 재생한 시간을 기록
        }
    }






    public void SFXVolume(float volume)
    {
        sfxVolume = volume;
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index].volume = sfxVolume;
        }
    }

    public void BGMVolume(float volume)
    {
        bgmVolume = volume;
        bgmPlayer.volume = bgmVolume;
    }
}