using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermediateClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (LookForGameObject(out RaycastHit hit))
            {
                if (hit.collider.gameObject != null)
                    InteractWithObject(hit.collider.gameObject);
            }
        }
    }

    private bool LookForGameObject(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit);
    }

    private void InteractWithObject(GameObject objectClicked)
    {
        ItemObject itemClicked = objectClicked.GetComponent<ItemObject>();
        if (itemClicked)
            itemClicked.Interactuate();
    }
}
