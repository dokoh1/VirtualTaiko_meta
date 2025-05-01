
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAnimation : MonoBehaviour
{
    private int GoblinNumber;
    [SerializeField]
    private AnimationClip animationClip;
    private List<GameObject> Goblins = new();
    
    public GameObject GoblinPrefab;
    public GameObject InitGoblin;
    private RectTransform InitGoblinRect;

    private void Awake()
    {
        Goblins.Add(InitGoblin);
        InitGoblinRect = InitGoblin.GetComponent<RectTransform>();
        Debug.Log("등장 애니메이션 길이 :" + animationClip.length);
    }
    
    public void CreateGoblin()
    {
        Vector2 offset = Goblins.Count == 1 ? new Vector2(-150, 0) : new Vector2(150, 0);
        Vector2 goblinPos = InitGoblinRect.anchoredPosition + offset;

        var go = Instantiate(GoblinPrefab, transform, false);
        go.GetComponent<RectTransform>().anchoredPosition = goblinPos;

        Animator animator = go.GetComponent<Animator>();
        animator.SetTrigger("IsEnable");
        StartCoroutine(IdleInit());
        Goblins.Add(go);
    }

    private IEnumerator IdleInit()
    {
        yield return new WaitForSeconds(0.516f);
        foreach (var go in Goblins)
        {
            go.GetComponent<Animator>().Play("GoblinIdle", 0, 0);
        }
    }
}
