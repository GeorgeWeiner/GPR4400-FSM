using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    [RequireComponent(typeof(NavMeshAgent), typeof(FiniteStateMachine))]
    public class Npc : MonoBehaviour
    {
        [SerializeField] private List<NpcPatrolPoint> patrolPoints;
        [SerializeField] private float playerDetectionRadius;
        [SerializeField] private LayerMask layerMask;

        [SerializeField] private float speedPatrolling;
        [SerializeField] private float speedChasing;

        private NavMeshAgent _agent;
        private FiniteStateMachine _fsm;

        private Collider[] _targets;
        private Collider _closestCollider;
        private float _closestTarget;


        public List<NpcPatrolPoint> PatrolPoints => patrolPoints;
        public bool IsChasing { get; private set; }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _fsm = GetComponent<FiniteStateMachine>();
        }

        private void Update()
        {
            TargetInChaseRange();
        }

        private void TargetInChaseRange()
        {
            _targets = Physics.OverlapSphere(transform.position, playerDetectionRadius, layerMask);
            if (_targets.Length != 0)
            {
                Debug.LogFormat("Detected {0} colliders", _targets.Length);
                if (!IsChasing) 
                {
                    _fsm.EnterState(FsmStateType.CHASE);
                }

                _agent.speed = speedChasing;
                IsChasing = true;
            }
            else
            {
                _agent.speed = speedPatrolling;
                IsChasing = false;
            }
        }
        
        //Detect the closest target in the area.
        public Transform DetectTargets()
        {
            if (_targets.Length == 0) return null;
            
            _closestTarget = Vector3.Distance(_targets[0].transform.position, transform.position);
            foreach (var target in _targets)
            {
                if (Vector3.Distance(target.transform.position, transform.position) <= _closestTarget)
                {
                    _closestCollider = target;
                }
            }
            return _closestCollider.transform;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
        }
    }
}