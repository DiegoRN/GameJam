using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMove : MonoBehaviour
{

    public NavMeshAgent agent;
    private GameObject objectSeen;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (objectSeen)
            {
                agent.SetDestination(objectSeen.transform.position);
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
                hit.collider.gameObject.GetComponent<InteractionObject>().ActivateImage();
                objectSeen = hit.collider.gameObject;
            }
            else if (objectSeen)
            {
                objectSeen.GetComponentInChildren<InteractionObject>().DeactivateImage();
                objectSeen = null;
            }
        }
        else if (objectSeen)
        {
            objectSeen.GetComponentInChildren<InteractionObject>().DeactivateImage();
            objectSeen = null;
        }
    }

    private bool LookForGameObject(out RaycastHit hit)
    {
        //print("Busco algo..");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = (1 << LayerMask.NameToLayer("whatIsGround")) | (1 << LayerMask.NameToLayer("whatIsInteractable"));
        return Physics.Raycast(ray, out hit, 1000f, layerMask);
    }

    private void InteractWithObject(GameObject objectClicked)
    {
        ItemObject itemClicked = objectClicked.GetComponent<ItemObject>();
        if (itemClicked)
            itemClicked.Interactuate();
    }
}
