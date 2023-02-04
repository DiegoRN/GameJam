using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class Dialogo : MonoBehaviour
{
    public GameObject panelDialogo;
    string[] dialogo;

    public Text textoDialogo;
    public bool dialogoActivo;

    Coroutine corrutina;
    public bool continuarDialogo = false;
    public FixedTouchField panelSiguiente;

    //public GameObject controlador;
    public GameObject flecha;

    public int numDialogo;


    public bool dejarDeHablar = true;


    // Utilizar json para los di�logos
    public TextAsset jsonDialogoMapa1;
    public TextAsset jsonDialogoMapa2;

    int numEscena;

    [System.Serializable]
    public class Dialogos
    {
        public string Frase1;
        public string Frase2;
        public string Frase3;
        public string Frase4;
        public string Frase5;
        public string Frase6;
        public string Frase7;
    }

    [System.Serializable]
    public class DialogosList
    {
        public Dialogos[] dialogos;
    }

    public DialogosList myDialogosList = new DialogosList();

    void Start()
    {

        //var juegoCargado = Variables.Instance.StringActiveBetweenScenes;

        /*if (numMapa == 0)
        {
            myDialogosList = JsonUtility.FromJson<DialogosList>(jsonDialogoMapa1.text);
        }
        else if (datos.pronombre == 2)
        {*/
            myDialogosList = JsonUtility.FromJson<DialogosList>(jsonDialogoMapa1.text);

       

    }

    public void Hablar()
    {
        // En el caso de que se pida activar el di�logo, y est� activo ya en ese momento,
        // la corrutina se nos cerrar� por los if, y se cancelar� la corrutina activa con CerrarDialogo()
        // Adem�s, se har� una espera para que no se solapen los textos

        if (dialogoActivo)
        {
            if (dejarDeHablar) CerrarDialogo();
            StartCoroutine(EsperarDialogo());
        }
        else
        {
            dialogoActivo = false;
            corrutina = StartCoroutine(MostrarDialogo()); // Lanzar corrutina para pasarle el di�logo que queremos
        }
    }

    IEnumerator MostrarDialogo(float time = 0.03f) // Se le pasa el di�logo que queremos y la velocidad del texto
    {
       // Controlador scriptControlador = controlador.GetComponent<Controlador>();
        panelDialogo.SetActive(true);

        if (dejarDeHablar)
        {

        }

       // scriptControlador.jugador.SetActive(false);

        // DIALOGO SE rellenar� dependiendo del valor de numero
        switch (numDialogo)
        {
            case 0:

                dialogo = new string[]  {JsonConvert.SerializeObject(myDialogosList.dialogos[0].Frase1).Replace("\"", ""),
                                         JsonConvert.SerializeObject(myDialogosList.dialogos[0].Frase2).Replace("\"", "")};
                break;
            case 1:

                dialogo = new string[]  {JsonConvert.SerializeObject(myDialogosList.dialogos[1].Frase1).Replace("\"", ""),
                                         JsonConvert.SerializeObject(myDialogosList.dialogos[1].Frase2).Replace("\"", "")};
                break;

            default:
                Debug.Log("No existe el di�logo.");
                break;

        }

        int numFrases = dialogo.Length; // N�mero de frases que tiene el array
        string res = ""; // Donde se le pasa todo el texto, inicialmente es fac�o
        dialogoActivo = true;
        yield return null; // Espera de confirmaci�n para que el texto no empiece directamente

        // Recorremos las frases recogidas, cada frase se reiniciar�.
        for (int i = 0; i < numFrases; i++)
        {
            res = "";
            // En el caso de que el di�logo est� activo (para no solaparse), se recorre cada car�cter 
            // A res se le sacar� caracter por caracter y se pone el el res del dialogo[i][s]
            // Se pondr� en el texto para imprimirlo
            if (dialogoActivo)
            {

                for (int s = 0; s < dialogo[i].Length; s++)
                {
                    if (dialogoActivo)
                    {
                        res = string.Concat(res, dialogo[i][s]);
                        textoDialogo.text = res;
                        yield return new WaitForSeconds(time); // Esperar el tiempo indicado
                    }
                    else yield break;
                }
                flecha.SetActive(true);
                //yield return new WaitForSeconds(0.4f); // Al terminar la frase entera, se esperar� para darle tiempo
                while (continuarDialogo == false)
                {
                    yield return null;
                }

                flecha.SetActive(false);

                continuarDialogo = false;
            }
            else yield break;
        }

        yield return new WaitForSeconds(0.4f);

        if (numDialogo == 0)
        {
            numDialogo = 1;
            Hablar();
        }
        else if (numDialogo == 1)
        {
            panelDialogo.SetActive(false);
            dejarDeHablar = true;
        }

        if (dejarDeHablar) CerrarDialogo();

    }

    public void cambiarDialogo(int numero)
    {
        numDialogo = numero;
    }

    // Para que no se solapen los textos, se espera un frame para que haya s�lo una llamada y se muestra el di�logo
    IEnumerator EsperarDialogo()
    {
        yield return new WaitForEndOfFrame();
        corrutina = StartCoroutine(MostrarDialogo());
    }

    // Para cerrar el di�logo, cancelaremos la corrutina activa adem�s de vaciar el campo del texto
    // y esconder el panel del di�logo
    public void CerrarDialogo()
    {
        /*Controlador scriptControlador = controlador.GetComponent<Controlador>();

        scriptControlador.jugador.SetActive(true);
        dialogoActivo = false;*/

        if (numEscena == 1)
        {

        }

        if (corrutina != null)
        {
            Debug.Log("Detener");
            StopCoroutine(corrutina);
            corrutina = null;
        }

        textoDialogo.text = "";


    }

    void Update()
    {
        //Controlador scriptControlador = controlador.GetComponent<Controlador>();

        if (Input.GetKey(KeyCode.A)) Hablar();
            // En el caso de que se toque la pantalla y haya aparecido la fecha
        if ((flecha.activeSelf && panelSiguiente.Pressed) || flecha.activeSelf && Input.GetKey(KeyCode.Return))
        {
            continuarDialogo = true;
            //scriptControlador.botonSonido();

        }

    }

}

