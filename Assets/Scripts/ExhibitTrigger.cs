using UnityEngine;
using TMPro;
using System.Collections;

public class ExhibitTrigger : MonoBehaviour
{
    [TextArea(3, 5)]
    public string exhibitInfo = "This is an ancient artwork from the 18th century.";

    public GameObject popupPanel;
    public TMP_Text popupText;
    public AudioSource aiVoice;
    public AudioClip exhibitClip;

    private CanvasGroup canvasGroup;
    public float fadeDuration = 0.5f;

    void Start()
    {
        canvasGroup = popupPanel.GetComponent<CanvasGroup>();
        popupPanel.SetActive(false);
        canvasGroup.alpha = 0f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            popupText.text = exhibitInfo;
            popupPanel.SetActive(true);

            StopAllCoroutines();
            StartCoroutine(FadeCanvas(1f));

            if (aiVoice != null && exhibitClip != null)
            {
                aiVoice.Stop();
                aiVoice.clip = exhibitClip;
                aiVoice.Play();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(FadeCanvas(0f));

            if (aiVoice != null) aiVoice.Stop();
        }
    }

    IEnumerator FadeCanvas(float target)
    {
        float start = canvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, target, elapsed / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = target;

        if (target == 0f) popupPanel.SetActive(false);
    }
}
