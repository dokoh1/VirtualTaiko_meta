using UnityEngine;

public class FireAnimation : MonoBehaviour
{
   private Animator _animator;
   private readonly int _hashIsFire = Animator.StringToHash("IsFire");

   private void Awake()
   {
      _animator = GetComponent<Animator>();
   }

   public void SetIsFire(int comboHit)
   {
      if (comboHit == (int)ComboState.ComboInit)
         _animator.SetBool(_hashIsFire, false);
      else if (comboHit == (int)ComboState.Combo10)
         _animator.SetBool(_hashIsFire, true);
   }
}


