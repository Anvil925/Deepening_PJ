using System;
using System.Collections.Generic;
using UnityEngine;

namespace DeepeningPJ
{
    [CreateAssetMenu(fileName = "Player", menuName = "Custom/CostumeIndex")]
    public class CostumeSO : ScriptableObject
    {
        public int[] costumeIndexes = new int[Enum.GetValues(typeof(CostumePart)).Length];

        public int GetCostumeIndex(CostumePart part)
        {
            return costumeIndexes[(int)part];
        }

        public void SetCostumeIndex(CostumePart part, int value)
        {
            costumeIndexes[(int)part] = value;
        }
    }
}
