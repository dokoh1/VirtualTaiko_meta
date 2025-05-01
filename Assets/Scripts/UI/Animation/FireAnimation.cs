using UnityEngine;

public class FireAnimation : MonoBehaviour
{
   private Animator animator;
   private readonly int HashIsFire = Animator.StringToHash("IsFire");

   private void Awake()
   {
      animator = GetComponent<Animator>();
   }

   public void SetIsFire(int Combohit)
   {
      if (Combohit == 0)
      {
         animator.SetBool(HashIsFire, false);
      }
      else if (Combohit == 30)
      {
         animator.SetBool(HashIsFire, true);
      }
   }



}
