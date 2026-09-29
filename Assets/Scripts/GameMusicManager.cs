using UnityEngine;

public class GameMusicManager : MonoBehaviour
{
    public static GameMusicManager Instance;

    [Header("Audio Clips")]
    public AudioClip buttonClickSFX;
    public AudioClip buttonPopSFX;
    public AudioClip playerHurtSFX;
    public AudioClip enemyHurtSFX;
    public AudioClip backgroundMusic;
    public AudioClip hintSFX;

    [Header("Individual Audio Gain")]
    [Range(0f, 2f)]
    public float buttonClickVolume = 1f;

    [Range(0f, 2f)]
    public float buttonPopVolume = 1f;

    [Range(0f, 2f)]
    public float playerHurtVolume = 1f;

    [Range(0f, 2f)]
    public float enemyHurtVolume = 1f;

    [Range(0f, 2f)]
    public float hintVolume = 1f;

    [Range(0f, 2f)]
    public float backgroundMusicVolume = 1f;

    private AudioSource bgmSource;
    private AudioSource sfxSource;

    private bool bgmPaused = false;

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

    // -------------------------
    // HURT SFX
    // -------------------------

    public void PlayPlayerHurtSFX()
    {
        if (playerHurtSFX != null)
        {
            sfxSource.PlayOneShot(
                playerHurtSFX,
                playerHurtVolume
            );
        }
    }

    public void PlayEnemyHurtSFX()
    {
        if (enemyHurtSFX != null)
        {
            sfxSource.PlayOneShot(
                enemyHurtSFX,
                enemyHurtVolume
            );
        }
    }

    // -------------------------
    // HINT SFX
    // -------------------------

    public void PlayHintSFX()
    {
        if (hintSFX != null)
        {
            sfxSource.PlayOneShot(
                hintSFX,
                hintVolume
            );
        }
    }

    // -------------------------
    // BGM CONTROL
    // -------------------------

    public void PauseBGM()
    {
        if (bgmSource != null && bgmSource.isPlaying)
        {
            bgmSource.Pause();
            bgmPaused = true;
        }
    }

    public void ResumeBGM()
    {
        if (bgmSource != null && bgmPaused)
        {
            bgmSource.UnPause();
            bgmPaused = false;
        }
    }
}