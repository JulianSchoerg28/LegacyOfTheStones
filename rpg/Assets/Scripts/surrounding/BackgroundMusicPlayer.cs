using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    public static BackgroundMusicPlayer Instance;

    public AudioClip[] songs;
    private AudioSource audioSource;
    private int currentSongIndex = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    /*void Start()
    {
        if (audioSource == null) return;

        audioSource.volume = SettingsManager.Instance.Volume;
        audioSource.mute = SettingsManager.Instance.IsMuted;

        PlayNextSong();
    }*/

    
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.volume = SettingsManager.Instance.Volume;
            audioSource.mute = SettingsManager.Instance.IsMuted;
        }
    }

    
    void Update()
    {
        if (audioSource == null) return;

        if (!audioSource.isPlaying)
        {
            PlayNextSong();
        }

        audioSource.volume = SettingsManager.Instance.Volume;
        audioSource.mute = SettingsManager.Instance.IsMuted;
    }

    void PlayNextSong()
    {
        if (songs.Length == 0 || audioSource == null) return;

        audioSource.clip = songs[currentSongIndex];
        audioSource.Play();

        currentSongIndex = (currentSongIndex + 1) % songs.Length;
    }
}