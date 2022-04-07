using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public enum ExecutionState
    {
        NONE,
        ACTIVE,
        COMPLETED,
        TERMINATED
    }

    public enum FsmStateType
    {
        IDLE,
        PATROL
    }
    
    public abstract class FsmState : ScriptableObject
    {
        protected NavMeshAgent navMeshAgent;
        protected Npc npc;
        protected FiniteStateMachine fsm;
        
        public virtual void OnEnable()
        {
            ExecutionState = ExecutionState.NONE;
        }

        public ExecutionState ExecutionState { get; protected set; }
        public bool EnteredState { get; protected set; }
        public FsmStateType StateType { get; protected set; }

        public virtual bool EnterState()
        {
            ExecutionState = ExecutionState.ACTIVE;
            
            //Does the navmesh-agent exist?
            var success = navMeshAgent != null && npc != null;
            return success;
        }

        public abstract void UpdateState();

        public virtual bool ExitState()
        {
            ExecutionState = ExecutionState.COMPLETED;
            return true;
        }

        public virtual void SetNavMeshAgent(NavMeshAgent agent)
        {
            if (agent != null)
            {
                navMeshAgent = agent;
            }
        }

        public void SetExecutingFsm(FiniteStateMachine fsmToSet)
        {
            if (fsmToSet != null)
                fsm = fsmToSet;
        }

        public void SetExecutionNpc(Npc npcToSet)
        {
            if (npcToSet != null)
            {
                npc = npcToSet;
            }
        }
    }
}
