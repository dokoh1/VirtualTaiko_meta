using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class StartAnimation : MonoBehaviour
{
    public RectTransform Right, Left, BigRed, BigBlue, Drum, Title;
    public RectTransform YutStick, Bell;

    [Header("크리스탈, 꽃, 잎 오브젝트들")]
    public List<RectTransform> Crystals;
    public List<RectTransform> Flowers;
    public List<RectTransform> Leafs;


    [SerializeField] 
    private Image[] Lights;

    [SerializeField] private RectTransform[] MoveObjects;
    private float moveAmount = 50f;
    private void OnEnable()
    {
        Sequence seq = DOTween.Sequence();
        PlayUIAnimation(seq);
        
        seq.AppendCallback(() =>
        {
            foreach (var light in Lights)
            {
               light.DOFade(0.3f, 0.5f)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine);
            }
            
            foreach (var moveObject in MoveObjects)
            {
                Vector2 startPos = moveObject.anchoredPosition;
                Sequence LoopSequence = DOTween.Sequence();
                LoopSequence.Append(moveObject.DOAnchorPosY(startPos.y + moveAmount, 0.5f).SetEase(Ease.InOutSine))
                    .Append(moveObject.DOAnchorPosY(startPos.y, 0.5f).SetEase(Ease.InOutSine))
                    .AppendInterval(1)
                    .SetLoops(-1);
            }
        });
    }
    void PlayUIAnimation(Sequence seq)
    {

        // 기본 이동
        seq.Append(Right.DOAnchorPosX(260f, 0.5f).SetEase(Ease.OutQuad));
        seq.Append(Left.DOAnchorPosX(-240f, 0.5f).SetEase(Ease.OutQuad));

        
        // BigRed 등장
        seq.Append(BigRed.DOAnchorPosX(-180f, 0.5f).SetEase(Ease.OutQuad));
        seq.Join(BigRed.DOAnchorPosY(15f, 0.5f).SetEase(Ease.OutQuad));
        seq.Join(BigRed.DOSizeDelta(new Vector2(250f, 230f), 0.5f).SetEase(Ease.OutQuad));
        
        // BigBlue 등장
        AnimateFull(BigBlue, new Vector2(180f, 15f), new Vector2(250f, 230f), 0.5f, seq);

        // Drum + Title 등장
        AnimateSizeInit(Drum, new Vector2(160f, 150f), 0.5f, seq);
        AnimateSize(Title, new Vector2(300f, 130f), 0.5f, seq);

        // 초기 크리스탈/플라워/리프 애니메이션
        AnimateGroup(Crystals, new Vector2[] {
            new Vector2(315f, 0f), new Vector2(-280f, 178f), new Vector2(-280f, -150f),
            new Vector2(-200f, 130f), new Vector2(-300f, 0f)
        }, 0.3f, new Vector2(75f, 80f), seq);

        AnimateGroup(Flowers, new Vector2[] {
            new Vector2(180f, 180f), new Vector2(280f, 90f),
            new Vector2(40f, 160f), new Vector2(310f, -90f)
        }, 0.3f, new Vector2(75f, 80f), seq);

        AnimateGroup(Leafs, new Vector2[] {
            new Vector2(90f, -200f), new Vector2(-320f, -90f)
        }, 0.3f, new Vector2(75f, 80f), seq);

        AnimateFull(YutStick, new Vector2(-110f, 120f), new Vector2(110f, 120f), 0.3f, seq);
        AnimateFull(Bell, new Vector2(110f, 120f), new Vector2(75f, 80f), 0.3f, seq);

        // 이후 위치 이동
        MoveGroup(Crystals, new Vector2[] {
            new Vector2(330f, 10f), new Vector2(-295f, 188f), new Vector2(-295f, -170f),
            new Vector2(-215f, 140f), new Vector2(-315f, 10f)
        }, 2f, seq);

        MoveGroup(Flowers, new Vector2[] {
            new Vector2(195f, 190f), new Vector2(295f, 100f),
            new Vector2(55f, 170f), new Vector2(325f, -80f)
        }, 2f, seq);

        MoveGroup(Leafs, new Vector2[] {
            new Vector2(105f, -210f), new Vector2(-335f, -80f)
        }, 2f, seq);

        MovePosition(YutStick, new Vector2(-125f, 130f), 2f, seq);
        MovePosition(Bell, new Vector2(125f, 130f), 2f, seq);
    }

    void AnimateFull(RectTransform target, Vector2 pos, Vector2 size, float time, Sequence seq)
    {
        seq.Join(target.DOAnchorPosX(pos.x, time).SetEase(Ease.OutQuad));
        seq.Join(target.DOAnchorPosY(pos.y, time).SetEase(Ease.OutQuad));
        seq.Join(target.DOSizeDelta(size, time).SetEase(Ease.OutQuad));
    }

    void AnimateSize(RectTransform target, Vector2 size, float time, Sequence seq)
    {
        seq.Join(target.DOSizeDelta(size, time).SetEase(Ease.OutQuad));
    }
    void AnimateSizeInit(RectTransform target, Vector2 size, float time, Sequence seq)
    {
        seq.Append(target.DOSizeDelta(size, time).SetEase(Ease.OutQuad));
    }

    void AnimateGroup(List<RectTransform> targets, Vector2[] positions, float time, Vector2 size, Sequence seq)
    {
        for (int i = 0; i < targets.Count && i < positions.Length; i++)
        {
            AnimateFull(targets[i], positions[i], size, time, seq);
        }
    }

    void MoveGroup(List<RectTransform> targets, Vector2[] positions, float time, Sequence seq)
    {
        for (int i = 0; i < targets.Count && i < positions.Length; i++)
        {
            MovePosition(targets[i], positions[i], time, seq);
        }
    }

    void MovePosition(RectTransform target, Vector2 pos, float time, Sequence seq)
    {
        seq.Join(target.DOAnchorPosX(pos.x, time).SetEase(Ease.OutQuad));
        seq.Join(target.DOAnchorPosY(pos.y, time).SetEase(Ease.OutQuad));
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
    }

    void Init(RectTransform ObjectRect)
    {
        ObjectRect.anchoredPosition = new Vector2(0f, -100f);
        ObjectRect.sizeDelta = new Vector2(0f, 0f);
    }
}
