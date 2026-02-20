using UnityEngine;

public class DanceMusicTrigger : MonoBehaviour
{
    public Transform player; // assign your outside player
    public AudioSource musicSource;
    public float triggerDistance = 8f; // how close player must be

    private bool isPlaying = false;

    void Update()
    {
        if (player == null || musicSource == null) return;

        float distance = Vector3.Distance(player.position, transform.position);

        // When player comes close
        if (distance <= triggerDistance && !isPlaying)
        {
            musicSource.Play();
            isPlaying = true;
        }
        // When player goes away
        else if (distance > triggerDistance && isPlaying)
        {
            musicSource.Stop();
            isPlaying = false;
        }
    }
}
