using UnityEngine;

public class StartController : MonoBehaviour
{
    public Drums DrumFace;
    public DrumSide DrumSide;
    // public TestDrumInput Input;
    private bool isChanged;

    void OnEnable()
    {
        isChanged = false;
    }
    void Update()
    {
        //Execute Code
        DrumDataType drumDataType = dokoh.System.DrumManager.UseQueue();
        if (drumDataType == DrumDataType.RightFace)
        {
            isChanged = true;
            dokoh.System.SceneManager.LoadScene(SceneDataType.MusicChoice);
        }
        
        //Test Code
        // if (!isChanged && Input.testInputType == DrumDataType.RightFace)
        // {
        //     isChanged = true;
        //     dokoh.System.SceneManager.LoadScene(SceneDataType.MusicChoice);
        // }
    }
}
