using System.Collections.Generic;
using UnityEngine;

namespace AI.States
{
    [CreateAssetMenu(fileName = "Patrol State", menuName = "States/Patrol", order = 2)]
    public class PatrolState : FsmState
    {
        private List<NpcPatrolPoint> _patrolPoints;
        private int _patrolPointIndex;

        public override void OnEnable()
        {
            base.OnEnable();
            _patrolPointIndex = -1;
            
            StateType = FsmStateType.PATROL;
        }

        public override bool EnterState()
        {
            EnteredState = false;
            if (base.EnterState())
            {
                _patrolPoints = npc.PatrolPoints;

                if (_patrolPoints == null || _patrolPoints.Count == 0)
                {
                    Debug.LogError("PatrolState: Failed to grab Patrol Points from the NPC");
                    Debug.Log(_patrolPoints.Count);
                }
                else
                {
                    if (_patrolPointIndex < 0)
                    {
                        _patrolPointIndex = Random.Range(0, _patrolPoints.Count);
                    }
                    else
                    {
                        _patrolPointIndex = (_patrolPointIndex + 1) % _patrolPoints.Count; //Protects from IndexOutOfRangeException
                    }

                    SetDestination(_patrolPoints[_patrolPointIndex]);
                    EnteredState = true;
                }
            }
            return EnteredState;
        }

        private void SetDestination(NpcPatrolPoint patrolPoint)
        {
            if (navMeshAgent != null && patrolPoint != null)
            {
                navMeshAgent.SetDestination(patrolPoint.transform.position);
            }
        }

        public override bool ExitState()
        {
            return base.ExitState(); 
        }
        
        public override void UpdateState()
        {
            // Need to make sure we've successfully entered the state.
            if (EnteredState)
            {
                //If we reached the destination.
                if (Vector3.Distance(navMeshAgent.transform.position, _patrolPoints[_patrolPointIndex].transform.position) <= 1f)
                {
                    fsm.EnterState(FsmStateType.IDLE);
                }
            }
        }
    }
}