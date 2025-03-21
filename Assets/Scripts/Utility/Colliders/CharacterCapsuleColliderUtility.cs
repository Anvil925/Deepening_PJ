using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeepeningPJ
{
    [Serializable]
    public class CharacterCapsuleColliderUtility : CapsuleColliderUtility
    {
        [field: SerializeField] public PlayerTriggerColliderData TriggerColliderData { get; private set; }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            TriggerColliderData.Initialize();
        }
    }
}
