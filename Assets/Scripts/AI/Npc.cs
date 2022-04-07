using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    [RequireComponent(typeof(NavMeshAgent), typeof(FiniteStateMachine))]
    public class Npc : MonoBehaviour
    {
        [SerializeField] private List<NpcPatrolPoint> patrolPoints;

        private NavMeshAgent _agent;
        private FiniteStateMachine _fsm;
        
        public List<NpcPatrolPoint> PatrolPoints => patrolPoints;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _fsm = GetComponent<FiniteStateMachine>();
        }
    }
}