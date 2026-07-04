using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameFeedback : MonoBehaviour
{
    public static GameFeedback Instance { get; private set; }

    [Header("Hit Flash")]
    [SerializeField] private Image damageFlashImage;
    [SerializeField] private float flashDuration = 0.18f;
    [SerializeField] private float flashMaxAlpha = 0.35f;

    [Header("Counter Pop")]
    [SerializeField] private RectTransform collectibleCounter;
    [SerializeField] private float popScale = 1.25f;
    [SerializeField] private float popDuration = 0.15f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip collectClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private bool generateTemporarySounds = true;

    private Coroutine flashCoroutine;
    private Coroutine popCoroutine;
    private Vector3 counterOriginalScale = Vector3.one;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        if (collectibleCounter != null)
        {
            counterOriginalScale = collectibleCounter.localScale;
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (generateTemporarySounds)
        {
            if (collectClip == null)
            {
                collectClip = GenerateTone(880f, 0.08f, 0.18f);
            }

            if (hitClip == null)
            {
                hitClip = GenerateTone(140f, 0.12f, 0.25f);
            }
        }

        if (damageFlashImage != null)
        {
            SetFlashAlpha(0f);
        }
    }

    public void PlayCollectFeedback()
    {
        PlaySound(collectClip);
        PopCounter();
    }

    public void PlayHitFeedback()
    {
        PlaySound(hitClip);
        FlashDamage();
    }

    private void FlashDamage()
    {
        if (damageFlashImage == null) return;

        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        flashCoroutine = StartCoroutine(FlashDamageRoutine());
    }

    private IEnumerator FlashDamageRoutine()
    {
        float elapsed = 0f;

        SetFlashAlpha(flashMaxAlpha);

        while (elapsed < flashDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / flashDuration;
            float alpha = Mathf.Lerp(flashMaxAlpha, 0f, t);
            SetFlashAlpha(alpha);
            yield return null;
        }

        SetFlashAlpha(0f);
        flashCoroutine = null;
    }

    private void SetFlashAlpha(float alpha)
    {
        Color color = damageFlashImage.color;
        color.a = alpha;
        damageFlashImage.color = color;
    }

    private void PopCounter()
    {
        if (collectibleCounter == null) return;

        if (popCoroutine != null)
        {
            StopCoroutine(popCoroutine);
        }

        popCoroutine = StartCoroutine(PopCounterRoutine());
    }

    private IEnumerator PopCounterRoutine()
    {
        float halfDuration = popDuration / 2f;
        float elapsed = 0f;

        Vector3 targetScale = counterOriginalScale * popScale;

        while (elapsed < halfDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / halfDuration;
            collectibleCounter.localScale = Vector3.Lerp(counterOriginalScale, targetScale, t);
            yield return null;
        }

        elapsed = 0f;

        while (elapsed < halfDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / halfDuration;
            collectibleCounter.localScale = Vector3.Lerp(targetScale, counterOriginalScale, t);
            yield return null;
        }

        collectibleCounter.localScale = counterOriginalScale;
        popCoroutine = null;
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource == null || clip == null) return;
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GenerateTone(float frequency, float duration, float volume)
    {
        int sampleRate = 44100;
        int sampleCount = Mathf.CeilToInt(sampleRate * duration);
        float[] samples = new float[sampleCount];

        for (int i = 0; i < sampleCount; i++)
        {
            float time = (float)i / sampleRate;
            float fadeOut = 1f - ((float)i / sampleCount);
            samples[i] = Mathf.Sin(2f * Mathf.PI * frequency * time) * volume * fadeOut;
        }

        AudioClip clip = AudioClip.Create("TemporaryFeedbackSound", sampleCount, 1, sampleRate, false);
        clip.SetData(samples, 0);
        return clip;
    }
}
