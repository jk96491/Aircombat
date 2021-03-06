# Aircombat
비행 슈팅게임(space invaders, 갤러그 등등과 유사한)에 대한 Reinforcement Learning Test

   <img src="https://user-images.githubusercontent.com/17878413/75980650-4046d000-5f26-11ea-8bbb-6cfd7b7d04eb.png" width = 23%></img>

텐서플로와 유니티 ML-Agents로 배우는 강화학습 - https://wikibook.co.kr/tensorflow-mlagents/ 

   <img src="https://user-images.githubusercontent.com/17878413/75980924-c4995300-5f26-11ea-94f8-ebcc139de8a4.png" width = 17%></img>

위 서적을 참고하여 제작함.

DDPG 기법 사용 - https://arxiv.org/abs/1509.02971

# 튜토리얼

비행슈팅게임에 강화학습 적용하기 - https://blog.naver.com/jk96491/221841840402

# 환경설정
Unity3D(2019.1.14f1)  https://unity3d.com/kr/get-unity/download/archive

pycharm https://www.jetbrains.com/ko-kr/pycharm/download/#section=windows
      
Unity3D 설치 후 Check out 받은 위치에서 trunk 폴더 위치에서 실행 하면 됩니다.

ml-agents 0.10.0 버전으로 다운로드 하시기 바랍니다. https://github.com/Unity-Technologies/ml-agents/releases/tag/0.10.0

pycharm 설치 후 trunk\AircombatPython 위치에서 실행 하시면 됩니다

아나콘다 프롬프트를 열어 pip install mlagents==0.10.0 으로 설치해 주시기 바랍니다.

그 이후 pip install mlagents-envs==0.10.0 로 설치 바랍니다.

tensorflow 1.13.1 버전 

# Unity 빌드 
왼쪽 상단에 File-Build Settings윈도우 에서 build 버튼 클릭!

build후 해당 exe파일은 trunk\AircombatPython\UnityTesting 위치에 Aircombat.exe 생성

# 학습 실행

pycharm 에서 main.py 실행하기만 하면 됨(디렉터리 설정은 모두 세팅되어 있음!)
