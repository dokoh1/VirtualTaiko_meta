using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private bool IsThirldIdle;
    private bool IsSecondIdle;

    private Animator animator;
    
    private readonly int HashSecondIdle = Animator.StringToHash("IsSecondIdle");
    private readonly int HashThirdIdle = Animator.StringToHash("IsThirdIdle");
    private void Awake()
    {
        animator = GetComponent<Animator>();
        IsThirldIdle = false;
        IsSecondIdle = false;
    }

    public void UpdateAnimator(int currentHit)
    {
        if (currentHit == 0)
        {
            IsThirldIdle = false;
            IsSecondIdle = false;
            animator.SetBool(HashThirdIdle, false);
            animator.SetBool(HashSecondIdle, false);
        }

        if (currentHit == 100)
        {
            IsThirldIdle = true;
            animator.SetBool(HashThirdIdle, true);
        }
        else if (currentHit == 50)
        {
            IsSecondIdle = true;
            animator.SetBool(HashSecondIdle, true);
        }

        if (IsSecondIdle)
        {
            if (currentHit % 10 == 0 && currentHit != 0)
            {
                animator.SetTrigger("IsSecondAction");
            }
        }
        else if (!IsSecondIdle)
        {
            if (currentHit % 10 == 0 && currentHit != 0)
            {
                animator.SetTrigger("IsFirstAction");
            }
        }
    }
}
