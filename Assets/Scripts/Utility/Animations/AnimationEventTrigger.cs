using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeepeningPJ
{
    public class AnimationEventTrigger : MonoBehaviour
    {
        private PlayerHandler player;

        private void Awake()
        {
            player = transform.parent.GetComponent<PlayerHandler>();
        }

        public void TriggerOnMovementStateAnimationEnterEvent()
        {
            if (IsInAnimationTransition()) return;
            player.OnMovementStateAnimationEnterEvent();
        }

        public void TriggerOnMovementStateAnimationExitEvent()
        {
            if (IsInAnimationTransition()) return;
            player.OnMovementStateAnimationExitEvent();
        }

        public void TriggerOnMovementStateAnimationTransitionEvent()
        {
            if (IsInAnimationTransition()) return;
            player.OnMovementStateAnimationTransitionEvent();
        }

        private bool IsInAnimationTransition(int layerIndex = 0)
        {
            return player.Animator.IsInTransition(layerIndex);
        }
    }
}
