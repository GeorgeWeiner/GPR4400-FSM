using UnityEngine;
using UnityEngine.Serialization;

namespace AI.Attacks
{
    [CreateAssetMenu(fileName = "Projectile Attack", menuName = "Attacks/Projectile Attack", order = 1)]
    public class BasicProjectileAttack : Attack
    {
        [SerializeField] private GameObject projectile;
        [SerializeField] private float instantiationOffset;

        [SerializeField] private LayerMask layerMask;
        
        
        public override void ExecuteAttack(Transform target)
        {
            if (npc == null) return;

            Transform transform;
            (transform = npc.transform).LookAt(target); // This needs to be interpolated in some way. Probably through the nav-mesh agent.
            
            if (!Physics.Linecast(transform.position, target.position, layerMask, QueryTriggerInteraction.Ignore))
                Instantiate(projectile, transform.position + transform.forward * instantiationOffset, transform.rotation);
            else
                npc.Agent.SetDestination(target.position); //This creates artificial group coordination
            
            //If one of the agents obscures the target, this agent will set destination towards the player,
            //resulting in group coordination.
        }
    }
}