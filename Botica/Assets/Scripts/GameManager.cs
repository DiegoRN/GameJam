using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void GoToScene(string SceneName){
        SceneManager.LoadScene(SceneName);
    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}
