using UnityEngine;

public class FireAnimation : MonoBehaviour
{
   private Animator _animator;
   private readonly int _hashIsFire = Animator.StringToHash("IsFire");
   
   private void Awake()
   {
      _animator = GetComponent<Animator>();
   }
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
      if (combo == (int)ComboState.ComboInit)
         _animator.SetBool(_hashIsFire, false);
      else if (combo == (int)ComboState.Combo10)
         _animator.SetBool(_hashIsFire, true);
   }
}


