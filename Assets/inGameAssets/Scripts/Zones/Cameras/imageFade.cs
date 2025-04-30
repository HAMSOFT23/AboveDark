using UnityEngine;
using System.Collections;

public class SpriteFader : MonoBehaviour // Ensure filename matches: SpriteFader.cs
{
    [Header("Target")]
    [SerializeField]
    [Tooltip("The Sprite Renderer component on the target GameObject to fade. Drag the GameObject with the Sprite Renderer here.")]
    private SpriteRenderer spriteToFade; // Assign this in the Inspector

    [Header("Fade Values")]
    [SerializeField]
    [Range(0f, 1f)]
    [Tooltip("The alpha value when faded OUT (usually 0 for fully transparent).")]
    private float defaultFadeValue = 0f;

    [SerializeField]
    [Range(0f, 1f)]
    [Tooltip("The alpha value when faded IN (usually 1 for fully opaque).")]
    private float transitionFadeValue = 1f;

    // Combine Durations and Delay under one header for clarity
    [Header("Transition Timing")]
    [SerializeField]
    [Min(0.01f)]
    [Tooltip("The time in seconds the fade IN effect should last.")]
    private float fadeInDuration = 0.5f;

    [SerializeField]
    [Min(0.01f)]
    [Tooltip("The time in seconds the fade OUT effect should last.")]
    private float fadeOutDuration = 0.5f;

    // --- NEW VARIABLE FOR DELAY ---
    [SerializeField]
    [Min(0f)] // Allow zero delay
    [Tooltip("Delay in seconds between fade in completing and fade out starting during a full transition.")]
    private float transitionDelay = 0f; // Default to no delay
    // --- END NEW VARIABLE ---


    // To keep track of the currently running fade coroutine
    private Coroutine _activeFadeCoroutine = null;
    // To keep track of the master transition coroutine
    private Coroutine _masterTransitionCoroutine = null;

    // Called when the script instance is first loaded
    void Awake()
    {
        if (spriteToFade == null)
        {
            Debug.LogError("SpriteFader: The 'Sprite To Fade' field is not assigned in the Inspector.", this);
            enabled = false;
            return;
        }
        SetAlpha(defaultFadeValue);
    }

    // --- Public Methods to Trigger Fades ---
    public void FadeIn()
    {
        StopMasterTransition();
        if (spriteToFade != null)
        {
            // Debug.Log("Fading In"); // Keep logs minimal unless debugging
            StartFade(spriteToFade.color.a, transitionFadeValue, fadeInDuration);
        }
    }

    public void FadeOut()
    {
        StopMasterTransition();
        if (spriteToFade != null)
        {
            // Debug.Log("Fading Out"); // Keep logs minimal unless debugging
            StartFade(spriteToFade.color.a, defaultFadeValue, fadeOutDuration);
        }
    }

    public void PerformFullTransition()
    {
        StopMasterTransition();
        StopActiveFade();
        _masterTransitionCoroutine = StartCoroutine(FullTransitionRoutine());
    }


    // --- Helper method to manage starting the individual fade coroutine ---
    private void StartFade(float startAlpha, float targetAlpha, float duration)
    {
        StopActiveFade();

        if (spriteToFade != null)
        {
             _activeFadeCoroutine = StartCoroutine(FadeRoutine(startAlpha, targetAlpha, duration));
        }
        else
        {
            Debug.LogWarning("SpriteFader: Cannot start fade because target Sprite Renderer is null.", this);
        }
    }

    // --- Helper method to stop the active individual fade ---
    private void StopActiveFade()
    {
        if (_activeFadeCoroutine != null)
        {
            StopCoroutine(_activeFadeCoroutine);
            _activeFadeCoroutine = null;
        }
    }

     // --- Helper method to stop the master transition ---
    private void StopMasterTransition()
    {
         if(_masterTransitionCoroutine != null)
         {
            StopCoroutine(_masterTransitionCoroutine);
            _masterTransitionCoroutine = null;
         }
    }


