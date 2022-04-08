using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class FiniteStateMachine : MonoBehaviour
    {
        [SerializeField] private FsmState startingState;
        [SerializeField] private List<FsmState> validStates;
        
        private FsmState _currentState;
        private Dictionary<FsmStateType, FsmState> _fsmStates;

        private MeshRenderer _renderer;

        protected void Awake()
        {
            _currentState = null;

            _fsmStates = new Dictionary<FsmStateType, FsmState>();
            _renderer = GetComponent<MeshRenderer>();

            var navMeshAgent = GetComponent<NavMeshAgent>();
            var npc = GetComponent<Npc>();
            
            //We pass the values into the Scriptable Object (Dependency Injection).
            foreach (var state in validStates)
            {
                state.SetExecutingFsm(this);
                state.SetExecutionNpc(npc);
                state.SetNavMeshAgent(navMeshAgent);
                
                _fsmStates.Add(state.StateType, state);
            }
        }

        protected void Start()
        {
            //Enter the first state
            if (startingState != null)
            {
                EnterState(startingState);
            } 
        }

        protected void Update()
        {
            //Update the state (I.E move, or shoot or anything)
            if (_currentState != null)
            {
                _currentState.UpdateState();
            }
        }

        //Two different methods to enter a new state (They call the same thing and just take different parameters)
        public void EnterState(FsmState state)
        {
            if (startingState == null) return;
            if (_currentState != null)
            {
                _currentState.ExitState();
            }
            
            _currentState = state;
            _currentState.EnterState();

            _renderer.material = _currentState.stateMaterial;
        }

        public void EnterState(FsmStateType stateType)
        {
            if (_fsmStates.ContainsKey(stateType))
            {
                var nextState = _fsmStates[stateType];
                EnterState(nextState);
            }
        }
    }
}