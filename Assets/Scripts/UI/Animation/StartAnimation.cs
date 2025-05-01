using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class StartAnimation : MonoBehaviour
{
    public RectTransform Right, Left, BigRed, BigBlue, Drum, Title;
    public RectTransform YutStick, Bell;
    public RectTransform AppendObject;

    [Header("크리스탈, 꽃, 잎 오브젝트들")]
    public List<RectTransform> Crystals;
    public List<RectTransform> Flowers;
    public List<RectTransform> Leafs;

    private Sequence mainSeq;
    private Sequence LoopSequence;
    [SerializeField] private Image[] GuideImages;


    [SerializeField] 
    private Image[] Lights;

    [SerializeField] private Text guideText;

    [SerializeField] private RectTransform[] MoveObjects;
    private float moveAmount = 50f;
    private void OnEnable()
    {
        mainSeq = DOTween.Sequence();
        LoopSequence = DOTween.Sequence();
        PlayUIAnimation();
        
        mainSeq.AppendCallback(() =>
        {
            foreach (var light in Lights)
            {
               light.DOFade(0.3f, 0.5f)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine);
            }

            foreach (var GuideImage in GuideImages)
            {
                GuideImage.DOFade(0.3f, 0.5f)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutQuad)
                    .From(1f);
            }
            guideText.DOFade(0.3f, 0.5f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutQuad)
                .From(1f);
            foreach (var moveObject in MoveObjects)
            {
                Vector2 startPos = moveObject.anchoredPosition;
                LoopSequence = DOTween.Sequence();
                LoopSequence.Append(moveObject.DOAnchorPosY(startPos.y + moveAmount, 0.5f).SetEase(Ease.InOutSine))
                    .Append(moveObject.DOAnchorPosY(startPos.y, 0.5f).SetEase(Ease.InOutSine))
                    .AppendInterval(1)
                    .SetLoops(-1, LoopType.Yoyo);
            }
        });
    }
    
    void PlayUIAnimation()
    {
        
        // 기본 이동
        mainSeq.Append(Right.DOAnchorPosX(260f, 0.5f).SetEase(Ease.OutQuad));
        mainSeq.Append(Left.DOAnchorPosX(-240f, 0.5f).SetEase(Ease.OutQuad));

        
        // BigRed 등장
        mainSeq.Append(BigRed.DOAnchorPosX(-180f, 0.5f).SetEase(Ease.OutQuad));
        mainSeq.Join(BigRed.DOAnchorPosY(15f, 0.5f).SetEase(Ease.OutQuad));
        mainSeq.Join(BigRed.DOSizeDelta(new Vector2(250f, 230f), 0.5f).SetEase(Ease.OutQuad));
        
        // BigBlue 등장
        AnimateFull(BigBlue, new Vector2(180f, 15f), new Vector2(250f, 230f), 0.5f);

        // Drum + Title 등장
        AnimateSizeInit(Drum, new Vector2(160f, 150f), 0.5f);
        AnimateSize(Title, new Vector2(300f, 130f), 0.5f);
        
        // 초기 크리스탈/플라워/리프 애니메이션
        DoAppend(0.3f);
        AnimateGroup(Crystals, new Vector2[] {
            new Vector2(315f, 0f), new Vector2(-280f, 178f), new Vector2(-280f, -150f),
            new Vector2(-200f, 130f), new Vector2(-300f, 0f)
        }, 0.3f, new Vector2(75f, 80f));

        AnimateGroup(Flowers, new Vector2[] {
            new Vector2(180f, 180f), new Vector2(280f, 90f),
            new Vector2(40f, 160f), new Vector2(310f, -90f)
        }, 0.3f, new Vector2(75f, 80f));

        AnimateGroup(Leafs, new Vector2[] {
            new Vector2(90f, -200f), new Vector2(-320f, -90f)
        }, 0.3f, new Vector2(75f, 80f));
        
        
        AnimateFull(YutStick, new Vector2(-110f, 120f), new Vector2(110f, 120f), 00.3f);
        AnimateFull(Bell, new Vector2(110f, 120f), new Vector2(75f, 80f), 0.3f);

        // 이후 위치 이동
        DoAppend(2f);
        MoveGroup(Crystals, new Vector2[] {
            new Vector2(330f, 10f), new Vector2(-295f, 188f), new Vector2(-295f, -170f),
            new Vector2(-215f, 140f), new Vector2(-315f, 10f)
        }, 2f);

        MoveGroup(Flowers, new Vector2[] {
            new Vector2(195f, 190f), new Vector2(295f, 100f),
            new Vector2(55f, 170f), new Vector2(325f, -80f)
        }, 2f);

        MoveGroup(Leafs, new Vector2[] {
            new Vector2(105f, -210f), new Vector2(-335f, -80f)
        }, 2f);

        MovePosition(YutStick, new Vector2(-125f, 130f), 2f);
        MovePosition(Bell, new Vector2(125f, 130f), 2f);
    }

    void AnimateFull(RectTransform target, Vector2 pos, Vector2 size, float time)
    {
        mainSeq.Join(target.DOAnchorPosX(pos.x, time).SetEase(Ease.OutQuad));
        mainSeq.Join(target.DOAnchorPosY(pos.y, time).SetEase(Ease.OutQuad));
        mainSeq.Join(target.DOSizeDelta(size, time).SetEase(Ease.OutQuad));
    }

    void AnimateSize(RectTransform target, Vector2 size, float time)
    {
        mainSeq.Join(target.DOSizeDelta(size, time).SetEase(Ease.OutQuad));
    }
    void AnimateSizeInit(RectTransform target, Vector2 size, float time)
    {
        mainSeq.Append(target.DOSizeDelta(size, time).SetEase(Ease.OutQuad));
    }

    void AnimateGroup(List<RectTransform> targets, Vector2[] positions, float time, Vector2 size)
    {
        for (int i = 0; i < targets.Count && i < positions.Length; i++)
        {
            AnimateFull(targets[i], positions[i], size, time);
        }
    }

    void MoveGroup(List<RectTransform> targets, Vector2[] positions, float time)
    {
        for (int i = 0; i < targets.Count && i < positions.Length; i++)
        {
            MovePosition(targets[i], positions[i], time);
        }
    }

    void DoAppend(float time)
    {
        mainSeq.Append(AppendObject.DOMoveX(AppendObject.position.x, time).SetEase(Ease.OutQuad));
    }
    void MovePosition(RectTransform target, Vector2 pos, float time)
    {
        mainSeq.Join(target.DOAnchorPosX(pos.x, time).SetEase(Ease.OutQuad));
        mainSeq.Join(target.DOAnchorPosY(pos.y, time).SetEase(Ease.OutQuad));
    }
    void OnDisable()
    {
        Right.anchoredPosition = new Vector2(-550f, -175f);
        Left.anchoredPosition = new Vector2(-580f, -175f);
        Init(BigRed);
        Init(BigBlue);
        Init(Drum);
        Init(Title);
        Init(YutStick);
        Init(Bell);
        InitList(Crystals);
        InitList(Flowers);
        InitList(Leafs);
        mainSeq.Kill();
        LoopSequence.Kill();
    }

    void Init(RectTransform ObjectRect)
    {
        ObjectRect.anchoredPosition = new Vector2(0f, -100f);
        ObjectRect.sizeDelta = new Vector2(0f, 0f);
    }

    void InitList(List<RectTransform> list)
    {
        foreach (var obj in list)
        {
            obj.anchoredPosition = new Vector2(0f, -100f);
            obj.sizeDelta = new Vector2(0f, 0f);
        }
    }
}

// 코드 정리
// 1. DoAppend 라고 빈 오브젝트를 만들고 flag를 세운다.
// 2. Random.Range 값을 이용해서 꽃,크리스탈,나뭇잎 코드를 메서드화 한다.
