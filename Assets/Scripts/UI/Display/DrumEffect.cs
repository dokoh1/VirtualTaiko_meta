using System.Collections;
using UnityEngine;

public class DrumEffect : MonoBehaviour
{
   [SerializeField]
   private GameObject LeftSide;
   [SerializeField]
   private GameObject RightSide;
   [SerializeField]
   private GameObject LeftFace;
   [SerializeField]
   private GameObject RightFace;

   void OnEnable()
   {
       LeftSide.SetActive(false);
       RightSide.SetActive(false);
       LeftFace.SetActive(false);
       RightFace.SetActive(false);
   }

    public void DrumEffectInput(DrumDataType drumDataType)
    {
        if (drumDataType == DrumDataType.LeftSide)
            ShowDrumEffect(LeftSide);
        else if (drumDataType == DrumDataType.RightSide)
            ShowDrumEffect(RightSide);
        else if (drumDataType == DrumDataType.LeftFace)
            ShowDrumEffect(LeftFace);
        else if (drumDataType == DrumDataType.RightFace)
            ShowDrumEffect(RightFace);
        else if (drumDataType == DrumDataType.DobletFace)
            ShowDrumDoubleEffect(LeftFace, RightFace);
        else if (drumDataType == DrumDataType.Dobletside)
            ShowDrumDoubleEffect(LeftSide, RightSide);
    }

    private void ShowDrumEffect(GameObject drum)
    {
        StartCoroutine(FlashObject(drum));
    }
    private IEnumerator FlashObject(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        obj.SetActive(false);
    }

    private IEnumerator FlashObjects(GameObject obj, GameObject objTwo)
    {
        obj.SetActive(true);
        objTwo.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        obj.SetActive(false);
        objTwo.SetActive(false);
    }
    private void ShowDrumDoubleEffect(GameObject drum, GameObject drumTwo)
    {
        StartCoroutine(FlashObjects(drum, drumTwo));
    }
}
