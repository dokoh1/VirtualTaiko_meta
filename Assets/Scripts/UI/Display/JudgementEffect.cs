using System.Collections;
using UnityEngine;

public class JudgementEffect : MonoBehaviour
{
    private Animator animator;
    void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public IEnumerator EffectUpdate(TestEnum result)
    {
        yield return null;
        if (result == TestEnum.Bad)
        {
            Debug.Log("Bad");
        }
        else if (result == TestEnum.GoodBig)
        {
            animator.SetTrigger("GoodBig");
        }
        else if (result == TestEnum.GoodSmall)
        {
            animator.SetTrigger("GoodSmall");
        }
        else if (result == TestEnum.PerfectBig)
        {
            animator.SetTrigger("PerfectBig");
        }
        else if (result == TestEnum.PerfectSmall)
        {
            animator.SetTrigger("PerfectSmall");
        }

    }
}
