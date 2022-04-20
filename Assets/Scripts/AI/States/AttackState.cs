using UnityEngine;

namespace AI.States
{
    [CreateAssetMenu(fileName = "Attack State", menuName = "States/Attack", order = 4)]
    public class AttackState : FsmState
    {
        [SerializeField] private float attackIntervalDuration;

        [SerializeField] private Attack attack;

        private float _totalDuration;
        private Attack _attackInstance;
        
        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FsmStateType.ATTACKING;

            _attackInstance = Instantiate(attack);
            _attackInstance.SetExecutionNpc(npc);
        }

        public override bool EnterState()
        {
            if (base.EnterState())
            {
                Debug.Log("Entered Attacking State");
                EnteredState = true;
            }

            return EnteredState;
        }

        public override void UpdateState()
        {
            if (npc.CanAttack)
            {
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
                fsm.EnterState(FsmStateType.CHASE);
            }
        }
    }
}