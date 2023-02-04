using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    public GameObject interactionButton;
    public Camera cameraScene;
    private bool playerInRange = false;
    public float verticalOffsetButton = 50.0f;
    public float buttonScaleSize = 1.0f;

    private void Start()
    {
        interactionButton.SetActive(false);
    }
    private void Update()
    {
        if(playerInRange){
            Vector3 screenPos = cameraScene.WorldToScreenPoint(gameObject.transform.position);
            interactionButton.transform.position = new Vector3(screenPos.x, screenPos.y + verticalOffsetButton, screenPos.z);
            interactionButton.transform.localScale = new Vector3(buttonScaleSize, buttonScaleSize, buttonScaleSize);
            if(Input.GetKeyDown(KeyCode.G)){
                //Añadir al inventario
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            interactionButton.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            interactionButton.SetActive(false);
            playerInRange = false;
        }
    }
}
