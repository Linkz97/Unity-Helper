using System;
using System.Collections.Generic;
using UnityEngine;

namespace LinkzJ.Games.Animations
{
    public class StateMachine
    {
        StateNode current;
        Dictionary<Type, StateNode> nodes = new();
        private HashSet<ITransition> anyTransitions = new();

        public void Update()
        {
            var transition = GetTransition();
            if (transition != null)
                ChangeState(transition.To);

            current.State?.Update();
        }

        public void FixedUpdate()
        {
            current.State.FixedUpdate();
        }

        public void SetState(IState state)
        {
            current = nodes[state.GetType()];
            current.State?.OnEnter();
        }

        public void ChangeState(IState state)
        {
            if (state == current.State) return;

            var previousState = current.State;
            var nextState = nodes[state.GetType()];

            previousState?.OnExit();
            nextState.State?.OnEnter();

            current = nextState;
        }

        ITransition GetTransition()
        {
            foreach (var VARIABLE in anyTransitions)
            {
                if (VARIABLE.Condition.Evaluate())
                    return VARIABLE;
            }

            foreach (var VARIABLE in current.Transitions)
            {
                if (VARIABLE.Condition.Evaluate())
                    return VARIABLE;
            }

            return null;
        }

        public void AddAnyTransition(IState to, IPredicate condition)
        {
            anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            Debug.Log($"AddTransition : {from.GetType()}, To : {to.GetType()}");
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        StateNode GetOrAddNode(IState state)
        {
            var node = nodes.GetValueOrDefault(state.GetType());

            if (node == null)
            {
                node = new StateNode(state);
                nodes.Add(state.GetType(), node);
            }
            return node;
        }


        public class StateNode
        {
            public IState State { get; }
            public HashSet<ITransition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<ITransition>();
            }

            public void AddTransition(IState to, IPredicate condition)
            {
                Transitions.Add(new Transition(to, condition));
            }
        }


    }
}

