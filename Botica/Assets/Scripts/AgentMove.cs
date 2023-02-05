using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMove : MonoBehaviour
{

    public NavMeshAgent agent;
    private GameObject objectSeen;
    private GameObject pointSeen;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(new Vector3(agent.pathEndPosition.x, transform.position.y, agent.pathEndPosition.z));
        if (Input.GetMouseButtonDown(0))
        {
            if (objectSeen)
            {
                NavMeshHit navMeshHit;
                if (NavMesh.SamplePosition(objectSeen.transform.position, out navMeshHit, 2.0f, NavMesh.AllAreas))
                {
                    agent.SetDestination(navMeshHit.position);
                }
                else {
                    agent.SetDestination(objectSeen.transform.position);

                }
            } else if(pointSeen){
                NavMeshHit navMeshHit;
                if (NavMesh.SamplePosition(pointSeen.transform.position, out navMeshHit, 2.0f, NavMesh.AllAreas))
                {
                    agent.SetDestination(navMeshHit.position);
                }
                else
                {
                    agent.SetDestination(pointSeen.transform.position);

                }
            }
            else if (LookForGameObject(out RaycastHit hit))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("whatIsGround"))
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
        else if (LookForGameObject(out RaycastHit hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("whatIsInteractable"))
            {
                if(objectSeen) objectSeen.GetComponent<InteractionObject>().DeactivateImage();
                hit.collider.gameObject.GetComponent<InteractionObject>().ActivateImage();
                objectSeen = hit.collider.gameObject;
            } else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("whatIsPoint"))
            {
                if (pointSeen) pointSeen.GetComponent<InteractionPoint>().DeactivateImage();
                hit.collider.gameObject.GetComponent<InteractionPoint>().ActivateImage();
                pointSeen = hit.collider.gameObject;
            }
            else if (objectSeen)
            {
                objectSeen.GetComponentInChildren<InteractionObject>().DeactivateImage();
                objectSeen = null;
            }
            else if (pointSeen)
            {
                pointSeen.GetComponentInChildren<InteractionPoint>().DeactivateImage();
                pointSeen = null;
            }
        }
        else 
        {
            if (objectSeen) {
                objectSeen.GetComponentInChildren<InteractionObject>().DeactivateImage();
                objectSeen = null;
            }
            if (pointSeen)
            {
                pointSeen.GetComponentInChildren<InteractionPoint>().DeactivateImage();
                pointSeen = null;
            }
        }
    }

    private bool LookForGameObject(out RaycastHit hit)
    {
        //print("Busco algo..");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = (1 << LayerMask.NameToLayer("whatIsGround")) | (1 << LayerMask.NameToLayer("whatIsInteractable") | (1 << LayerMask.NameToLayer("whatIsPoint")));
        return Physics.Raycast(ray, out hit, 1000f, layerMask);
    }

    private void InteractWithObject(GameObject objectClicked)
    {
        ItemObject itemClicked = objectClicked.GetComponent<ItemObject>();
        if (itemClicked)
            itemClicked.Interactuate();
    }

    public void GoToPoint(Vector3 position){

    }
}
