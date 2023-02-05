using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    public GameObject interactionButton;
    public GameObject interactionImage;
    public Transform playerRef;
    public Camera cameraScene;
    //public InventoryContorller inventory;
    [SerializeField] Item myItem;
    private bool clickInRange = false;
    private bool playerInRange = false;
    public float verticalOffsetButton = 50.0f;
    public float buttonScaleSize = 1.0f;
    public float imageScaleSize = 1.0f;
    public float distanceToPlayer = 2.5f;

    private void Start()
    {
        interactionButton.SetActive(false);
        interactionImage.SetActive(false);
        Vector3 screenPos = cameraScene.WorldToScreenPoint(gameObject.transform.position);
        interactionButton.transform.position = new Vector3(screenPos.x, screenPos.y + verticalOffsetButton, screenPos.z);
        interactionButton.transform.localScale = new Vector3(buttonScaleSize, buttonScaleSize, buttonScaleSize);
        interactionImage.transform.position = new Vector3(screenPos.x, screenPos.y + verticalOffsetButton, screenPos.z);
        interactionImage.transform.localScale = new Vector3(imageScaleSize, imageScaleSize, imageScaleSize);
        playerRef = GameObject.FindWithTag("Player").gameObject.transform;
        cameraScene = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
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

            if (playerInRange)
            {
                interactionImage.SetActive(false);
                interactionButton.SetActive(true);
            }
            else
            {
                interactionImage.SetActive(true);
                interactionButton.SetActive(false);
            }

        }
        else
        {
            interactionImage.SetActive(false);
            interactionButton.SetActive(false);
        }
        if (Vector3.Distance(transform.position, playerRef.position) < distanceToPlayer)
        {
            playerInRange = true;
        }
        else
        {
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

    public void AddItemToInventory()
    {
        //ANDREA
        //No se si es en InventoryContorller.cs o en Inventory.cs
        gameObject.GetComponent<ItemObject>().Interactuate();

        //Destroy(this.gameObject.transform.root.gameObject, 0.1f);
    }

}