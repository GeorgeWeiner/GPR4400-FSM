using UnityEngine;

namespace AI
{
    public abstract class Attack : ScriptableObject
    {
        protected Npc npc;
        
        public void SetExecutionNpc(Npc npcToSet)
        {
            if (npcToSet != null)
                npc = npcToSet;
        }
        
        public abstract void ExecuteAttack(Transform target);
    }
}