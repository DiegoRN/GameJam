using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPoint : MonoBehaviour
{
    public GameObject interactionButton;
    public GameObject interactionImage;
    public Transform playerRef;
    public Transform droppingPoint;
    public Camera cameraScene;
    public bool clickInRange = false;
    public bool playerInRange = false;
    public float verticalOffsetButton = 50.0f;
    public float buttonScaleSize = 1.0f;
    public float imageScaleSize = 1.0f;

    private void Start()
    {
        interactionButton.SetActive(false);
        interactionImage.SetActive(false);
        Vector3 screenPos = cameraScene.WorldToScreenPoint(gameObject.transform.position);
        interactionButton.transform.position = new Vector3(screenPos.x, screenPos.y + verticalOffsetButton, screenPos.z);
        interactionButton.transform.localScale = new Vector3(buttonScaleSize, buttonScaleSize, buttonScaleSize);
        interactionImage.transform.position = new Vector3(screenPos.x, screenPos.y + verticalOffsetButton, screenPos.z);
        interactionImage.transform.localScale = new Vector3(imageScaleSize, imageScaleSize, imageScaleSize);
    }
    private void Update()
    {
        if (clickInRange)
        {
            Vector3 screenPos = cameraScene.WorldToScreenPoint(gameObject.transform.position);
            interactionButton.transform.position = new Vector3(screenPos.x, screenPos.y + verticalOffsetButton, screenPos.z);
            interactionButton.transform.localScale = new Vector3(buttonScaleSize, buttonScaleSize, buttonScaleSize);
            interactionImage.transform.position = new Vector3(screenPos.x, screenPos.y + verticalOffsetButton, screenPos.z);
            interactionImage.transform.localScale = new Vector3(imageScaleSize, imageScaleSize, imageScaleSize);
            

            if (Time.timeScale == 0f)
            {
                interactionImage.SetActive(false);
                interactionButton.SetActive(false);
            }

            if(playerInRange){
                interactionImage.SetActive(false);
                interactionButton.SetActive(true);
            } else{
                interactionImage.SetActive(true);
                interactionButton.SetActive(false);
            }

        } else{
            interactionImage.SetActive(false);
            interactionButton.SetActive(false);
        }
        if(Vector3.Distance(transform.position, playerRef.position) < 2.5f){
            playerInRange = true;
        } else{
            playerInRange = false;
        }

    }
    public void ActivateImage()
    {
        clickInRange = true;
    }

    public void DeactivateImage()
    {
        clickInRange = false;
    }

    public void DropItemFromInventory(){
        //ANDREA
        //Aqui tendria que bloquear el movimiento del personaje y como que hacer highlight del inventario para poner algo
        //O se puede arrastrar el objeto encima y ya? Algo parecido a lo de combinar objetos supongo (?)
        //Y habría que ver donde se realiza la accion de item correcto y tal
    }
    
}