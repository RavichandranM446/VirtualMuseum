using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject outsidePlayer;
    public GameObject insidePlayer;
    public Camera outsideCam;
    public Camera insideCam;

    private bool isInside = false;

    void Start()
    {
        insidePlayer.SetActive(false);
        insideCam.gameObject.SetActive(false);
        outsidePlayer.SetActive(true);
        outsideCam.gameObject.SetActive(true);
    }

    void Update()
    {
        // Press 'E' near the door to switch
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchPlayers();
        }
    }

    void SwitchPlayers()
    {
        isInside = !isInside;

        outsidePlayer.SetActive(!isInside);
        outsideCam.gameObject.SetActive(!isInside);

        insidePlayer.SetActive(isInside);
        insideCam.gameObject.SetActive(isInside);
    }
}
