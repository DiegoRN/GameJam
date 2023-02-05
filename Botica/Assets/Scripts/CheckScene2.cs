using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckScene2 : MonoBehaviour
{

    public static CheckScene2 Instance { get; private set; }

    public bool escena2PrimeraVez = true;

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
}
