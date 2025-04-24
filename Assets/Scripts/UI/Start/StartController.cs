using UnityEngine;

public class StartController : MonoBehaviour
{
    // public Drums DrumFace;
    // public DrumSide DrumSide;
    public TestDrumInput Input;
    void Update()
    {
        //Execute Code
        // if (DrumFace.dataSet == DrumDataType.RightFace)
        // {
        //     dokoh.System.SceneManager.LoadScene(SceneDataType.MusicChoice);
        // }
        
        //Test Code
        if (Input.testInputType == DrumDataType.RightFace)
        {
            dokoh.System.SceneManager.LoadScene(SceneDataType.MusicChoice);
        }
    }
}
