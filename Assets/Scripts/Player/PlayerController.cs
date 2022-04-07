using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Camera _cam;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _cam = Camera.main;
    }
    
    private void Update()
    {
        BasicMovement();
    }

    private void BasicMovement()
    {
        var ray = _cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out var hit);
        
        Debug.DrawRay(ray.origin, ray.direction, Color.green);

        if (!Input.GetMouseButtonDown(0)) return;
        _agent.SetDestination(hit.point);
    }
}
