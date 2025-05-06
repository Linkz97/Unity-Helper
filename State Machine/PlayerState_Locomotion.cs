using UnityEngine;

namespace LinkzJ.Games.Animations
{
    public class PlayerState_Locomotion : BaseState
    {
        public PlayerState_Locomotion(PlayerController player, Animator animator) : base(player, animator)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            // Cross Fade Animation
            //animator.CrossFade(player.moveX != 0 ? LocomotionHash : LocomotionIdleHash, crossFadeDuration);
        }

        public override void FixedUpdate()
        {
            // Do Your Function Here
        }
    }
}

