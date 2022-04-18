using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    //Where is the state in its progression
    public enum ExecutionState
    {
        NONE,
        ACTIVE,
        COMPLETED,
        TERMINATED
    }

    //Abstraction of different Fsm-States
    public enum FsmStateType
    {
        IDLE,
        PATROL,
        CHASE,
        FLANKING
    }
    
    public abstract class FsmState : ScriptableObject
    {
        public Material stateMaterial;
        
        protected NavMeshAgent navMeshAgent;
        protected Npc npc;
        protected FiniteStateMachine fsm;
        protected AIGroupManager groupManager;
        
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

        
        //Receive the dependencies for the scriptable object.
        public virtual void SetNavMeshAgent(NavMeshAgent agent)
        {
            if (agent != null)
                navMeshAgent = agent;
        }
        
        public virtual void SetExecutingFsm(FiniteStateMachine fsmToSet)
        {
            if (fsmToSet != null)
                fsm = fsmToSet;
        }

        public virtual void SetExecutionNpc(Npc npcToSet)
        {
            if (npcToSet != null)
                npc = npcToSet;
        }

        public virtual void SetGroupManager(AIGroupManager aiGroupManagerToSet)
        {
            if (aiGroupManagerToSet != null)
                groupManager = aiGroupManagerToSet;
        }
    }
}
