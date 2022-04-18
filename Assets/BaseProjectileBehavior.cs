using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BaseProjectileBehavior : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    
    private Rigidbody _rb;
    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Creature>() != null)
        {
            other.GetComponent<Creature>().TakeDamage(projectileDamage);
        }
    }
}
