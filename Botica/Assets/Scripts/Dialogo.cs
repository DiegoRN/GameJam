using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class Dialogo : MonoBehaviour
{
    public GameObject panelDialogo;
    public GameObject panelDialogo2;
    string[] dialogo;

    public Text textoDialogo;
    public Text textoDialogo2;
    public bool dialogoActivo;

    Coroutine corrutina;
    public bool continuarDialogo = false;
    public FixedTouchField panelSiguiente;

    //public GameObject controlador;
    public GameObject flecha;
    public GameObject flecha2;

    public int numDialogo;

    public GameObject player;
    public GameObject canvas;


    public bool dejarDeHablar = true;


    // Utilizar json para los diálogos
    public TextAsset jsonDialogoMapa1;
    public TextAsset jsonDialogoMapa2;

    int numEscena;

    public GameManager gameManager;

    bool primeraVez = true;

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
        // En el caso de que se pida activar el diálogo, y esté activo ya en ese momento,
        // la corrutina se nos cerrará por los if, y se cancelará la corrutina activa con CerrarDialogo()
        // Además, se hará una espera para que no se solapen los textos

        if (dialogoActivo)
        {
            if (dejarDeHablar) CerrarDialogo();
            StartCoroutine(EsperarDialogo());
        }
        else
        {
            dialogoActivo = false;
            corrutina = StartCoroutine(MostrarDialogo()); // Lanzar corrutina para pasarle el diálogo que queremos
        }
        player.GetComponent<AgentMove>().enabled = false;
    }

    IEnumerator MostrarDialogo(float time = 0.03f) // Se le pasa el diálogo que queremos y la velocidad del texto
    {
       // Controlador scriptControlador = controlador.GetComponent<Controlador>();

        if (numDialogo%2 == 0)
        {
            panelDialogo2.SetActive(true);
        }
        else panelDialogo.SetActive(true);


        if (dejarDeHablar)
        {

        }

       // scriptControlador.jugador.SetActive(false);

        // DIALOGO SE rellenará dependiendo del valor de numero
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
            case 2:

                dialogo = new string[]  {JsonConvert.SerializeObject(myDialogosList.dialogos[2].Frase1).Replace("\"", ""),
                                         JsonConvert.SerializeObject(myDialogosList.dialogos[2].Frase2).Replace("\"", "")};
                break;
            case 3:
                dialogo = new string[]  {JsonConvert.SerializeObject(myDialogosList.dialogos[3].Frase1).Replace("\"", ""),
                                         JsonConvert.SerializeObject(myDialogosList.dialogos[3].Frase2).Replace("\"", "")};
                break;
            default:
                Debug.Log("No existe el diálogo.");
                break;

        }

        int numFrases = dialogo.Length; // Número de frases que tiene el array
        string res = ""; // Donde se le pasa todo el texto, inicialmente es facío
        dialogoActivo = true;
        yield return null; // Espera de confirmación para que el texto no empiece directamente

        // Recorremos las frases recogidas, cada frase se reiniciará.
        for (int i = 0; i < numFrases; i++)
        {
            res = "";
            // En el caso de que el diálogo esté activo (para no solaparse), se recorre cada carácter 
            // A res se le sacará caracter por caracter y se pone el el res del dialogo[i][s]
            // Se pondrá en el texto para imprimirlo
            if (dialogoActivo)
            {

                for (int s = 0; s < dialogo[i].Length; s++)
                {
                    if (dialogoActivo)
                    {
                        res = string.Concat(res, dialogo[i][s]);

                        if (numDialogo % 2 == 0)
                        {
                            textoDialogo2.text = res;
                        }
                        else textoDialogo.text = res;


                        yield return new WaitForSeconds(time); // Esperar el tiempo indicado
                    }
                    else yield break;
                }

                if (numDialogo % 2 == 0)
                {
                    flecha2.SetActive(true);
                } else flecha.SetActive(true);

                //yield return new WaitForSeconds(0.4f); // Al terminar la frase entera, se esperará para darle tiempo
                while (continuarDialogo == false)
                {
                    yield return null;
                }

                if (numDialogo % 2 == 0)
                {
                    flecha2.SetActive(false);
                }
                else flecha.SetActive(false);

                continuarDialogo = false;
            }
            else yield break;
        }

        yield return new WaitForSeconds(0.4f);

        if (numDialogo == 0)
        {
            numDialogo = 1;
            panelDialogo2.SetActive(false);
            Hablar();
        }
        else if (numDialogo == 1)
        {
            //gameManager.comenzarDialogo = false;
            numDialogo = 2;
            panelDialogo.SetActive(false);
            Hablar();
        }
        else if (numDialogo == 2)
        {
            //gameManager.comenzarDialogo = false;
            numDialogo = 3;
            panelDialogo2.SetActive(false);
            Hablar();
        }
        else if (numDialogo == 3)
        {
            //gameManager.comenzarDialogo = false;
            panelDialogo.SetActive(false);
            dejarDeHablar = true;

            player.GetComponent<AgentMove>().enabled = true;
            canvas.SetActive(false);
            //gameManager.finalEscena = true;
        }

        if (dejarDeHablar) CerrarDialogo();

    }

    public void cambiarDialogo(int numero)
    {
        numDialogo = numero;
    }

    // Para que no se solapen los textos, se espera un frame para que haya sólo una llamada y se muestra el diálogo
    IEnumerator EsperarDialogo()
    {
        yield return new WaitForEndOfFrame();
        corrutina = StartCoroutine(MostrarDialogo());
    }

    // Para cerrar el diálogo, cancelaremos la corrutina activa además de vaciar el campo del texto
    // y esconder el panel del diálogo
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

        player.GetComponent<AgentMove>().enabled = true;
        panelDialogo.SetActive(false);


    }

    void Update()
    {
        //Controlador scriptControlador = controlador.GetComponent<Controlador>();
        
        if (gameManager.comenzarDialogo)
        {

            if (primeraVez)
            {
                Hablar();

                //continuarDialogo = true;
                primeraVez = false;
            }

        }


        // if (Input.GetKey(KeyCode.Return)) Hablar();

        // En el caso de que se toque la pantalla y haya aparecido la fecha
        if (numDialogo % 2 == 0)
        {
            if ((flecha2.activeSelf && panelSiguiente.Pressed) || flecha2.activeSelf && Input.GetKey(KeyCode.Return))
            {
                continuarDialogo = true;
                //scriptControlador.botonSonido();

            }
        }
        else if ((flecha.activeSelf && panelSiguiente.Pressed) || flecha.activeSelf && Input.GetKey(KeyCode.Return))
        {
            continuarDialogo = true;
            //scriptControlador.botonSonido();

        }



    }

}

