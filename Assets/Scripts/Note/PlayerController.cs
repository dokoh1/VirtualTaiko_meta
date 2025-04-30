using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private NoteManager1 noteManager;
    public Drums drums;
    public DrumSide drumSide;
    private TimingManager timingManager;

    void Start()
    {
        timingManager = FindFirstObjectByType<TimingManager>();
        noteManager = FindObjectOfType<NoteManager1>();
    }

    void Update()
    {
        if (timingManager.BoxNoteList.Count > 0) // 노트가 있을 때만 체크
        {
            GameObject closestNote = timingManager.BoxNoteList[0]; // 가장 가까운 노트 선택 (0번)

            // 노트 타입 확인
            NoteType noteType = closestNote.GetComponent<Note>().noteType; // 노트의 타입을 확인한다고 가정

            // 입력된 키가 올바른지 체크
            if (noteType == NoteType.smallRed)
            {
                // 작은 빨간 노트는 S나 K 중 하나만 눌러도 판정
                
                //test
                // if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.K))

                //execute
                if (drums.dataSet == DrumDataType.RightFace || drums.dataSet == DrumDataType.LeftFace)
                {
                    // CheckHit(KeyCode.S, KeyCode.K); // 작은 빨간 노트는 S와 K로 체크
                    CheckHit();
                }
            }
            else if (noteType == NoteType.smallBlue)
            {
                // 작은 파란 노트는 A나 L 중 하나만 눌러도 판정
                //test
                // if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.L))
                
                //execute
                if (drumSide.dataSet == DrumDataType.RightSide || drumSide.dataSet == DrumDataType.LeftSide)
                {
                    // CheckHit(KeyCode.A, KeyCode.L); // 작은 파란 노트는 A와 L로 체크
                    CheckHit();
                }
            }
            // test
            // else if (noteType == NoteType.bigRed)
            
            //execute
            else if (noteType == NoteType.bigRed)
            {
                // 큰 빨간 노트는 S와 K를 동시에 눌렀을 때 체크
                if (drums.dataSet == DrumDataType.DobletFace)
                {
                    // CheckHit(KeyCode.S, KeyCode.K); // 큰 빨간 노트는 S와 K 동시에 눌렀을 때 체크
                    CheckHit();
                }
            }
            //test
            // else if (noteType == NoteType.bigBlue)
            
            //execute
            else if (noteType == NoteType.bigBlue)
            {
                // 큰 파란 노트는 A와 L을 동시에 눌렀을 때 체크
                if (drums.dataSet == DrumDataType.Dobletside)
                {
                    // CheckHit(KeyCode.A, KeyCode.L); // 큰 파란 노트는 A와 L 동시에 눌렀을 때 체크
                    CheckHit();
                }
            }
        }
    }

    // 올바른 키 입력에 대한 판정 처리
    // private void CheckHit(KeyCode key1, KeyCode key2)
    private void CheckHit()
    {
        // 작은 노트는 하나만 눌려도 판정 (key1, key2 중 하나가 눌리면)
        // if (Input.GetKeyDown(key1) || Input.GetKeyDown(key2))
        // {
            // 판정 코드 (예시로 타이밍 체크)
            HitResult result = timingManager.CheckTiming();
            timingManager.ProcessResult(result);
        // }
    }
}