using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Audio Clips")]
    public AudioClip buttonClickSFX;
    public AudioClip backgroundMusic;

    [Header("Volume Settings")]
    [Range(0f, 1f)]
    public float bgmVolume = 0.5f;

    [Range(0f, 1f)]
    public float sfxVolume = 0.8f;

    private AudioSource bgmSource;
    private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.loop = true;
            bgmSource.playOnAwake = false;

            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (backgroundMusic != null)
        {
            bgmSource.clip = backgroundMusic;
            bgmSource.volume = bgmVolume;
            bgmSource.Play();
        }
    }

    public void PlayButtonSFX()
    {
        if (buttonClickSFX != null)
        {
            sfxSource.PlayOneShot(buttonClickSFX, sfxVolume);
        }
    }
}
