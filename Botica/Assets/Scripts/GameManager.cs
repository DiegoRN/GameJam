using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    //Creamoss Singleton
    public static GameManager Instance { get; private set; }


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
    public bool comenzarDialogo23;
    public bool activarMovimiento21 = false;
    public GameObject player;

    [Header("Scene Credits parameters")]
    public float creditsSpeed;
    public GameObject creditsText;
    public float finalCreditsTime;
    public float creditsTime;

    [Header("Recipebook")]
    public List<ItemCombined> Recipebook;

    public void GoToScene(string SceneName){
        SceneManager.LoadScene(SceneName);
        Time.timeScale = 1f;
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

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

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Scene1")
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

        if (activarMovimiento21)
        {
            player.GetComponent<AgentMove>().enabled = true;
        }
    }

    public Item CombineItems(Item item1, Item item2)
    {
        foreach (ItemCombined combined in Recipebook)
        {
            if (item1 == combined.item1) {
                if (item2 == combined.item2)
                {
                    return combined.itemResult;
                }
            }
            if (item2 == combined.item1) {
                if (item1 == combined.item2)
                {
                    return combined.itemResult;
                }
            }
        }
        return null;
    }




}
