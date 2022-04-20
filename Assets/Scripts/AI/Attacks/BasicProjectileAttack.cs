using UnityEngine;

namespace AI.Attacks
{
    [CreateAssetMenu(fileName = "Projectile Attack", menuName = "Attacks/Projectile Attack", order = 1)]
    public class BasicProjectileAttack : Attack
    {
        [SerializeField] private GameObject projectile;
        public override void ExecuteAttack(Transform target)
        {
            Transform transform;
            
            (transform = npc.transform).LookAt(npc.Player); // This needs to be interpolated in some way. Probably through the nav-mesh agent.
            Instantiate(projectile, transform.position, transform.rotation);
        }
    }
}