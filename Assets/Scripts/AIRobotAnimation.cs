using UnityEngine;

public class AIRobotAnimation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayTalk()
    {
        if (anim != null)
        {
            anim.SetTrigger("Talk");
        }
    }

    public void PlayWave()
    {
        if (anim != null)
        {
            anim.SetTrigger("Wave");
        }
    }
}
