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
        
        protected void Awake()
        {
            _currentState = null;

            _fsmStates = new Dictionary<FsmStateType, FsmState>();

            var navMeshAgent = GetComponent<NavMeshAgent>();
            var npc = GetComponent<Npc>();
            
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
            if (startingState != null)
            {
                EnterState(startingState);
            } 
        }

        protected void Update()
        {
            if (_currentState != null)
            {
                _currentState.UpdateState();
            }
        }

        public void EnterState(FsmState state)
        {
            if (startingState == null) return;

            if (_currentState != null)
            {
                _currentState.ExitState();
            }
            
            _currentState = state;
            _currentState.EnterState();
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