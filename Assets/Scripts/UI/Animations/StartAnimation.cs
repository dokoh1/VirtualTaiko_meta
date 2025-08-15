using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform right, left, bigRed, bigBlue, drum, title;
    [SerializeField] private RectTransform yutStick, bell;
    [SerializeField] private RectTransform appendObject;
    
    [Header("크리스탈, 꽃, 잎 오브젝트들")]
    [SerializeField]
    private List<RectTransform> crystals, flowers, leafs;

    private Sequence _mainSeq;
    private Sequence _loopSequence;
    
    [SerializeField] 
    private Image[] guideImages;
    
    [SerializeField] 
    private Image[] lights;

    [SerializeField] private Text guideText;
    [SerializeField] private RectTransform[] moveObjects;

    [SerializeField] private StartAnimationSettings _settings;
    private void LightFade()
    {
        foreach (var light in lights)
        {
            light.DOFade(_settings.minFade, _settings.moveTime)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine)
                .From(_settings.maxFade);
        }
    }

    private void GuideFade()
    {
        foreach (var guideImage in guideImages)
        {
            guideImage.DOFade(_settings.minFade, _settings.moveTime)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutQuad)
                .From(_settings.maxFade);
        }
            
        guideText.DOFade(_settings.minFade, _settings.moveTime)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad)
            .From(_settings.maxFade);
    }

    private void ObjectMove()
    {
        foreach (var moveObject in moveObjects)
        {
            Vector2 startPos = moveObject.anchoredPosition;
            _loopSequence = DOTween.Sequence();
            _loopSequence.Append(moveObject.DOAnchorPosY(startPos.y + _settings.objectMoveAmount, _settings.moveTime).SetEase(Ease.InOutSine))
                .Append(moveObject.DOAnchorPosY(startPos.y, _settings.moveTime).SetEase(Ease.InOutSine))
                .AppendInterval(1)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
    
    private void OnEnable()
    {
        _mainSeq = DOTween.Sequence();
        _loopSequence = DOTween.Sequence();
        PlayUIAnimation();
        
        _mainSeq.AppendCallback(() =>
        {
            LightFade();
            GuideFade();
            ObjectMove();
        });
    }
    
    void PlayUIAnimation()
    {
        // 기본 이동
        _mainSeq.Append(right.DOAnchorPosX(260f, _settings.moveTime).SetEase(Ease.OutQuad));
        _mainSeq.Append(left.DOAnchorPosX(-240f, _settings.moveTime).SetEase(Ease.OutQuad));
        
        // BigRed 등장
        DoAppend(_settings.moveTime);
        AnimateFull(bigRed, new Vector2(-180f, 15f), new Vector2(250f, 230f), _settings.moveTime);
        
        // BigBlue 등장
        DoAppend(_settings.moveTime);
        AnimateFull(bigBlue, new Vector2(180f, 15f), new Vector2(250f, 230f), _settings.moveTime);

        // Drum + Title 등장
        DoAppend(_settings.moveTime);
        AnimateSize(drum, new Vector2(160f, 150f), _settings.moveTime);
        AnimateSize(title, new Vector2(300f, 130f), _settings.moveTime);
        
        // 초기 크리스탈/플라워/리프 애니메이션
        DoAppend(_settings.moveTime);
        AnimateGroup(crystals, _settings.moveTime, _settings.elementSize);
        AnimateGroup(flowers, _settings.moveTime, _settings.elementSize);
        AnimateGroup(leafs, _settings.moveTime, _settings.elementSize);
        
        AnimateFull(yutStick, new Vector2(-110f, 120f), _settings.elementSize, _settings.moveTime);
        AnimateFull(bell, new Vector2(110f, 120f), _settings.elementSize, _settings.moveTime);

        // 이후 위치 이동
        DoAppend(_settings.slowMoveTime);
        MoveGroup(crystals, _settings.slowMoveTime);
        MoveGroup(flowers, _settings.slowMoveTime);
        MoveGroup(leafs, _settings.slowMoveTime);

        MovePosition(yutStick, new Vector2(-125f, 130f), _settings.slowMoveTime);
        MovePosition(bell, new Vector2(125f, 130f), _settings.slowMoveTime);
    }

    void AnimateFull(RectTransform target, Vector2 pos, Vector2 size, float time)
    {
        _mainSeq.Join(target.DOAnchorPosX(pos.x, time).SetEase(Ease.OutQuad));
        _mainSeq.Join(target.DOAnchorPosY(pos.y, time).SetEase(Ease.OutQuad));
        _mainSeq.Join(target.DOSizeDelta(size, time).SetEase(Ease.OutQuad));
    }

    void AnimateSize(RectTransform target, Vector2 size, float time)
    {
        _mainSeq.Join(target.DOSizeDelta(size, time).SetEase(Ease.OutQuad));
    }

    void AnimateGroup(List<RectTransform> targets, float time, Vector2 size)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            Vector2 randomPos = new Vector2(Random.Range(_settings.minX, _settings.maxX), Random.Range(_settings.minY, _settings.maxY));
            AnimateFull(targets[i], randomPos, size, time);
            targets[i].anchoredPosition = randomPos;
        }
    }

    void MoveGroup(List<RectTransform> targets, float time)
    {
        
        for (int i = 0; i < targets.Count; i++)
        {
            float xmoveAmount = targets[i].anchoredPosition.x < 0 ? -_settings.elementMoveAmount : _settings.elementMoveAmount;
            float ymoveAmount = targets[i].anchoredPosition.y < 0 ? -_settings.elementMoveAmount : _settings.elementMoveAmount;
            Vector2 movePos = new Vector2(targets[i].anchoredPosition.x + xmoveAmount, targets[i].anchoredPosition.y + ymoveAmount);
            MovePosition(targets[i], movePos, time);
        }
    }

    void DoAppend(float time)
    {
        _mainSeq.Append(appendObject.DOMoveX(appendObject.position.x, time).SetEase(Ease.OutQuad));
    }
    void MovePosition(RectTransform target, Vector2 pos, float time)
    {
        _mainSeq.Join(target.DOAnchorPosX(pos.x, time).SetEase(Ease.OutQuad));
        _mainSeq.Join(target.DOAnchorPosY(pos.y, time).SetEase(Ease.OutQuad));
    }
    void OnDisable()
    {
        right.anchoredPosition = new Vector2(-550f, -175f);
        left.anchoredPosition = new Vector2(-580f, -175f);
        Init(bigRed);
        Init(bigBlue);
        Init(drum);
        Init(title);
        Init(yutStick);
        Init(bell);
        InitList(crystals);
        InitList(flowers);
        InitList(leafs);
        _mainSeq.Kill();
        _loopSequence.Kill();
    }

    void Init(RectTransform ObjectRect)
    {
        ObjectRect.anchoredPosition = _settings.initPosition;
        ObjectRect.sizeDelta = _settings.initSize;
    }

    void InitList(List<RectTransform> list)
    {
        foreach (var obj in list)
        {
            obj.anchoredPosition = _settings.initPosition;
            obj.sizeDelta = _settings.initSize;
        }
    }
}

// 코드 정리
// 1. DoAppend 라고 빈 오브젝트를 만들고 flag를 세운다.
// 2. Random.Range 값을 이용해서 꽃,크리스탈,나뭇잎 코드를 메서드화 한다.
