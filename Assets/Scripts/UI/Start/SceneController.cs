using UnityEngine;

public class SceneController : MonoBehaviour
{
    public TestDrumInput testDrumInput;
    void Update()
    {
        if (testDrumInput.testInputType == DrumInputDataType.RightFace)
            dokoh.System.SceneManager.LoadScene(SceneDataType.MusicChoice);
    }
}
