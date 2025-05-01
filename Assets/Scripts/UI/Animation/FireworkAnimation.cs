using System.Collections.Generic;
using UnityEngine;

public class FireworkAnimation : MonoBehaviour
{
    [SerializeField]
    private List<Animator> animators;

    public void DoFireWork()
    {
        foreach (var animator in animators)
        {
            animator.SetTrigger("IsFireWork");
        }
    }
}
