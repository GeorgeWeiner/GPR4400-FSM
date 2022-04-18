using UnityEngine;

namespace AI.States
{
    public class FlankingState : FsmState
    {
        public override void OnEnable()
        {
            base.OnEnable();

            StateType = FsmStateType.FLANKING;
        }

        public override bool EnterState()
        {
            return base.EnterState();
        }

        public override void UpdateState()
        {
            throw new System.NotImplementedException();
        }

        public override bool ExitState()
        {
            return base.ExitState();
        }
    }
}