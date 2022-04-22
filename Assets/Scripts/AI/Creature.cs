using DefaultNamespace;
using UnityEngine;

public abstract class Creature : MonoBehaviour, IDamagable
{
    [SerializeField] private float health;
    
    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        
        if (health <= 0f)
        {
            OnDeath();
        }
    }
    protected abstract void OnDeath();
}