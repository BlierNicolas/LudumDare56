using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    
    private AudioSource _musicSource; 
    private AudioSource _sfxSource;   
    
    [Header("Background Music")]
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private AudioSource _startScreenMusic;
    
    [Header("Sound Effects")]
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _walkSound;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _stickSound;
    [SerializeField] private AudioClip _buttonPressSound;
    [SerializeField] private AudioClip _gameOverSound;
    [SerializeField] private AudioClip _victorySound;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
            InitializeAudioSources();
            PlayBackgroundMusic();
        }
    }
    
    private void InitializeAudioSources()
    {
        _musicSource = gameObject.AddComponent<AudioSource>();
        _musicSource.clip = _backgroundMusic;
        _musicSource.loop = true;
        _musicSource.playOnAwake = false;
        _musicSource.volume = 0.25f; 
        
        _sfxSource = gameObject.AddComponent<AudioSource>();
        _sfxSource.loop = false;
        _sfxSource.playOnAwake = false;
        _sfxSource.volume = 0.5f; 
    }
    
    private void PlayBackgroundMusic()
    {
            _musicSource.Play();

    }
    
    public void PlayJumpSound()
    {
        PlaySFX(_jumpSound);
    }
    
    public void PlayWalkSound()
    {
        PlaySFX(_walkSound);
    }
    
    public void PlayHitSound()
    {
        PlaySFX(_hitSound);
    }
    
    public void PlayStickSound()
    {
        PlaySFX(_stickSound);
    }
    
    public void PlayButtonPressSound()
    {
        PlaySFX(_buttonPressSound);
    }
    
    public void PlayGameOverSound()
    {
        PlaySFX(_gameOverSound);
    }
    
    public void PlayVictorySound()
    {
        PlaySFX(_victorySound);
    }
    
    public void PlayStartScreenMusic()
    {
        _startScreenMusic.Play();
    }
    
    public void StopStartScreenMusic()
    {
        _startScreenMusic.Stop();
    }
    
    private void PlaySFX(AudioClip clip)
    {
        _sfxSource.pitch = Random.Range(0.75f, 1.25f);
            _sfxSource.PlayOneShot(clip);

    }
    
    public void SetMusicMute(bool mute)
    {
        if (_musicSource != null)
        {
            _musicSource.mute = mute;
        }
    }
    
    public void SetMusicVolume(float volume)
    {
        if (_musicSource != null)
        {
            _musicSource.volume = Mathf.Clamp01(volume);
        }
    }
    
    public void SetSFXMute(bool mute)
    {
        if (_sfxSource != null)
        {
            _sfxSource.mute = mute;
        }
    }
    
    public void SetSFXVolume(float volume)
    {
        if (_sfxSource != null)
        {
            _sfxSource.volume = Mathf.Clamp01(volume);
        }
    }
}
