using UnityEngine;

public class MainMenuMusicManager : MonoBehaviour
{
    public static MainMenuMusicManager Instance;

    [Header("Audio Clips")]
    public AudioClip buttonClickSFX;
    public AudioClip buttonPopSFX;
    public AudioClip backgroundMusic;

    [Header("Individual Audio Gain")]
    [Range(0f, 2f)]
    public float buttonClickVolume = 1f;

    [Range(0f, 2f)]
    public float buttonPopVolume = 1f;

    [Range(0f, 2f)]
    public float backgroundMusicVolume = 1f;

    private AudioSource bgmSource;
    private AudioSource sfxSource;

    private void Awake()
    {
        Instance = this;

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;
    }

    private void Start()
    {
        if (backgroundMusic != null)
        {
            bgmSource.clip = backgroundMusic;
            bgmSource.volume = backgroundMusicVolume;
            bgmSource.Play();
        }
    }

    // -------------------------
    // BUTTON SFX
    // -------------------------

    public void PlayButtonSFX()
    {
        if (buttonClickSFX != null)
        {
            sfxSource.PlayOneShot(
                buttonClickSFX,
                buttonClickVolume
            );
        }
    }

    public void PlayButtonPopSFX()
    {
        if (buttonPopSFX != null)
        {
            sfxSource.PlayOneShot(
                buttonPopSFX,
                buttonPopVolume
            );
        }
    }
}