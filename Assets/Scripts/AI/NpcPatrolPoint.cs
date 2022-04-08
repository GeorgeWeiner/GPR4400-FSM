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
                if (Vector3.Distance(this.transform.position, patrolPoint.transform.position) <= connectionRadius && patrolPoint != this.gameObject)
                {
                    _patrolPoints.Add(patrolPoint.GetComponent<NpcPatrolPoint>());
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, gizmoRadius);
        }
    }
}