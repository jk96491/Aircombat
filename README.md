# Aircombat
비행 슈팅게임(space invaders, 갤러그 등등과 유사한)에 대한 Reinforcement Learning Test

텐서플로와 유니티 ML-Agents로 배우는 강화학습 - https://wikibook.co.kr/tensorflow-mlagents/ 
github - https://github.com/reinforcement-learning-kr/Unity_ML_Agents

위 서적을 참고하여 제작함.

DDPG 기법 사용 - https://arxiv.org/abs/1509.02971

# 환경설정
Unity3D(2019.1.14f1)  https://unity3d.com/kr/get-unity/download/archive

pycharm https://www.jetbrains.com/ko-kr/pycharm/download/#section=windows
      
Unity3D 설치 후 Check out 받은 위치에서 trunk 폴더 위치에서 실행 하면 됩니다.

pycharm 설치 후 trunk\AircombatPython 위치에서 실행 하시면 됩니다


# Unity 빌드 
왼쪽 상단에 File-Build Settings윈도우 에서 build 버튼 클릭!

build후 해당 exe파일은 trunk\AircombatPython\UnityTesting 위치에 Aircombat.exe 생성

# 학습 실행

pycharm 에서 Aircombat.py 실행하기만 하면 됨(디렉터리 설정은 모두 세팅되어 있음!)