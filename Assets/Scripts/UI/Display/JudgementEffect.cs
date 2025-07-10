using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementEffect : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private BellAnimation bellAnimationAnimation;
    [SerializeField] private GameObject gold;
    [SerializeField] private GameObject silver;

    private Dictionary<HitResult, string> _triggerMap;
    private HashSet<HitResult> _useSilver;
    private HashSet<HitResult> _useGold;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();

        // 트리거 매핑 초기화
        _triggerMap = new Dictionary<HitResult, string>
        {
            { HitResult.Bad, "Bad" },
            { HitResult.BigGood, "GoodBig" },
            { HitResult.SmallGood, "GoodSmall" },
            { HitResult.BigPerfect, "PerfectBig" },
            { HitResult.SmallPerfect, "PerfectSmall" }
        };

        _useSilver = new HashSet<HitResult> { HitResult.BigGood, HitResult.SmallGood };
        _useGold = new HashSet<HitResult> { HitResult.BigPerfect, HitResult.SmallPerfect };
    }

    public IEnumerator EffectUpdate(HitResult result)
    {
        yield return null;

        if (_triggerMap.TryGetValue(result, out var triggerName))
        {
            _animator.SetTrigger(triggerName);

            if (_useSilver.Contains(result))
                bellAnimationAnimation.DoAnimation(silver);
            else if (_useGold.Contains(result))
                bellAnimationAnimation.DoAnimation(gold);
        }
    }
}