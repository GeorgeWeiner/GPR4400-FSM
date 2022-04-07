using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace AI.States
{
    [CreateAssetMenu(fileName = "Idle State", menuName = "States/Idle", order = 1)]
    public class IdleState : FsmState
    {
        [SerializeField] private float duration;

        private float _totalDuration;
        public override void OnEnable()
        {
            base.OnEnable();
            StateType = FsmStateType.IDLE;
        }

        public override bool EnterState()
        {
            EnteredState = base.EnterState();

            if (EnteredState)
            {
                Debug.Log("Entered Idle State");
                _totalDuration = 0f;
            }
            return EnteredState;
        }

        public override bool ExitState()
        {
            base.ExitState();
            Debug.Log("Exited Idle State");

            return true;
        }

        public override void UpdateState()
        {
            if (EnteredState)
            {
                _totalDuration += Time.deltaTime;

                if (_totalDuration >= duration)
                {
                    fsm.EnterState(FsmStateType.PATROL);
                }
                Debug.LogFormat("Updating Idle State {0}", _totalDuration);
            }
        }
    }
}
