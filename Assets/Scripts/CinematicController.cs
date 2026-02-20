using System.Collections;
using UnityEngine;
using Cinemachine;

public class CinematicController : MonoBehaviour
{
    [Header("Cinemachine")]
    public CinemachineDollyCart dollyCart;
    public CinemachineVirtualCamera cineVcam;
    public CinemachineVirtualCamera gameplayVcam;
    public float cinematicSpeed = 6f;

    [Header("Timing")]
    public float duration = 6f;
    public KeyCode skipKey = KeyCode.Space;

    [Header("UI")]
    public CanvasGroup fadePanelGroup;
    public CanvasGroup titleGroup;
    public CanvasGroup createdByGroup;

    [Header("Player")]
    public GameObject playerRoot;

    private bool skipped = false;

    void Start()
    {
        // Fade Panel visible fully
        if (fadePanelGroup != null)
        {
            fadePanelGroup.alpha = 1f;
            fadePanelGroup.gameObject.SetActive(true);
        }

        // Hide UI texts initially
        if (titleGroup != null) titleGroup.alpha = 0f;
        if (createdByGroup != null) createdByGroup.alpha = 0f;

        // Camera priorities
        gameplayVcam.Priority = 10;
        cineVcam.Priority = 100;

        // Dolly stays still until cinematic begins
        dollyCart.m_Speed = 0f;

        StartCoroutine(RunCinematic());
    }

    void Update()
    {
        if (Input.GetKeyDown(skipKey))
        {
            skipped = true;
        }
    }

    IEnumerator RunCinematic()
    {
        // Disable player movement
        TogglePlayerMovement(false);

        // Fade from black ? clear
        if (fadePanelGroup != null)
            yield return StartCoroutine(Fade(fadePanelGroup, 1f, 0f, 1f));

        // ------------------------------
        //  CREATED BY TEXT SEQUENCE
        // ------------------------------
        if (createdByGroup != null)
            yield return StartCoroutine(Fade(createdByGroup, 0f, 1f, 1f)); // fade in

        yield return new WaitForSeconds(1.5f);

        if (createdByGroup != null)
            yield return StartCoroutine(Fade(createdByGroup, 1f, 0f, 1f)); // fade out

        // ------------------------------
        //  TITLE SEQUENCE
        // ------------------------------
        if (titleGroup != null)
            yield return StartCoroutine(Fade(titleGroup, 0f, 1f, 1f)); // fade in title

        yield return new WaitForSeconds(1.5f);

        if (titleGroup != null)
            yield return StartCoroutine(Fade(titleGroup, 1f, 0f, 1f)); // fade out title

        // ------------------------------
        // Start Dolly movement
        // ------------------------------
        dollyCart.m_Speed = cinematicSpeed;

        float t = 0f;
        while (t < duration && !skipped)
        {
            t += Time.deltaTime;
            yield return null;
        }

        // Stop movement
        dollyCart.m_Speed = 0f;

        // Switch cameras
        cineVcam.Priority = 10;
        gameplayVcam.Priority = 100;

        // Enable player movement
        TogglePlayerMovement(true);
    }

    IEnumerator Fade(CanvasGroup cg, float from, float to, float time)
    {
        float elapsed = 0f;
        cg.alpha = from;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(from, to, elapsed / time);
            yield return null;
        }

        cg.alpha = to;
    }

    void TogglePlayerMovement(bool enable)
    {
        if (playerRoot == null) return;

        var scripts = playerRoot.GetComponentsInChildren<MonoBehaviour>();

        foreach (var s in scripts)
        {
            if (s != null && s.GetType().Name == "PlayerMovement")
            {
                s.enabled = enable;
            }
        }
    }
}
