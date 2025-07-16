using System.Collections.Generic;
using UnityEngine;

public class FireworkAnimation : MonoBehaviour
{
    [SerializeField]
    private List<Animator> animators;

    private void OnEnable()
    {
        GameEvents.OnComboUpdated += HandleComboUpdated;
    }

    private void OnDisable()
    {
        GameEvents.OnComboUpdated -= HandleComboUpdated;
    }
    private void HandleComboUpdated(int combo)
    {
        Debug.Log("Do Combo Trigger");
        if (combo % 10 == 0 && combo != 0)
        {
            DoFireWork();
        }
    }
    private void DoFireWork()
    {
        foreach (var animator in animators)
            animator.SetTrigger("IsFireWork");
    }
}
