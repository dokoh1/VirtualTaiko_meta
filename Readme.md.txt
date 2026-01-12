 # VirtualTaiko

  ---

  작성자 : (이름)

  궁금한 점 있을 시 Issue 남겨주시면 답변드립니다!

  # 목차

  - [개요](#개요)
    * [작성 의의](#작성-의의)
    * [소개](#소개)
      + [함께 사용된 라이브러리](#함께-사용된-라이브러리)
      + [문서 내용](#문서-내용)
  - [Part 1. 프로젝트 기능 분석](#part-1-프로젝트-기능-분석)
    * [구현된 기능 리스트](#구현된-기능-리스트)
      + [게임플로우](#게임플로우)
      + [입력 시스템](#입력-시스템)
      + [리듬 판정/스코어](#리듬-판정스코어)
      + [UI 연출](#ui-연출)
  - [Part 2. 디자인 패턴 소개](#part-2-디자인-패턴-소개)
    * [State 패턴](#state-패턴)
    * [MVP 패턴](#mvp-패턴)
    * [Strategy 패턴](#strategy-패턴)
    * [Observer 패턴](#observer-패턴)
    * [Singleton/Service Locator](#singletonservice-locator)
    * [Object Pool](#object-pool)
    * [Factory](#factory)
  - [Part 3. Core 시스템 분석](#part-3-core-시스템-분석)
    * [System (서비스 접근점)](#system-서비스-접근점)
    * [SceneManager (씬 전환 및 페이드)](#scenemanager-씬-전환-및-페이드)
    * [InputManager (입력 라우팅)](#inputmanager-입력-라우팅)
  - [Part 4. 게임플레이 흐름](#part-4-게임플레이-흐름)
    * [노트 생성](#노트-생성)
    * [타이밍 판정](#타이밍-판정)
    * [스코어/콤보 갱신](#스코어콤보-갱신)
  - [더 나아가서 고민해볼 것](#더-나아가서-고민해볼-것)

  ---

  # 개요

  ## 작성 의의

  VirtualTaiko 프로젝트에서 적용된 핵심 디자인 패턴(State, MVP, Strategy, Observer 등)을 정리해
  구조 이해와 확장 포인트를 쉽게 파악할 수 있도록 한다.

  ## 소개

  ### 함께 사용된 라이브러리

  1. XR Interaction Toolkit
  2. DOTween

  ### 문서 내용

  Part 1에서는 프로젝트의 주요 기능을 정리한다.
  Part 2에서는 실제 코드에서 확인되는 디자인 패턴을 중심으로 설명한다.
  Part 3에서는 핵심 시스템의 구조를 설명한다.
  Part 4에서는 노트 생성부터 판정 및 결과 처리까지의 흐름을 정리한다.

  ---

  # Part 1. 프로젝트 기능 분석

  ## 구현된 기능 리스트

  ### 게임플로우
  - Start → MusicChoice → Play → Result의 상태 전환 구조
  - `GameStateMachine`과 `IGameState`로 상태 관리 (`Assets/Scripts/UI/Core/GameState`)
  - `SceneManager`를 통해 씬 오브젝트 전환 + 페이드 연출

  ### 입력 시스템
  - VR 모드와 Keyboard 모드를 분리하여 입력 처리
  - `InputManager`가 `IInputProvider`를 교체하여 입력 전략 선택

  ### 리듬 판정/스코어
  - `TimingManager`가 가장 가까운 노트를 찾아 판정
  - `ScoreData`에서 콤보/스코어/데드게이지 계산

  ### UI 연출
  - 음악 선택 UI: `ChoiceView` + `ChoicePresenter` + `ChoiceModel`
  - DOTween 기반 애니메이션 연출
  - `NumberImagePool`로 숫자 UI 풀링 처리

  ---

  # Part 2. 디자인 패턴 소개

  ## State 패턴
  게임의 흐름을 상태로 분리하여 책임을 명확히 한다.

  - `GameStateMachine`이 `IGameState`를 교체하며 Enter/Exit 실행
  - Start, MusicChoice, Play, Result 상태 클래스가 각각의 진입/이탈 로직 담당

  ```csharp
  public void ChangeState(IGameState newState)
  {
      _currentState?.Exit();
      _currentState = newState;
      _currentState?.Enter();
  }

  (Assets/Scripts/UI/Core/GameState/GameStateMachine.cs)

  ## MVP 패턴

  MusicChoice UI에 MVP 구조 적용.

  - View: ChoiceView
  - Model: ChoiceModel
  - Presenter: ChoicePresenter

  _choiceView.OnScrollUpRequested += HandleScrollUpRequested;
  _choiceModel.OnChoiceUpdated += HandleChoicesUpdated;

  (Assets/Scripts/UI/Panels/MusicChoice/ChoicePresenter.cs)

  ## Strategy 패턴

  VR/Keyboard 입력 방식을 런타임에 교체한다.

  - IInputProvider 인터페이스
  - VRInputProvider, KeyboardInputProvider 구현

  if (isVREnabled)
      _inputProvider = gameObject.AddComponent<VRInputProvider>();
  else
      _inputProvider = gameObject.AddComponent<KeyboardInputProvider>();

  (Assets/Scripts/UI/Core/System/InputManager.cs)

  ## Observer 패턴

  UI와 게임플레이의 결합도를 낮추기 위해 이벤트 기반 통신 사용.

  - GameEvents에서 전역 이벤트 발행
  - PlayController가 판정 결과를 이벤트로 브로드캐스트
  - UI/Effect가 구독하여 반응

  GameEvents.TriggerOnScoreUpdated(_scoreData.score);
  GameEvents.OnDeadGauge += HandleDeadGaugeUpdated;

  (Assets/Scripts/UI/Shared/GameEvents.cs, Assets/Scripts/UI/GamePlay/Effects/DeadGauge.cs)

  ## Singleton/Service Locator

  전역에서 접근 가능한 시스템 진입점 제공.

  - Single.System이 Scene/Audio/Input/Drum/Score 관리
  - GameStateMachine도 싱글톤으로 유지

  public static AudioManager AudioManager => Instance?.audioManager;

  (Assets/Scripts/UI/Core/System/System.cs)

  ## Object Pool

  숫자 UI를 재사용해 GC 부하를 줄임.

  - NumberImagePool에서 ObjectPool<GameObject> 운영

  pool = new ObjectPool<GameObject>(createFunc: () => Instantiate(digitPrefab), ...);

  (Assets/Scripts/UI/GamePlay/Effects/NumberImagePool.cs)

  ## Factory

  노트 타입에 따라 프리팹을 선택해 생성.

  switch (noteInfo.notetype)
  {
      case NoteType.smallRed: prefab = SmallDon; break;
      ...
  }

  (Assets/Scripts/Note/NoteManager1.cs)

  ———

  # Part 3. Core 시스템 분석

  ## System (서비스 접근점)

  - Single.System에서 주요 매니저에 대한 전역 접근 제공
  - 씬 전환과 무관하게 유지 (DontDestroyOnLoad)

  ## SceneManager (씬 전환 및 페이드)

  - SceneData 목록에서 활성 씬 오브젝트를 교체
  - DOTween으로 Fade In/Out 연출

  ## InputManager (입력 라우팅)

  - VR/Keyboard 분기 처리
  - GetInput()으로 게임 전반의 입력 규격 통일

  ———

  # Part 4. 게임플레이 흐름

  ## 노트 생성

  - NoteManager1이 NoteMap의 스폰 타이밍을 기반으로 생성
  - 음악 재생 시간과 noteSpawnOffset을 비교하여 스폰

  ## 타이밍 판정

  - TimingManager에서 가장 가까운 노트 거리 계산
  - HitResult에 따라 판정 구분

  ## 스코어/콤보 갱신

  - ScoreData.ApplyHit()에서 점수/콤보/데드게이지 처리
  - PlayController가 이벤트를 발행하여 UI가 반응

  ———

  # 더 나아가서 고민해볼 것

  - GameEvents를 인터페이스 기반 이벤트 버스 혹은 채널 시스템으로 개선할지
  - NoteManager의 스폰 로직을 데이터 드리븐(스크립터블 데이터/패턴 테이블)로 확장할지
  - Single.System 대신 DI(Container)로 전환하여 테스트 가능성 확보할지