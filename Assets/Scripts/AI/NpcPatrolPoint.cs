using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AI
{
    public class NpcPatrolPoint : MonoBehaviour
    {
        [SerializeField] private float gizmoRadius;
        [SerializeField] private float connectionRadius;

        private readonly List<NpcPatrolPoint> _patrolPoints = new List<NpcPatrolPoint>();
        
        private void Start()
        {
            var allPatrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
            foreach (var patrolPoint in allPatrolPoints)
            {
                if (Vector3.Distance(this.transform.position, patrolPoint.transform.position) <= connectionRadius)
                {
                    _patrolPoints.Add(patrolPoint.GetComponent<NpcPatrolPoint>());
                } 
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, gizmoRadius);
        }
    }
}