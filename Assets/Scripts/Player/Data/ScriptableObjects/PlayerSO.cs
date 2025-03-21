using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeepeningPJ
{
    [CreateAssetMenu(fileName = "Player", menuName = "Custom/Characters/Player")] // ScriptableObject 메뉴에 추가
    public class PlayerSO : ScriptableObject // ScriptableObject 상속
    {
        [field: SerializeField] public PlayerGroundedData GroundedData {  get; private set; } // GroundedData 변수를 SerializeField로 노출
        [field: SerializeField] public PlayerAirborneData AirborneData {  get; private set; } // AirborneData 변수를 SerializeField로 노출
    }
}
