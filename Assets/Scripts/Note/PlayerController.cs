using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private NoteManager1 noteManager;
    private TimingManager timingManager;
    public DrumEffect drumEffect;

    void Start()
    {
        timingManager = FindFirstObjectByType<TimingManager>();
        noteManager = FindObjectOfType<NoteManager1>();
    }
    void Update()
    {
        DrumDataType drumDataType = InputManager.Instance.GetInput();
        if (drumDataType != DrumDataType.NotHit)
            drumEffect.DrumEffectInput(drumDataType);
        if (timingManager.BoxNoteList.Count == 0)
            return;
        
        GameObject closestNote = timingManager.BoxNoteList[0]; // 가장 가까운 노트 선택 (0번)
        // 노트 타입 확인
        NoteType noteType = closestNote.GetComponent<Note>().noteType; // 노트의 타입을 확인한다고 가정
        // 입력된 키가 올바른지 체크
        if (noteType == NoteType.smallRed)
        {
            if (drumDataType == DrumDataType.RightFace || drumDataType == DrumDataType.LeftFace)
            {
                timingManager.CheckTiming();
            }
        }
        else if (noteType == NoteType.smallBlue)
        {
            if (drumDataType== DrumDataType.RightSide || drumDataType== DrumDataType.LeftSide)
            {
                timingManager.CheckTiming();
            }
        }
        // test
        // else if (noteType == NoteType.bigRed)
        
        //execute
        else if (noteType == NoteType.bigRed)
        {
            // 큰 빨간 노트는 S와 K를 동시에 눌렀을 때 체크
            if (drumDataType == DrumDataType.DobletFace)
            {
                // CheckHit(KeyCode.S, KeyCode.K); // 큰 빨간 노트는 S와 K 동시에 눌렀을 때 체크
                timingManager.CheckTiming();
            }
        }
        //test
        // else if (noteType == NoteType.bigBlue)
        
        //execute
        else if (noteType == NoteType.bigBlue)
        {
            // 큰 파란 노트는 A와 L을 동시에 눌렀을 때 체크
            if (drumDataType == DrumDataType.Dobletside)
            {
                // CheckHit(KeyCode.A, KeyCode.L); // 큰 파란 노트는 A와 L 동시에 눌렀을 때 체크
                timingManager.CheckTiming();
            }
        }
        // drumEffect.DrumEffectInput(drumDataType);
        drumEffect.DrumEffectInput(drumDataType);
    
    }
}