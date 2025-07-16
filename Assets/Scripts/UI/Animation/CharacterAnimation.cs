using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator _animator;
    
    private readonly int _hashSecondIdle = Animator.StringToHash("IsSecondIdle");
    private readonly int _hashThirdIdle = Animator.StringToHash("IsThirdIdle");
    private readonly int _hashFirstAction = Animator.StringToHash("IsFirstAction");
    private readonly int _hashSecondAction = Animator.StringToHash("IsSecondAction");
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        GameEvents.OnComboUpdated += HandleComboUpdated;
    }

    private void HandleComboUpdated(int currentHit)
    {
        ResetIdleStatesIfNeeded(currentHit);
        
        if (currentHit == (int)ComboState.Combo50)
            _animator.SetBool(_hashThirdIdle, true);
        else if (currentHit == (int)ComboState.Combo100)
            _animator.SetBool(_hashSecondIdle, true);

        if (ShouldTriggerAction(currentHit))
        {
            if (IsSecondIdle())
                _animator.SetTrigger(_hashSecondAction);
            else
                _animator.SetTrigger(_hashFirstAction);
        }
    }

    private void ResetIdleStatesIfNeeded(int currentHit)
    {
        if (currentHit == 0)
        {
            _animator.SetBool(_hashThirdIdle, false);
            _animator.SetBool(_hashSecondIdle, false);
        }
    }
    
    private bool IsSecondIdle()
    {
        return _animator.GetBool(_hashSecondIdle);
    }

    private bool ShouldTriggerAction(int currentHit)
    {
        return currentHit != 0 && currentHit % 10 == 0;
    }
}

