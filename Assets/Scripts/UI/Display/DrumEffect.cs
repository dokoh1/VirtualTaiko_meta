using System.Collections;
using UnityEngine;

public class DrumEffect : MonoBehaviour
{
    [SerializeField]
    private TestDrumInput testDrumInput;
    private Drums drums;
    [SerializeField]
    private DrumSide drumsSide;
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
        //TestCode
        if (testDrumInput.testInputType == DrumDataType.LeftSide)
            ShowDrumEffect(LeftSide);
        else if (testDrumInput.testInputType == DrumDataType.RightSide)
            ShowDrumEffect(RightSide);
        else if (testDrumInput.testInputType == DrumDataType.LeftFace)
            ShowDrumEffect(LeftFace);
        else if (testDrumInput.testInputType == DrumDataType.RightFace)
            ShowDrumEffect(RightFace);
        
        //ExecuteCode
        // if (drumsSide.dataSet == DrumDataType.LeftSide)
        //     ShowDrumEffect(LeftSide);
        // else if (drumsSide.dataSet == DrumDataType.RightSide)
        //     ShowDrumEffect(RightSide);
        // else if (drums.dataSet == DrumDataType.LeftFace)
        //     ShowDrumEffect(LeftFace);
        // else if (drums.dataSet == DrumDataType.RightFace)
        //     ShowDrumEffect(RightFace);
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
