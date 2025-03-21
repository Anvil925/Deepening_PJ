using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DeepeningPJ
{
    public class PlayerInput : MonoBehaviour
    {
        public PlayerInputActions InputActions { get; private set; } // PlayerInputActions 객체를 저장할 변수
        public PlayerInputActions.PlayerActions PlayerActions { get; private set; } // PlayerActions 객체를 저장할 변수

        private void Awake()
        {
            InputActions = new PlayerInputActions(); // PlayerInputActions 객체 생성

            PlayerActions = InputActions.Player; // PlayerActions 객체를 PlayerActions 변수에 저장
        }

        private void OnEnable()
        {
            InputActions.Enable(); // InputActions 활성화
        }

        private void OnDisable()
        {
            InputActions.Disable(); // InputActions 비활성화
        }

        public void DisableActionFor(InputAction action, float seconds) // 입력 처리를 비활성화하는 함수
        {
            StartCoroutine(DisableAction(action, seconds)); // DisableAction 코루틴 실행
        }

        private IEnumerator DisableAction(InputAction action, float seconds) // 입력 처리를 비활성화하는 코루틴
        {
            action.Disable(); // 입력 처리 비활성화

            yield return new WaitForSeconds(seconds); // seconds만큼 대기

            action.Enable(); // 입력 처리 활성화
        }
    }
}