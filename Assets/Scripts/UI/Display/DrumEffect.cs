using System.Collections;
using UnityEngine;

public class DrumEffect : MonoBehaviour
{
    [SerializeField]
   private TestDrumInput testDrumInput;
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

    // Update is called once per frame
    void Update()
    {
        if (testDrumInput.testInputType == DrumInputDataType.LeftSide)
            ShowDrumEffect(LeftSide);
        else if (testDrumInput.testInputType == DrumInputDataType.RightSide)
            ShowDrumEffect(RightSide);
        else if (testDrumInput.testInputType == DrumInputDataType.LeftFace)
            ShowDrumEffect(LeftFace);
        else if (testDrumInput.testInputType == DrumInputDataType.RightFace)
            ShowDrumEffect(RightFace);
    }

    private void ShowDrumEffect(GameObject drum)
    {
        StartCoroutine(FlashObject(drum));
    }
    private IEnumerator FlashObject(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(0.2f); // 0.2초 대기
        obj.SetActive(false);
    }
}
