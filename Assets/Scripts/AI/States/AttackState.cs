using UnityEngine;

namespace AI.States
{
    [CreateAssetMenu(fileName = "Attack State", menuName = "States/Attack", order = 4)]
    public class AttackState : FsmState
    {
        [Header("Attack Stats")]
        [SerializeField] private float attackIntervalDuration;
        [SerializeField] private float minAttackDuration;
        [SerializeField] private Attack attack;

        private float _totalDuration;
        private float _attackBuffer;
        private Attack _attackInstance;
        
        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FsmStateType.ATTACKING;
        }

        public override bool EnterState()
        {
            if (base.EnterState())
            {
                Debug.Log("Entered Attacking State");
                EnteredState = true;
                
                _attackInstance = Instantiate(attack);
                _attackInstance.SetExecutionNpc(npc);
            }

            return EnteredState;
        }

        public override void UpdateState()
        {
            if (npc.IsAttacking)
            {
                _attackBuffer = 0f;
                
                if (_totalDuration < attackIntervalDuration)
                {
                    _totalDuration += Time.deltaTime;
                }
                else
                {
                    //Execute the assigned attack method.
                    _attackInstance.ExecuteAttack(npc.Player);
                    _totalDuration = 0f;
                }
            }

            else
            {
                _attackBuffer += Time.deltaTime;
                
                if (_attackBuffer >= minAttackDuration)
                    fsm.EnterState(FsmStateType.CHASE);
            }
        }
    }
}