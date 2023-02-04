using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMove : MonoBehaviour
{

    public NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (LookForGameObject(out RaycastHit hit))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("whatIsGround"))
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
    }

    private bool LookForGameObject(out RaycastHit hit)
    {
        //print("Busco algo..");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << LayerMask.NameToLayer("whatIsGround");
        return Physics.Raycast(ray, out hit, 1000f, layerMask);
    }
}
