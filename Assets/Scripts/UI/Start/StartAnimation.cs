using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class StartAnimation : MonoBehaviour
{
    [SerializeField]
    private RectTransform Right, Left;
    
    [SerializeField]
    private RectTransform BigRed, BigBlue, Drum, Title;
    
    [SerializeField]
    private RectTransform CryStal1, CryStal2, CryStal3, CryStal4, CryStal5;

    [SerializeField]
    private RectTransform Flower1, Flower2, Flower3, Flower4;

    [SerializeField] 
    private RectTransform Leaf1, Leaf2;
        
    [SerializeField] 
    private RectTransform BackGround;

    [SerializeField] 
    private RectTransform YutStick, Bell;

    [SerializeField] 
    private Image[] Lights;

    [SerializeField] private RectTransform[] MoveObjects;
    private float moveAmount = 50f;
    private void OnEnable()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(Right.DOAnchorPosX(260f, 0.5f).SetEase(Ease.OutQuad));
        seq.Append(Left.DOAnchorPosX(-240f, 0.5f).SetEase(Ease.OutQuad));
        
        seq.Append(BigRed.DOAnchorPosX(-180f, 0.5f).SetEase(Ease.OutQuad));
        seq.Join(BigRed.DOAnchorPosY(15f, 0.5f).SetEase(Ease.OutQuad));
        seq.Join(BigRed.DOSizeDelta(new Vector2(250f, 230f), 0.5f).SetEase(Ease.OutQuad));
        
        seq.Append(BigBlue.DOAnchorPosX(180f, 0.5f).SetEase(Ease.OutQuad));
        seq.Join(BigBlue.DOAnchorPosY(15f, 0.5f).SetEase(Ease.OutQuad));
        seq.Join(BigBlue.DOSizeDelta(new Vector2(250, 230f), 0.5f).SetEase(Ease.OutQuad));
        
        seq.Append(Drum.DOSizeDelta(new Vector2(160f, 150f), 0.5f).SetEase(Ease.OutQuad));
        seq.Join(Title.DOSizeDelta(new Vector2(300f, 130f), 0.5f).SetEase(Ease.OutQuad));
        
        //Effect 전
        seq.Append(CryStal1.DOSizeDelta(new Vector2(75f, 80f), 0.3f).SetEase(Ease.OutQuad));
        seq.Join(CryStal1.DOAnchorPosX(315f, 0.3f).SetEase(Ease.OutQuad));
        seq.Join(CryStal1.DOAnchorPosY( 0f, 0.3f).SetEase(Ease.OutQuad));
        
        seq.Join(CryStal2.DOSizeDelta(new Vector2(75f, 80f), 0.3f).SetEase(Ease.OutQuad));
        seq.Join(CryStal2.DOAnchorPosX(-280f, 0.3f).SetEase(Ease.OutQuad));
        seq.Join(CryStal2.DOAnchorPosY( 178f, 0.3f).SetEase(Ease.OutQuad));
        
        seq.Join(CryStal3.DOSizeDelta(new Vector2(75f, 80f), 0.3f).SetEase(Ease.OutQuad));
        seq.Join(CryStal3.DOAnchorPosX(-280f, 0.3f).SetEase(Ease.OutQuad));
        seq.Join(CryStal3.DOAnchorPosY( -150f, 0.3f).SetEase(Ease.OutQuad));
        
        seq.Join(CryStal4.DOSizeDelta(new Vector2(75f, 80f), 0.3f).SetEase(Ease.OutQuad));
        seq.Join(CryStal4.DOAnchorPosX(-200f, 0.3f).SetEase(Ease.OutQuad));
        seq.Join(CryStal4.DOAnchorPosY( 130f, 0.3f).SetEase(Ease.OutQuad));
        
        seq.Join(CryStal5.DOSizeDelta(new Vector2(75f, 80f), 0.3f).SetEase(Ease.OutQuad));
        seq.Join(CryStal5.DOAnchorPosX(-300f, 0.3f).SetEase(Ease.OutQuad));
        seq.Join(CryStal5.DOAnchorPosY( 0f, 0.3f).SetEase(Ease.OutQuad));
        
        seq.Join(Flower1.DOSizeDelta(new Vector2(75f, 80f), 0.3f).SetEase(Ease.OutQuad));
        seq.Join(Flower1.DOAnchorPosX(180f, 0.3f).SetEase(Ease.OutQuad));
        seq.Join(Flower1.DOAnchorPosY( 180f, 0.3f).SetEase(Ease.OutQuad));
        
        seq.Join(Flower2.DOSizeDelta(new Vector2(75f, 80f), 0.3f).SetEase(Ease.OutQuad));
        seq.Join(Flower2.DOAnchorPosX(280f, 0.3f).SetEase(Ease.OutQuad));
        seq.Join(Flower2.DOAnchorPosY( 90f, 0.3f).SetEase(Ease.OutQuad));
        
        seq.Join(Flower3.DOSizeDelta(new Vector2(75f, 80f), 0.3f).SetEase(Ease.OutQuad));
        seq.Join(Flower3.DOAnchorPosX(40f, 0.3f).SetEase(Ease.OutQuad));
        seq.Join(Flower3.DOAnchorPosY( 160f, 0.3f).SetEase(Ease.OutQuad));
        
        seq.Join(Flower4.DOSizeDelta(new Vector2(75f, 80f), 0.3f).SetEase(Ease.OutQuad));
        seq.Join(Flower4.DOAnchorPosX(310f, 0.3f).SetEase(Ease.OutQuad));
        seq.Join(Flower4.DOAnchorPosY( -90f, 0.3f).SetEase(Ease.OutQuad));
        
        seq.Join(Leaf1.DOSizeDelta(new Vector2(75f, 80f), 0.3f).SetEase(Ease.OutQuad));
        seq.Join(Leaf1.DOAnchorPosX(90f, 0.3f).SetEase(Ease.OutQuad));
        seq.Join(Leaf1.DOAnchorPosY( -200f, 0.3f).SetEase(Ease.OutQuad));
        
        seq.Join(Leaf2.DOSizeDelta(new Vector2(75f, 80f), 0.3f).SetEase(Ease.OutQuad));
        seq.Join(Leaf2.DOAnchorPosX(-320f, 0.3f).SetEase(Ease.OutQuad));
        seq.Join(Leaf2.DOAnchorPosY( -90f, 0.3f).SetEase(Ease.OutQuad));
        
        seq.Join(YutStick.DOSizeDelta(new Vector2(110f, 120f), 0.3f).SetEase(Ease.OutQuad));
        seq.Join(YutStick.DOAnchorPosX(-110f, 0.3f).SetEase(Ease.OutQuad));
        seq.Join(YutStick.DOAnchorPosY( 120f, 0.3f).SetEase(Ease.OutQuad));
        
        seq.Join(Bell.DOSizeDelta(new Vector2(75f, 80f), 0.3f).SetEase(Ease.OutQuad));
        seq.Join(Bell.DOAnchorPosX(110f, 0.3f).SetEase(Ease.OutQuad));
        seq.Join(Bell.DOAnchorPosY( 120f, 0.3f).SetEase(Ease.OutQuad));
        
        //Effect 후
        seq.Append(CryStal1.DOAnchorPosX(330f, 2f).SetEase(Ease.OutQuad));
        seq.Join(CryStal1.DOAnchorPosY( 10f, 2f).SetEase(Ease.OutQuad));
        
        seq.Join(CryStal2.DOAnchorPosX(-295f, 2f).SetEase(Ease.OutQuad));
        seq.Join(CryStal2.DOAnchorPosY( 188f, 2f).SetEase(Ease.OutQuad));
        
        seq.Join(CryStal3.DOAnchorPosX(-295f, 2f).SetEase(Ease.OutQuad));
        seq.Join(CryStal3.DOAnchorPosY( -170f, 2f).SetEase(Ease.OutQuad));
        
        seq.Join(CryStal4.DOAnchorPosX(-215f, 2f).SetEase(Ease.OutQuad));
        seq.Join(CryStal4.DOAnchorPosY( 140f, 2f).SetEase(Ease.OutQuad));
        
        seq.Join(CryStal5.DOAnchorPosX(-315f, 2f).SetEase(Ease.OutQuad));
        seq.Join(CryStal5.DOAnchorPosY( 10f, 2f).SetEase(Ease.OutQuad));

        seq.Join(Flower1.DOAnchorPosX(195f, 2f).SetEase(Ease.OutQuad));
        seq.Join(Flower1.DOAnchorPosY( 190f, 2f).SetEase(Ease.OutQuad));
        
        seq.Join(Flower2.DOAnchorPosX(295f, 2f).SetEase(Ease.OutQuad));
        seq.Join(Flower2.DOAnchorPosY( 100f, 2f).SetEase(Ease.OutQuad));
        
        seq.Join(Flower3.DOAnchorPosX(55f, 2f).SetEase(Ease.OutQuad));
        seq.Join(Flower3.DOAnchorPosY( 170f, 2f).SetEase(Ease.OutQuad));
        
        seq.Join(Flower4.DOAnchorPosX(325f, 2f).SetEase(Ease.OutQuad));
        seq.Join(Flower4.DOAnchorPosY( -80f, 2f).SetEase(Ease.OutQuad));
        
        seq.Join(Leaf1.DOAnchorPosX(105f, 2f).SetEase(Ease.OutQuad));
        seq.Join(Leaf1.DOAnchorPosY( -210f, 2f).SetEase(Ease.OutQuad));
        
        seq.Join(Leaf2.DOAnchorPosX(-335f, 2f).SetEase(Ease.OutQuad));
        seq.Join(Leaf2.DOAnchorPosY( -80f, 2f).SetEase(Ease.OutQuad));
        
        seq.Join(YutStick.DOAnchorPosX(-125f, 2f).SetEase(Ease.OutQuad));
        seq.Join(YutStick.DOAnchorPosY( 130f, 2f).SetEase(Ease.OutQuad));
        
        seq.Join(Bell.DOAnchorPosX(125f, 2f).SetEase(Ease.OutQuad));
        seq.Join(Bell.DOAnchorPosY( 130f, 2f).SetEase(Ease.OutQuad));

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
    
    void OnDisable()
    {
        
    }
}
