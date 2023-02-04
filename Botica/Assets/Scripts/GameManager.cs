using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject client;
    public Vector3 posicionFinalCliente;
    public float speed;
    public bool comenzarDialogo;

    public void GoToScene(string SceneName){
        SceneManager.LoadScene(SceneName);
    }

    public void LeaveGame()
    {
        Application.Quit();
    }


    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Scene1")
        {
            client.transform.position = Vector3.MoveTowards(client.transform.position, posicionFinalCliente, speed * Time.deltaTime);
        }

        if (client.transform.position == posicionFinalCliente) comenzarDialogo = true;
    }


}
