using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    [RequireComponent(typeof(NavMeshAgent), typeof(FiniteStateMachine))]
    public class Npc : Creature
    {
        [SerializeField] private List<NpcPatrolPoint> patrolPoints;
        [SerializeField] private float viewDistance;
        [SerializeField] private float viewAngle;

        [SerializeField] private float smellRadius;
        
        [SerializeField] private float minAttackDistance;
        
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private LayerMask playerLayerMask;


        [SerializeField] private float speedPatrolling;
        [SerializeField] private float speedChasing;
        
        private List<Npc> _alliedNpcList;

        private NavMeshAgent _agent;
        private FiniteStateMachine _fsm;
        private AIGroupManager _aiGroupManager;

        private Collider[] _targets;
        private Collider _closestCollider;
        private float _closestTarget;

        public Transform Player { get; private set; }
        public List<NpcPatrolPoint> PatrolPoints => patrolPoints;
        public bool IsChasing { get; private set; }
        public bool CanAttack { get; private set; }
        

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _fsm = GetComponent<FiniteStateMachine>();
            _aiGroupManager = FindObjectOfType<AIGroupManager>();

            Player = GameObject.FindWithTag("Player").transform;

            _alliedNpcList = _aiGroupManager.npcsInGroup;
            _alliedNpcList.Remove(this);
        }

        private void Update()
        {
            View();
            Smell();
            Attack();
        }

        private void View()
        {
            if (!(Vector3.Distance(transform.position, Player.position) < viewDistance)) return;
            
            var view = transform;
                
            var directionToPlayer = (Player.position - view.position).normalized;
            var angleToPlayer = Vector3.Angle(view.forward, directionToPlayer);

            if (angleToPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, Player.position, layerMask))
                {
                    if (IsChasing) return;

                    _fsm.EnterState(FsmStateType.CHASE);
                    AlertAllies();
                }

                _agent.speed = speedChasing;
                IsChasing = true;
            }
        }

        private void Smell()
        {
            var playerInRange = Physics.CheckSphere(transform.position, smellRadius, playerLayerMask);

            if (playerInRange)
            {
                if (IsChasing) return;

                _fsm.EnterState(FsmStateType.CHASE);
                AlertAllies();
                
                _agent.speed = speedChasing;
                IsChasing = true;
            }
        }

        private void Attack()
        {
            if (Vector3.Distance(Player.position, transform.position) < minAttackDistance && IsChasing)
            {
                _fsm.EnterState(FsmStateType.ATTACKING);
                CanAttack = true;
            }
            else
            {
                CanAttack = false;
            }
        }

        private void AlertAllies()
        {
            foreach (var ally in _alliedNpcList)
            {
                ally._fsm.EnterState(FsmStateType.CHASE);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, viewDistance);
        }
        
        protected override void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}