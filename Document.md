# Saboteur

창 크기 1280*720

1. Menu
    - 방 만들기 버튼, 방 참가 버튼, 배경 이미지
        1) **방 만들기** (`CreateRoom`)
            - 방 이름 입력
            - 서버에서 `RoomCode` 생성 (1000부터 차례대로)
            - 방 만든 사람은 `Creator`
        2) **방 참가** (`JoinRoom`)
            - 버튼 누르면 애니메이션 적용해서 TextField 띄운 후,
            `RoomCode` 입력해서 방 참가
            - 참가한 사람은 `Member`
            - 그 방에 7명이 있다면 참가 불가
    
2. Room
    - 플레이어 표시, 채팅창, 시작 버튼(방장만 보이게), 나가기 버튼
        1) **플레이어 표시**
            - 1~7까지 들어온 순서대로 `PlayerNumber` 부여
            - 할당된 플레이어 번호 옆에 랜턴 불 켜짐
            - 중간에 한 명이 나가면 다음 들어오는 사람이 그 자리 차지
                - 나간 상태로 시작하면 번호 유지한 채 시작
                    - (1, 2, 3, 4 -> 3 나감 -> 1, 2, 4 -> 두 명 들어옴 -> 1, 2, 3, 4, 5(3, 5 채움) -> 3 나감 -> 1, 2, 4, 5 -> 시작)
        2) **채팅창**
            - 클라이언트가 서버로 채팅 보내면, 모든 클라이언트는 서버에 내용 변경되면 채팅 내용 전체 다시 가져옴
            - 대화 내용이 100개가 넘어가면 최근 기한 100개만 가져옴
        3) **시작 버튼**
            - 방장만 보이게 표시
            - 시작 버튼을 누르면 게임 시작
        4) **나가기 버튼**
            - `Creator`, `Member` 모두 나갈 수 있음
            - `Creator` 나가면 (최초 생성 시 방장이 무조건 1번) 최소값의 `PlayerNumber`가 방장
                - 1, 2, 3, 4, 5 (1 방장) -> 1 나감 -> 2, 3, 4, 5 (2 방장) -> 1 들어오고 2 나감 -> 1, 3, 4, 5 (1 방장)

3. InGame
    - `Player`, `Field`, `Hand`, `DeckInfo`로 화면 분할
        1) `Player`
            - `Tool(Pickaxe, Cart, Lantern)`, `PlayerNumber`, `CardNumber` 표시
        2) `Field`
            - `Cards` 표시, `Hand`에 있는 `Card`를 Dragging으로 `Field`에 가져다 놓아서 게임 진행
                - 가져다 놓을 때 서버에서 유효한 위치인지 확인
        3) `Hand`
            - 본인이 가지고 있는 카드 표시
        4) `DeckInfo` 
            - `Deck`, `UsedCard` 표시
                - `Deck`에는 남은 카드의 수 표시
                - `UsedCard`엔 버려진 카드 표시, 클릭 시 `Front`로 버려진 카드들 표시

*. Utility
    - `enum Screen`
        1) `MainMenu`, `Game`, `Room`
    - `ViewController`
        1) `ViewController.SwitchScreen(Screen s)`
            - `Screen.*`로 원하는 화면 