    // --- The Coroutine that performs the individual fade step over time ---
    private IEnumerator FadeRoutine(float startAlpha, float targetAlpha, float duration)
    {
        if (duration <= 0f)
        {
            SetAlpha(targetAlpha);
            _activeFadeCoroutine = null;
            yield break;
        }

        float elapsedTime = 0f;
        // Optional Debugging: Log start time if needed
        // Debug.Log($"Starting FadeRoutine: Start={startAlpha:F3}, Target={targetAlpha:F3}, Duration={duration:F3}, Time={Time.time:F3}");

        while (elapsedTime < duration)
        {
            if (spriteToFade == null)
            {
                 Debug.LogWarning("SpriteFader: Target Sprite Renderer became null during fade.", this);
                 _activeFadeCoroutine = null;
                 yield break;
            }

            elapsedTime += Time.deltaTime;
            float timeRatio = Mathf.Clamp01(elapsedTime / duration);
            float currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, timeRatio);
            // Optional Debugging: Log progress if needed
            // Debug.Log($"FadeRoutine: t={elapsedTime:F3}, ratio={timeRatio:F3}, currentAlpha={currentAlpha:F3}");
            SetAlpha(currentAlpha);
            yield return null;
        }

        if (spriteToFade != null)
        {
            SetAlpha(targetAlpha);
        }
        // Optional Debugging: Log end time if needed
        // Debug.Log($"Ending FadeRoutine: Final Alpha={targetAlpha:F3}, Time={Time.time:F3}");
        _activeFadeCoroutine = null;
    }

    // --- Coroutine for the full Fade In -> Delay -> Fade Out sequence ---
    private IEnumerator FullTransitionRoutine()
    {
        // Debug.Log("Starting Full Transition..."); // Keep logs minimal unless debugging

        // --- Fade In Phase ---
        StartFade(defaultFadeValue, transitionFadeValue, fadeInDuration);
        while (_activeFadeCoroutine != null)
        {
            yield return null; // Wait frame by frame for fade-in to complete
        }
        // Debug.Log("Full Transition: Fade In Complete."); // Keep logs minimal unless debugging

        // --- Delay Phase ---
        if (transitionDelay > 0f) // Only delay if value is positive
        {
            // Debug.Log($"Full Transition: Starting delay of {transitionDelay} seconds."); // Keep logs minimal unless debugging
            yield return new WaitForSeconds(transitionDelay); // Wait for the specified delay
            // Debug.Log("Full Transition: Delay complete."); // Keep logs minimal unless debugging
        }
        // --- End Delay Phase ---


        // Check if target still exists after potential delay
        if (spriteToFade == null)
        {
            Debug.LogWarning("SpriteFader: Target lost before/during Fade Out phase.", this);
            _masterTransitionCoroutine = null;
            yield break;
        }

        // --- Fade Out Phase ---
        StartFade(transitionFadeValue, defaultFadeValue, fadeOutDuration);
        while (_activeFadeCoroutine != null)
        {
            yield return null; // Wait frame by frame for fade-out to complete
        }

        // Debug.Log("Full Transition: Fade Out Complete."); // Keep logs minimal unless debugging
        _masterTransitionCoroutine = null;
    }


    // --- Helper method to set the alpha of the target sprite ---
    private void SetAlpha(float alpha)
    {
        if (spriteToFade == null) return;
        Color currentColor = spriteToFade.color;
        currentColor.a = Mathf.Clamp01(alpha);
        spriteToFade.color = currentColor;
    }

    // --- Optional: Methods to instantly set the state ---
    public void SetToDefault()
    {
        StopMasterTransition();
        StopActiveFade();
        SetAlpha(defaultFadeValue);
    }

    public void SetToTransition()
    {
        StopMasterTransition();
        StopActiveFade();
        SetAlpha(transitionFadeValue);
    }
}