using UnityEngine;
using LinkzJ.Games.Animations;

// This is a sample of How to Setup the State Machine
public class PlayerController : MonoBehaviour
{
    private PlayerState_Locomotion locomotionState;
    private StateMachine stateMachine;
    private Animator animator;
    protected virtual void Awake()
    {
        // Get Animator
        animator = GetComponent<Animator>();
        
        // State Machine
        stateMachine = new();

        // Define State
        locomotionState = new PlayerState_Locomotion(this, animator);

        // Define Transition
        // Define Your Own Logic for Transition Here
        Any(locomotionState, new FuncPredicate(() => true));

        // Force State Machine To Idle/Locomotion State On Awake
        stateMachine.SetState(locomotionState);
    }

    // State Transitions Method
    void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
    void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

}
