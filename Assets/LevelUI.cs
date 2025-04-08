using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    public TextMeshProUGUI levelText;         // für Ein-/Ausblendung
    public TextMeshProUGUI levelCounterText;  // für permanente Anzeige
    public CanvasGroup canvasGroup;
    public float fadeDuration = 0.5f;
    public float displayTime = 2f;

    private Coroutine showRoutine;

    public void ShowLevel(int level)
    {
        // Aktualisiere permanenten Text
        if (levelCounterText != null)
            levelCounterText.text = "Level " + level;

        // Zeige große Einblendung wie bisher
        ShowMessage("Level " + level);
    }

    public void ShowMessage(string message)
    {
        if (showRoutine != null)
            StopCoroutine(showRoutine);

        showRoutine = StartCoroutine(ShowAndFade(message));
    }

    private System.Collections.IEnumerator ShowAndFade(string message)
    {
        levelText.text = message;
        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(true);

        // Fade-In
        float t = 0f;
        while (t < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            t += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(displayTime);

        // Fade-Out
        t = 0f;
        while (t < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            t += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.gameObject.SetActive(false);
    }
}
