using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public CameraSwitcher camSwitcher;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                camSwitcher.SendMessage("SwitchPlayers");
            }
        }
    }
}
