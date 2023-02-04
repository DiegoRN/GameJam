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
            if(Time.timeScale == 0f){
                interactionButton.SetActive(false);
            } else{
                interactionButton.SetActive(true);
            }
        }
    }
    public void ActivateImage()
    {
        interactionButton.SetActive(true);
        playerInRange = true;
    }

    public void DeactivateImage()
    {
        interactionButton.SetActive(false);
        playerInRange = false;
    }
}
