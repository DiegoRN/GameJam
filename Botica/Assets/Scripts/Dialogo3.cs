using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class Dialogo3 : MonoBehaviour
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

    public GameObject player;

    public bool dejarDeHablar = true;


    // Utilizar json para los diálogos
    public TextAsset jsonDialogoMapa1;

    int numEscena;

    public GameManager gameManager;

    bool primeraVez = true;

    public GameObject fadeIn;
    public GameObject fadeOut;
    public GameObject fadeInChangeScene;


    // Escena3
    public bool haInteracctuadoConOllin;
    public bool haInteracctuadoConBaul;
    public bool haInteracctuadoConEstanteria;
    public bool interaccionConBaul;
    public bool llaveUsada;

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
            myDialogosList = JsonUtility.FromJson<DialogosList>(jsonDialogoMapa1.text);
            player.GetComponent<AgentMove>().enabled = true;

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

        panelDialogo.SetActive(true);


        if (dejarDeHablar)
        {

        }

        // DIALOGO SE rellenará dependiendo del valor de numero
        switch (numDialogo)
        {
            case 0:

                dialogo = new string[]  {JsonConvert.SerializeObject(myDialogosList.dialogos[0].Frase1).Replace("\"", "")};
                break;
            case 1:

                dialogo = new string[]  {JsonConvert.SerializeObject(myDialogosList.dialogos[1].Frase1).Replace("\"", "")};
                break;
            case 2:

                dialogo = new string[]  {JsonConvert.SerializeObject(myDialogosList.dialogos[2].Frase1).Replace("\"", "")};
                break;
            case 3:
                dialogo = new string[]  {JsonConvert.SerializeObject(myDialogosList.dialogos[3].Frase1).Replace("\"", "")};
                break;
            case 4:
                dialogo = new string[]  {JsonConvert.SerializeObject(myDialogosList.dialogos[4].Frase1).Replace("\"", "")};
                break;
            case 5:
                dialogo = new string[] { JsonConvert.SerializeObject(myDialogosList.dialogos[5].Frase1).Replace("\"", "") };
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

                        textoDialogo.text = res;


                        yield return new WaitForSeconds(time); // Esperar el tiempo indicado
                    }
                    else yield break;
                }

                flecha.SetActive(true);

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
            panelDialogo.SetActive(false);
        }
        else if (numDialogo == 1)
        {
            panelDialogo.SetActive(false);
        }
        else if (numDialogo == 2)
        {
            panelDialogo.SetActive(false);
        }
        else if (numDialogo == 3)
        {
            panelDialogo.SetActive(false);
        }
        else if (numDialogo == 4)
        {

            numDialogo = 5;
            panelDialogo.SetActive(false);
            dejarDeHablar = true;

            gameManager.finalEscena = true;

            //fadeInChangeScene.SetActive(true);
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
        
        /*
        if (gameManager.comenzarDialogo3)
        {

            if (primeraVez)
            {
                Hablar();

                //continuarDialogo = true;
                primeraVez = false;
            }

        }
        */

        if (haInteracctuadoConOllin)
        {
            numDialogo = 0;
            haInteracctuadoConOllin = false;
            Hablar();
        }

        if (haInteracctuadoConBaul)
        {
            numDialogo = 1;
            haInteracctuadoConBaul = false;
            interaccionConBaul = true;
            Hablar();
        }

        if (haInteracctuadoConEstanteria)
        {
            if (!interaccionConBaul) numDialogo = 2;
            else numDialogo = 3;
            haInteracctuadoConEstanteria = false;
            Hablar();
        }

        if (llaveUsada)
        {
            llaveUsada = false;
            numDialogo = 4;
            Hablar();
        }

        

        // En el caso de que se toque la pantalla y haya aparecido la fecha
        if ((flecha.activeSelf && panelSiguiente.Pressed) || flecha.activeSelf && Input.GetKey(KeyCode.Return))
        {
            continuarDialogo = true;

        }

    }

}

