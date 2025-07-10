using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumEffect : MonoBehaviour
{
    [SerializeField] private GameObject leftSide;
    [SerializeField] private GameObject rightSide;
    [SerializeField] private GameObject leftFace;
    [SerializeField] private GameObject rightFace;

    private Dictionary<DrumDataType, GameObject[]> _effectMap;
    private const float FlashDuration = 0.2f;

    private void Awake()
    {
        _effectMap = new Dictionary<DrumDataType, GameObject[]>
        {
            { DrumDataType.LeftSide, new[] { leftSide } },
            { DrumDataType.RightSide, new[] { rightSide } },
            { DrumDataType.LeftFace, new[] { leftFace } },
            { DrumDataType.RightFace, new[] { rightFace } },
            { DrumDataType.DobletFace, new[] { leftFace, rightFace } },
            { DrumDataType.Dobletside, new[] { leftSide, rightSide } },
        };
    }

    private void OnEnable()
    {
        foreach (var objs in _effectMap.Values)
        {
            foreach (var obj in objs)
                obj.SetActive(false);
        }
    }

    public void DrumEffectInput(DrumDataType drumDataType)
    {
        if (_effectMap.TryGetValue(drumDataType, out var drums))
        {
            StartCoroutine(FlashObjects(drums));
        }
    }

    private IEnumerator FlashObjects(GameObject[] objects)
    {
        foreach (var obj in objects)
            obj.SetActive(true);

        yield return new WaitForSeconds(FlashDuration);

        foreach (var obj in objects)
            obj.SetActive(false);
    }
}
