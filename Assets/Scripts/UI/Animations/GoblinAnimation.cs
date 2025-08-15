using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAnimation : MonoBehaviour
{

    [SerializeField]
    private AnimationClip animationClip;
    [SerializeField]
    private GameObject goblinPrefab;
    [SerializeField]
    private GameObject initGoblin;
    
    private int _goblinNumber;
    private readonly List<GameObject> _goblins = new();
    private RectTransform _initGoblinRect;
    
    private void Awake()
    {
        _goblins.Add(initGoblin);
        _initGoblinRect = initGoblin.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        GameEvents.OnNoteHit += HandleNoteHit;
    }

    private void OnDisable()
    {
        GameEvents.OnNoteHit -= HandleNoteHit;
    }
    private void CreateGoblin()
    {
        Vector2 offset = _goblins.Count == 1 ? new Vector2(-150f, 0f) : new Vector2(150f, 0f);
        Vector2 goblinPos = _initGoblinRect.anchoredPosition + offset;

        var goblin = Instantiate(goblinPrefab, transform, false);
        goblin.GetComponent<RectTransform>().anchoredPosition = goblinPos;

        Animator animator = goblin.GetComponent<Animator>();
        animator.SetTrigger("IsEnable");
        
        StartCoroutine(IdleInit());
        _goblins.Add(goblin);
    }

    private void HandleNoteHit(HitResult result, ScoreData scoreData)
    {
        if (scoreData.hit == 30 || scoreData.hit == 60)
        {
            CreateGoblin();
        }
    }
    private IEnumerator IdleInit()
    {
        float goblinAnimationTime = animationClip.length / animationClip.frameRate;
        yield return new WaitForSeconds(goblinAnimationTime);
        
        foreach (var go in _goblins)
            go.GetComponent<Animator>().Play("GoblinIdle", 0, 0);
    }
}
