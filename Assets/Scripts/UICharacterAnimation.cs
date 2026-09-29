using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UICharacterAnimation : MonoBehaviour
{
    [Header("Idle Squash")]
    public bool enableIdleAnimation = true;
    public float squashAmount = 0.06f;
    public float squashSpeed = 2.5f;

    [Header("Damage Flash")]
    public Color damageColor = Color.red;
    public float damageFlashDuration = 0.15f;

    private RectTransform rectTransform;
    private Image image;

    private Vector3 originalScale;
    private Color originalColor;

    private Coroutine damageCoroutine;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        originalScale = rectTransform.localScale;
        originalColor = image.color;
    }

    private void Update()
    {
        if (!enableIdleAnimation)
            return;

        float squash = Mathf.Sin(Time.unscaledTime * squashSpeed);

        float scaleX = 1f + squash * squashAmount;
        float scaleY = 1f - squash * squashAmount;

        rectTransform.localScale = new Vector3(
            originalScale.x * scaleX,
            originalScale.y * scaleY,
            originalScale.z
        );
    }

    public void PlayDamageFlash()
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }

        damageCoroutine = StartCoroutine(DamageFlash());
    }

    private IEnumerator DamageFlash()
    {
        image.color = damageColor;

        yield return new WaitForSecondsRealtime(damageFlashDuration);

        image.color = originalColor;

        damageCoroutine = null;
    }

    public void ResetAnimation()
    {
        rectTransform.localScale = originalScale;
        image.color = originalColor;
    }
}