using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update


    [Header("Scene 1 parameters")]
    public GameObject client;
    public Vector3 posicionFinalCliente;
    public float clientSpeed;
    public bool comenzarDialogo;
    public bool finalEscena;
    public GameObject ouroborosImage;
    public float ouroborosSpeed;
    public GameObject fadeIn;

    [Header("Scene Credits parameters")]
    public float creditsSpeed;
    public GameObject creditsText;
    public float finalCreditsTime;
    public float creditsTime;

    public void GoToScene(string SceneName){
        SceneManager.LoadScene(SceneName);
        Time.timeScale = 1f;
    }

    public void LeaveGame()
    {
        Application.Quit();
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
    }


}
