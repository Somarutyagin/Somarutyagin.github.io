//using Unity.Mathematics;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _sliderSound;
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private AudioSource _mainMenuMusic;

    [SerializeField] private AudioSource _gameMusic1;
    [SerializeField] private AudioSource _gameMusic2;
    [SerializeField] private AudioSource _gameMusic3;
    [SerializeField] private AudioSource _gameMusic4;

    private AudioSource _activeGameMusic;

    public AudioSource HitSound;
    public AudioSource DeathSound;
    public AudioSource BonusCollectSound;
    public AudioSource BtnClickSound;

    private readonly string _keySound = "sound";
    private float _valueSound;
    public float Sound
    {
        get
        {
            Init();
            return _valueSound;
        }
        set
        {
            PlayerPrefs.SetFloat(_keySound, value);
            _valueSound = value;
        }
    }
    private readonly string _keyMusic = "music";
    private float _valueMusic;
    public float Music
    {
        get
        {
            Init();
            return _valueMusic;
        }
        set
        {
            PlayerPrefs.SetFloat(_keyMusic, value);
            _valueMusic = value;
        }
    }
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("AudioManager").AddComponent<AudioManager>();
            }
            return _instance;
        }
    }
    private void Init()
    {
        Sound = PlayerPrefs.GetFloat(_keySound, 0f);
        Music = PlayerPrefs.GetFloat(_keyMusic, 0f);
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Initialize()
    {
    }
    public void SoundValueChange()
    {
        _audioMixer.SetFloat(_keySound, Mathf.Lerp(-80.0f, 0, _sliderSound.value));
    }
    public void MusicValueChange()
    {
        _audioMixer.SetFloat(_keyMusic, Mathf.Lerp(-80.0f, 0, _sliderMusic.value));
    }
    public void MainMenuMusicPlay()
    {
        _activeGameMusic.Pause();

        _mainMenuMusic.Play();
    }
    public void GameMusicPlay()
    {
        _mainMenuMusic.Pause();

        int randomMusic = UnityEngine.Random.Range(0, 4);

        switch (randomMusic)
        {
            case 0:
                _gameMusic1.Play();
                _activeGameMusic = _gameMusic1;
                break;
            case 1:
                _gameMusic2.Play();
                _activeGameMusic = _gameMusic2;
                break;
            case 2:
                _gameMusic3.Play();
                _activeGameMusic = _gameMusic3;
                break;
            case 3:
                _gameMusic4.Play();
                _activeGameMusic = _gameMusic4;
                break;
        }
    }
}
