using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class Dialogo2 : MonoBehaviour
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


    public bool dejarDeHablar = true;


    // Utilizar json para los diálogos
    public TextAsset jsonDialogoMapa1;

    int numEscena;

    public GameManager gameManager;

    bool primeraVez = true;

    public bool objetoObtenido;
    public GameObject personaje2;

    public GameObject fadeIn;
    public GameObject fadeOut;
    public GameObject fadeInChangeScene;

    public Vector3 posicionFinalPersonaje = new Vector3(-1.60000002f, 0.4833333f, -0.5f);

    public GameObject panelBotones;
    public GameObject panelBotones2;

    public GameObject prefabPlayer;

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

        if (!CheckScene2.Instance.escena2PrimeraVez)
        {
            personaje2.SetActive(true);
            numDialogo = 3;
        }



    }

    public void Hablar()
    {
        // En el caso de que se pida activar el diálogo, y esté activo ya en ese momento,
        // la corrutina se nos cerrará por los if, y se cancelará la corrutina activa con CerrarDialogo()
        // Además, se hará una espera para que no se solapen los textos

        //gameManager.activarMovimiento21 = false;
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

        if (numDialogo == 4) panelBotones.SetActive(false);
        if (numDialogo == 5) panelBotones2.SetActive(false);

        if (player) player.GetComponent<AgentMove>().enabled = false;
        if (prefabPlayer) prefabPlayer.GetComponent<AgentMove>().enabled = false;

    }

    IEnumerator MostrarDialogo(float time = 0.03f) // Se le pasa el diálogo que queremos y la velocidad del texto
    {
        // Controlador scriptControlador = controlador.GetComponent<Controlador>();

        if ((numDialogo % 2 == 0) || (numDialogo == 5))
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

                        if ((numDialogo % 2 == 0) || (numDialogo == 5))
                        {
                            textoDialogo2.text = res;
                        }
                        else textoDialogo.text = res;


                        yield return new WaitForSeconds(time); // Esperar el tiempo indicado
                    }
                    else yield break;
                }

                if ((numDialogo % 2 == 0) || (numDialogo == 5))
                {
                    flecha2.SetActive(true);
                } else flecha.SetActive(true);

                //yield return new WaitForSeconds(0.4f); // Al terminar la frase entera, se esperará para darle tiempo
                while (continuarDialogo == false)
                {
                    yield return null;
                }

                if ((numDialogo % 2 == 0) || (numDialogo == 5))
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
            panelDialogo2.SetActive(false);
            dejarDeHablar = true;
            gameManager.activarMovimiento21 = true;
            gameManager.comenzarDialogo21 = false;
            primeraVez = true;
            numDialogo = 3;
        }
        else if (numDialogo == 3)
        {
            //gameManager.comenzarDialogo = false;
            
            panelDialogo.SetActive(false);
            dejarDeHablar = true;

            if (CheckScene2.Instance.escena2PrimeraVez)
            {
                numDialogo = 4;
                panelBotones.SetActive(true);
            }
            else
            {
                numDialogo = 5;
                panelBotones2.SetActive(true);
            }
            //gameManager.finalEscena = true;
        }
        else if (numDialogo == 4)
        {

            //gameManager.comenzarDialogo = false;
            numDialogo = 5;

            dejarDeHablar = true;

            if (CheckScene2.Instance.escena2PrimeraVez)
            {
                CheckScene2.Instance.escena2PrimeraVez = false;
                panelBotones.SetActive(false);
            }
            else panelBotones2.SetActive(false);

            //fadeInChangeScene.SetActive(true);

            gameManager.finalEscena = true;
        }

        else if (numDialogo == 5)
        {
            //gameManager.comenzarDialogo = false;
            numDialogo = 6;
            panelDialogo2.SetActive(false);
            dejarDeHablar = true;

            if (CheckScene2.Instance.escena2PrimeraVez)
            {
                CheckScene2.Instance.escena2PrimeraVez = false;
                panelBotones.SetActive(false);
            }
            else panelBotones2.SetActive(false);

            //fadeInChangeScene.SetActive(true);

            gameManager.finalEscena = true;
        }

        if (dejarDeHablar) CerrarDialogo();

    }

    public void cambiarDialogo(int numero)
    {
        numDialogo = numero;
    }

    public void EsconderBotones(GameObject panel)
    {
        panel.SetActive(false);
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

        if (player) player.GetComponent<AgentMove>().enabled = true;
        panelDialogo.SetActive(false);
        panelDialogo2.SetActive(false);


    }

    IEnumerator FadeInFadeOut()
    {
        gameManager.activarMovimiento21 = false;
        fadeIn.SetActive(true);
        yield return new WaitForSeconds(fadeIn.GetComponent<SceneFader>().fadeSpeed);
        fadeIn.SetActive(false);
        personaje2.SetActive(true);
        //player.transform.position = posicionFinalPersonaje;
        //instantiate(player, player.transform.position, player.transform.rotation);
        Destroy(player);
        Instantiate(prefabPlayer, posicionFinalPersonaje, Quaternion.identity);
        prefabPlayer.GetComponent<AgentMove>().enabled = false;
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(fadeOut.GetComponent<SceneFader>().fadeSpeed);
        //fadeOut.SetActive(false);
        gameManager.comenzarDialogo21 = true;
        primeraVez = true;

    }

    void Update()
    {
        //Controlador scriptControlador = controlador.GetComponent<Controlador>();

        
        

        if (gameManager.comenzarDialogo21)
        {

            if (primeraVez)
            {
                Hablar();

                //continuarDialogo = true;
                primeraVez = false;
            }

        }

        if (objetoObtenido)
        {
            objetoObtenido = false;
            // Fundido en negro y aparición de personaje
            StartCoroutine(FadeInFadeOut());
        }


        // if (Input.GetKey(KeyCode.Return)) Hablar();

        // En el caso de que se toque la pantalla y haya aparecido la fecha
        if ((numDialogo % 2 == 0) || (numDialogo == 5))
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

