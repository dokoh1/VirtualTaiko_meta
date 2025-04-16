using UnityEngine;

public class StartSceneController : MonoBehaviour
{
    public TestDrumInput testDrumInput;
    void Update()
    {
        if (testDrumInput.testType == DrumDataType.LeftFace)
            dokoh.System.SceneManager.LoadScene(SceneDataType.MusicChoice);
    }
}
