using UnityEngine;

public class SceneController : MonoBehaviour
{
    public TestDrumInput testDrumInput;
    void Update()
    {
        if (testDrumInput.testType == DrumDataType.RightFace)
            dokoh.System.SceneManager.LoadScene(SceneDataType.MusicChoice);
    }
}
