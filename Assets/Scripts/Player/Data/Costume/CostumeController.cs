using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace DeepeningPJ
{

    // 설명 : 부위별 코스튬을 장착/해제
    // 코스튬 부위
    public enum CostumePart
    {
        Hat,
        Hair,
        Glass,
        Face,
        Hand,
        Outwear,
        Pant,
        Sock,
        Shoes,
    }
    public class CostumeController : MonoBehaviour
    {
        public CostumeSO costumeSO;
        public List<GameObject> Hat;
        public List<GameObject> Hair;
        public List<GameObject> Glass;
        public List<GameObject> Face;
        public List<GameObject> Hand;
        public List<GameObject> Outwear;
        public List<GameObject> Pant;
        public List<GameObject> Sock;
        public List<GameObject> Shoes;

        void Start()
        {
            StartCoroutine(LoadCostume());
        }
        public IEnumerator LoadCostume()
        {
            yield return null;
            // 정보가 있으면 코스튬 설정
            for (int i = 0; i < costumeSO.costumeIndexes.Length; i++)
            {
                Equip((CostumePart)i, costumeSO.GetCostumeIndex((CostumePart)i));
            }
        }
        public int GetLength(CostumePart costumePart)
        {
            return GetCostumeArray(costumePart).Count;
        }

        // 장착 / 탈착
        public void Equip(CostumePart costumePart, int index)
        {
            // 특정 부위의 인덱스로 활성/비활성화, ndex = 0은 부위 해제
            List<GameObject> arr = GetCostumeArray(costumePart);

            if (arr != null)
            {
                if (index == 0)
                {
                    // index가 0이면 모든 요소를 비활성화
                    foreach (var costume in arr)
                    {
                        costume.SetActive(false);
                    }
                    return;
                }

                // 0이 아니면
                for (int i = 1; i <= arr.Count; i++)
                {
                    if (i != index)
                    {
                        arr[i - 1].SetActive(false);
                    }
                    else
                    {
                        // 활성화, 0이 해제이므로 i-1
                        arr[i - 1].SetActive(true);
                    }
                }
            }
        }
        // 부위 목록 반환
        private List<GameObject> GetCostumeArray(CostumePart costumePart)
        {
            // 배열을 반환하는 로직
            return costumePart switch
            {
                CostumePart.Hat => Hat,
                CostumePart.Hair => Hair,
                CostumePart.Glass => Glass,
                CostumePart.Face => Face,
                CostumePart.Hand => Hand,
                CostumePart.Outwear => Outwear,
                CostumePart.Pant => Pant,
                CostumePart.Sock => Sock,
                CostumePart.Shoes => Shoes,
                _ => null,
            };
        }
        public void AddCostume(CostumePart costumePart, GameObject costume)
        {
            List<GameObject> arr = GetCostumeArray(costumePart);
            arr.Add(costume);
        }
    }
}
