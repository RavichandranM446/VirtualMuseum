using UnityEngine;
using TMPro;

public class AIChatManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject chatPanel;
    public TMP_InputField chatInput;
    public TMP_Text chatHistory;

    [Header("Robot")]
    public Animator robotAnim;

    bool playerInside = false;

    void Start()
    {
        chatPanel.SetActive(false);

        // Prevent old texts at start
        chatHistory.text = "";
    }

    void Update()
    {
        // Press Enter to send message
        if (playerInside && Input.GetKeyDown(KeyCode.Return))
        {
            OnSendMessage();
        }
    }

    public void OnSendMessage()
    {
        string playerText = chatInput.text.Trim();

        if (playerText.Length == 0)
            return;

        // Add Player message
        chatHistory.text += "\n<color=yellow>You:</color> " + playerText;

        // Robot reply
        string reply = GetAIReply(playerText);
        chatHistory.text += "AI Robot: " + reply;

        chatInput.text = "";
        chatInput.ActivateInputField();

        // Robot Talk Animation
        if (robotAnim != null)
            robotAnim.SetTrigger("Talk");
    }

    // ? FIXED: CLEAR CHAT FUNCTION
    public void ClearChat()
    {
        Debug.Log("CLEAR PRESSED");   // Debug check
        chatHistory.text = "";
    }

    string GetAIReply(string question)
    {
        question = question.ToLower();

        if (question.Contains("hi") || question.Contains("hello"))
            return "Hello! Welcome to the AI Virtual Museum.";

        if (question.Contains("statue"))
            return "This statue represents ancient Greek heritage from 450 BC.";

        if (question.Contains("egypt"))
            return "Ancient Egypt is known for pyramids, pharaohs, and hieroglyphics.";

        if (question.Contains("museum"))
            return "This museum displays art, culture, and history.";

        if (question.Contains("who are you"))
            return "I am the AI tour guide, ready to assist you.";

        if (question.Contains("art"))
            return "This artwork was inspired by Renaissance-era techniques.";

        return "That's interesting! This exhibit has a rich cultural story.";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            chatPanel.SetActive(true);

            // UNLOCK CURSOR FOR UI
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            chatPanel.SetActive(false);

            // LOCK CURSOR BACK FOR GAMEPLAY
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
