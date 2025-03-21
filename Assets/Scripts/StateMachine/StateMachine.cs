using UnityEngine;

namespace DeepeningPJ
{
    public abstract class StateMachine
    {
        protected IState currentState; // 현재 상태를 저장할 변수

        public void ChangeState(IState newState) // 상태 전환 함수
        {
            currentState?.Exit(); // 현재 상태가 null이 아니면 Exit 함수 실행

            currentState = newState; // 현재 상태를 newState로 변경

            currentState.Enter(); // newState의 Enter 함수 실행
        }

        public void HandleInput() // 입력 처리 함수
        {
            currentState?.HandleInput(); // 현재 상태가 null이 아니면 HandleInput 함수 실행
        }
        
        public void Update() // 업데이트 함수
        {
            currentState?.Update(); // 현재 상태가 null이 아니면 Update 함수 실행
        }

        public void FixedUpdate() // 물리 업데이트 함수
        {
            currentState?.FixedUpdate(); // 현재 상태가 null이 아니면 FixedUpdate 함수 실행
        }

        public void OnAnimationEnterEvent() // 애니메이션 시작 함수
        {
            currentState?.OnAnimationEnterEvent(); // 현재 상태가 null이 아니면 OnAnimationEnterEvent 함수 실행
        }

        public void OnAnimationExitEvent() // 애니메이션 종료 함수
        {
            currentState?.OnAnimationExitEvent(); // 현재 상태가 null이 아니면 OnAnimationExitEvent 함수 실행
        }

        public void OnAnimationTransitionEvent() // 애니메이션 전환 함수
        {
            currentState?.OnAnimationTransitionEvent(); // 현재 상태가 null이 아니면 OnAnimationTransitionEvent 함수 실행
        }

        public void OnTriggerEnter(Collider collider) // 트리거 진입 함수
        {
            currentState?.OnTriggerEnter(collider); // 현재 상태가 null이 아니면 OnTriggerEnter 함수 실행
        }

        public void OnTriggerExit(Collider collider) // 트리거 탈출 함수
        {
            currentState?.OnTriggerExit(collider); // 현재 상태가 null이 아니면 OnTriggerExit 함수 실행
        }
    }
}
