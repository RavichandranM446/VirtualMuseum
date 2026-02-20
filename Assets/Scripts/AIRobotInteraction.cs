using UnityEngine;
using TMPro;

public class AIRobotInteraction : MonoBehaviour
{
    public TMP_Text chatText;       // UI text to show AI response
    public float displayTime = 4f;  // How long message shows
    public Animator anim;           // Robot Animator

    [System.Serializable]
    public struct QA
    {
        public string playerKeyword; // keyword player can trigger
        public string response;      // robot response text
    }

    public QA[] responses;           // Array of built-in responses

    private bool playerNear = false;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.D)) // Press E to interact
        {
            GiveResponse();
        }
    }

    void GiveResponse()
    {
        // Pick a random response from built-in responses
        if (responses.Length == 0) return;

        int index = Random.Range(0, responses.Length);
        chatText.text = responses[index].response;

        // Play Talk animation
        if (anim != null)
        {
            anim.SetTrigger("Talk");
        }

        // Optional: stop message after some time
        Invoke("ClearText", displayTime);
    }

    void ClearText()
    {
        chatText.text = "";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNear = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            chatText.text = "";
        }
    }
}
