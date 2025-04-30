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
    public IEnumerator EffectUpdate(TestEnum result)
    {
        yield return null;
        if (result == TestEnum.Bad)
        {
            animator.SetTrigger("Bad");
        }
        else if (result == TestEnum.GoodBig)
        {
            animator.SetTrigger("GoodBig");
            bellAnimation.CreateBell(Silver);
        }
        else if (result == TestEnum.GoodSmall)
        {
            animator.SetTrigger("GoodSmall");
            bellAnimation.CreateBell(Silver);
        }
        else if (result == TestEnum.PerfectBig)
        {
            animator.SetTrigger("PerfectBig");
            bellAnimation.CreateBell(Gold);
        }
        else if (result == TestEnum.PerfectSmall)
        {
            animator.SetTrigger("PerfectSmall");
            bellAnimation.CreateBell(Gold);
        }

    }
}
