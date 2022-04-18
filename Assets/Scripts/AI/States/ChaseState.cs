﻿using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace AI.States
{
    [CreateAssetMenu(fileName = "Chase State", menuName = "States/Chase", order = 3)]
    
    public class ChaseState : FsmState
    {
        [SerializeField] private float minFlankingDistance;
        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FsmStateType.CHASE;
        }
        
        public override bool EnterState()
        {
            EnteredState = false;
            if (base.EnterState())
            {
                Debug.Log("Entered Chase State");
                EnteredState = true;
            }

            return EnteredState;
        }
        
        public override void UpdateState()
        {
            //if (!npc.IsChasing)
            //{
            //    fsm.EnterState(FsmStateType.IDLE);
            //}
            
            // Need to make sure we've successfully entered the state.
            if (EnteredState)
            {
                SetDestination(npc.Player);
            }
        }

        public override bool ExitState()
        {
            Debug.Log("Exited Chase State");
            SetDestination(npc.transform);
            return base.ExitState();
        }

        private void SetDestination(Transform chaseTarget)
        {
            if (navMeshAgent != null && chaseTarget != null)
            {
                if (Vector3.Distance(npc.transform.position, chaseTarget.transform.position) >= minFlankingDistance)
                {
                    //navMeshAgent.SetDestination()
                }
                
                navMeshAgent.SetDestination(chaseTarget.position);
            }
        }
        /*
        * If the target is too far away from this instance,
        * Initiate the flanking maneuver,
        * this will be achieved, by picking a random way-point in the general direction of the target.
        * After reaching the waypoint, the instance will switch to the target.
        */
        private void InitiateFlankingBehaviour()
        {
            
        }
    }
}