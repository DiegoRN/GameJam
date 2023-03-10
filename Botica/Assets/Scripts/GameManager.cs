using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    //Creamoss Singleton
    //public static GameManager Instance { get; private set; }


    [Header("Scene 1 parameters")]
    public GameObject client;
    public Vector3 posicionFinalCliente;
    public float clientSpeed;
    public bool comenzarDialogo;
    public bool finalEscena;
    public GameObject ouroborosImage;
    public float ouroborosSpeed;
    public GameObject fadeIn;

    [Header("Scene 2 parameters")]
    public int numVecesEscena2 = 1;
    public bool primeraVezHablaEscena21 = true;
    public bool primeraVezHablaEscena22 = true;
    public bool primeraVezHablaEscena23 = true;
    public bool comenzarDialogo21;
    public bool comenzarDialogo22;
    public bool activarMovimiento21 = false;
    public GameObject player;

    [Header("Scene Credits parameters")]
    public float creditsSpeed;
    public GameObject creditsText;
    public float finalCreditsTime;
    public float creditsTime;

    [Header("Items")]
    public Item racies;
    public Item candelabroencendido;

    public void GoToScene(string SceneName){
        SceneManager.LoadScene(SceneName);
        Time.timeScale = 1f;
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

/*
    void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    */

    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
    }

    void CheckItems()
    {
        if (racies) finalEscena = InventoryContorller.Instance.InventoryHasItem(racies);

        Dialogo2 d2 = FindObjectOfType<Dialogo2>();
        if (d2 != null && candelabroencendido)
        {
            d2.objetoObtenido = InventoryContorller.Instance.InventoryHasItem(candelabroencendido);
        }
    }

    void Update()
    {
        CheckItems();
        if (SceneManager.GetActiveScene().name == "Scene1copia")
        {
            client.transform.position = Vector3.MoveTowards(client.transform.position, posicionFinalCliente, clientSpeed * Time.deltaTime);
            if (client.transform.position == posicionFinalCliente) comenzarDialogo = true;

            if (finalEscena)
            {
                ouroborosImage.SetActive(true);
                ouroborosImage.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * ouroborosSpeed, Space.World);
                if (ouroborosSpeed < 1000)
                {
                    ouroborosSpeed += Time.deltaTime * 250f;
                }
                else
                {
                    InventoryContorller.Instance.DeleteItem(racies);
                    fadeIn.SetActive(true);
                    FindObjectOfType<AudioPlayer>().changeTrack("escena2");
                }
            }

        }

        if ((SceneManager.GetActiveScene().name == "Scene3") || (SceneManager.GetActiveScene().name == "Scene2"))
        {
            if (finalEscena)
            {
                ouroborosImage.SetActive(true);
                ouroborosImage.transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * ouroborosSpeed, Space.World);
                if (ouroborosSpeed < 1000)
                {
                    ouroborosSpeed += Time.deltaTime * 250f;
                }
                else
                {
                    fadeIn.SetActive(true);
                }
            }

        }

        if (SceneManager.GetActiveScene().name == "Credits")
        {
            creditsText.transform.Translate(Vector3.up * Time.deltaTime * creditsSpeed, Space.World);
            creditsTime += Time.deltaTime;

            if (creditsTime >= finalCreditsTime)
            {
                fadeIn.SetActive(true);
            }

        }


        // Primera vez que se viaja a la escena 2
        if (numVecesEscena2 == 1)
        {
            //if (primeraVezHablaEscena21) ;
        }

        if (player)
        {
            if (activarMovimiento21)
            {
                player.GetComponent<AgentMove>().enabled = true;
            }
            else
            {
                player.GetComponent<AgentMove>().enabled = false;
            }
        }
        
    }
}
