using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LectorJSON : MonoBehaviour
{
    public TextAsset json;

    [System.Serializable]
    public class Instrucciones
    {
        public string Frase1;
        public string Frase2;
        public string Frase3;
        public string Frase4;
        public string Frase5;
    }

    [System.Serializable]
    public class InstruccionesList
    {
        public Instrucciones[] instrucciones;
    }

    public InstruccionesList myInstruccionesList = new InstruccionesList();
    // Start is called before the first frame update
    void Start()
    {
        myInstruccionesList = JsonUtility.FromJson<InstruccionesList>(json.text);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
