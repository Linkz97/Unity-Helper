using UnityEngine;

namespace LinkzJ.Games.Animations
{
    public abstract class BaseState : IState
    {
        protected readonly PlayerController player;
        protected readonly Animator animator;   
        
        protected const float crossFadeDuration = 0.1f;

        protected BaseState(PlayerController player, Animator animator)
        {
            this.player = player;
            this.animator = animator;
        }

        public bool isCompleteded { get; set; }
        public virtual void OnEnter() { Debug.Log($"State Enter <color=green>{GetType()}</color>"); }

        public virtual void Update() { }

        public virtual void OnExit() { Debug.Log($"State Exit <color=red>{GetType()}</color>"); }

        public virtual void FixedUpdate() { }
    }
}