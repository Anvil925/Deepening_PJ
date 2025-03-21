using UnityEngine;

namespace DeepeningPJ
{
    public interface IState 
    {
        public void Enter(); // 시작
        public void Exit(); // 종료
        public void HandleInput(); // 입력 처리
        public void Update(); // 업데이트
        public void FixedUpdate(); // 물리 업데이트
        public void OnAnimationEnterEvent(); // 애니메이션 시작
        public void OnAnimationExitEvent(); // 애니메이션 종료
        public void OnAnimationTransitionEvent(); // 애니메이션 전환
        public void OnTriggerEnter(Collider collider); // 트리거 진입
        public void OnTriggerExit(Collider collider); // 트리거 탈출
    }
}
