using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    public GameObject interactionButton;
    public Camera cameraScene;
    private void Start()
    {
        interactionButton.SetActive(false);
    }
    private void Update()
    {
        Vector3 screenPos = cameraScene.WorldToScreenPoint(gameObject.transform.position);
        interactionButton.transform.position = screenPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            interactionButton.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            interactionButton.SetActive(false);

        }
    }
}
