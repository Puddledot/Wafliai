using System.Collections;
using TMPro;
using UnityEngine;

public class PickupCounter : MonoBehaviour
{
    [Header("Counter UI")]
    public TextMeshProUGUI counterText;

    [Header("Trigger")]
    [SerializeField] private int neededToTrigger = 1;

    [Header("Fade + Message UI")]
    [SerializeField] private CanvasGroup fadeCanvasGroup;      // FadePanel -> CanvasGroup
    [SerializeField] private TextMeshProUGUI warningText;      // DrugWarningText
    [SerializeField] private float fadeDuration = 1f;

    private int pickupCount = 0;
    private bool triggered = false;

    private void Start()
    {
        UpdateCounterText();

        if (fadeCanvasGroup != null)
            fadeCanvasGroup.alpha = 0f;

        if (warningText != null)
            warningText.gameObject.SetActive(false);
    }

    public void AddPickup()
    {
        pickupCount++;
        UpdateCounterText();

        if (!triggered && pickupCount >= neededToTrigger)
        {
            triggered = true;
            StartCoroutine(FadeToBlackAndShowMessage());
        }
    }

    private void UpdateCounterText()
    {
        if (counterText != null)
            counterText.text = "Picked up items: " + pickupCount;
    }

    private IEnumerator FadeToBlackAndShowMessage()
    {
        // jeigu nori sustabdyti žaidimą (garsas liks)
        // Time.timeScale = 0f;

        if (warningText != null)
        {
            warningText.text = "NEVARTOKIT NARKOTIKU";
            warningText.gameObject.SetActive(true);
        }

        if (fadeCanvasGroup == null) yield break;

        float t = 0f;
        float start = fadeCanvasGroup.alpha;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime; // veikia net jei timeScale=0
            fadeCanvasGroup.alpha = Mathf.Lerp(start, 1f, t / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = 1f;
    }
}