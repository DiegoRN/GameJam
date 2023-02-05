using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class Dialogo11 : MonoBehaviour
{
    public GameObject panelDialogo;
    string[] dialogo;

    public bool dialogoActivo;

    public Text textoDialogo; 
    public GameObject personaje2;

    Coroutine corrutina;
    public bool continuarDialogo = false;
    public FixedTouchField panelSiguiente;

    //public GameObject controlador;
    public GameObject flecha;

    public int numDialogo;

    public GameObject player;


    public bool dejarDeHablar = true;


    // Utilizar json para los di�logos
    public TextAsset jsonDialogoMapa1;

    int numEscena;

    public GameManager gameManager;

    bool primeraVez = true;

    public bool objetoObtenido;

    public GameObject fadeIn;
    public GameObject fadeOut;
    public GameObject fadeInChangeScene;

    public Vector3 posicionFinalPersonaje = new Vector3(-1.60000002f, 0.4833333f, -0.5f);

    public GameObject panelBotones;
    public GameObject panelBotones2;

    public GameObject prefabPlayer;

    public bool haInteracctuadoConMesa;

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

        //gameManager.activarMovimiento21 = false;
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

        if (player) player.GetComponent<AgentMove>().enabled = false;
        if (prefabPlayer) prefabPlayer.GetComponent<AgentMove>().enabled = false;
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

                dialogo = new string[]  {JsonConvert.SerializeObject(myDialogosList.dialogos[0].Frase1).Replace("\"", "")};
                break;
            case 1:

                dialogo = new string[]  {JsonConvert.SerializeObject(myDialogosList.dialogos[1].Frase1).Replace("\"", "")};
                break;
            case 2:

                dialogo = new string[] { JsonConvert.SerializeObject(myDialogosList.dialogos[2].Frase1).Replace("\"", "") };
                break;
            case 3:

                dialogo = new string[] { JsonConvert.SerializeObject(myDialogosList.dialogos[3].Frase1).Replace("\"", "") };
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
            panelDialogo.SetActive(false);
            dejarDeHablar = true;
            gameManager.activarMovimiento21 = true;
            gameManager.comenzarDialogo21 = false;
            primeraVez = true;
            numDialogo = 1;
        }
        else if (numDialogo == 1)
        {
            numDialogo = 2;
            gameManager.activarMovimiento21 = false;

            panelDialogo.SetActive(false);
            dejarDeHablar = true;

            
            panelBotones.SetActive(true);
        }
        else if (numDialogo == 2)
        {
            numDialogo = 5;

            dejarDeHablar = true;

            panelBotones.SetActive(false);

            //fadeInChangeScene.SetActive(true);

            gameManager.finalEscena = true;
        }
        else if (numDialogo == 3)
        {
            numDialogo = 5;

            dejarDeHablar = true;

            panelBotones.SetActive(false);

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

    public void AparecePersonaje()
    {
        StartCoroutine(FadeInFadeOut());
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

        if (player) player.GetComponent<AgentMove>().enabled = true;
        if (prefabPlayer) prefabPlayer.GetComponent<AgentMove>().enabled = true;
        panelDialogo.SetActive(false);

        //gameManager.activarMovimiento21 = true;

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
        gameManager.activarMovimiento21 = false;
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(fadeOut.GetComponent<SceneFader>().fadeSpeed);
        //fadeOut.SetActive(false);
        gameManager.comenzarDialogo21 = true;
        primeraVez = true;
        Hablar();

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

        if (haInteracctuadoConMesa)
        {
            numDialogo = 1;
            haInteracctuadoConMesa = false;
            player.GetComponent<AgentMove>().enabled = false;
            gameManager.activarMovimiento21 = false;
            Hablar();
        }


        // if (Input.GetKey(KeyCode.Return)) Hablar();

        // En el caso de que se toque la pantalla y haya aparecido la fecha
        if ((flecha.activeSelf && panelSiguiente.Pressed) || flecha.activeSelf && Input.GetKey(KeyCode.Return))
        {
            continuarDialogo = true;
            //scriptControlador.botonSonido();

        }



    }

}

