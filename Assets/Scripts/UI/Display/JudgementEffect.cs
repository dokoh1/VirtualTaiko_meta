using System.Collections;
using UnityEngine;

public class JudgementEffect : MonoBehaviour
{
    private Animator animator;
    public BellAnimation bellAnimation;
    public GameObject Gold;
    public GameObject Silver;
    
    void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    //test
    // public IEnumerator EffectUpdate(TestEnum result)
    // {
    //     yield return null;
    //     if (result == TestEnum.Bad)
    //     {
    //         Debug.Log("Bad");
    //     }
    //     else if (result == TestEnum.GoodBig)
    //     {
    //         animator.SetTrigger("GoodBig");
    //     }
    //     else if (result == TestEnum.GoodSmall)
    //     {
    //         animator.SetTrigger("GoodSmall");
    //     }
    //     else if (result == TestEnum.PerfectBig)
    //     {
    //         animator.SetTrigger("PerfectBig");
    //     }
    //     else if (result == TestEnum.PerfectSmall)
    //     {
    //         animator.SetTrigger("PerfectSmall");
    //     }

    // }
    //execute
    public IEnumerator EffectUpdate(HitResult result)
    {
        yield return null;
        if (result == HitResult.Bad)
        {
            animator.SetTrigger("Bad");
        }
        else if (result == HitResult.BigGood)
        {
            animator.SetTrigger("GoodBig");
            bellAnimation.CreateBell(Silver);
        }
        else if (result == HitResult.SmallGood)
        {
            animator.SetTrigger("GoodSmall");
            bellAnimation.CreateBell(Silver);
        }
        else if (result == HitResult.BigPerfect)
        {
            animator.SetTrigger("PerfectBig");
            bellAnimation.CreateBell(Gold);
        }
        else if (result == HitResult.SmallPerfect)
        {
            animator.SetTrigger("PerfectSmall");
            bellAnimation.CreateBell(Gold);
        }

    }

}
