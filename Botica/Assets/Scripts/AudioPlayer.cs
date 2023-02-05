using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    Coroutine currentActiveFade = null;

    AudioSource audioSource;
    [SerializeField] AudioClip intro;
    [SerializeField] AudioClip capa1;
    [SerializeField] AudioClip capa2;
    [SerializeField] AudioClip capa3;

    [SerializeField] AudioClip pociones;
    [SerializeField] AudioClip ouroboros;
    [SerializeField] AudioClip escena2;



    static AudioPlayer instance;

    public bool bchangeTrack;
    float volume = 1.0f;


    void Awake()
    {
        ManageSingleton();

    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (!audioSource.isPlaying)
        {
            if (!bchangeTrack)
            {
                audioSource.clip = capa1;
                audioSource.Play();
                audioSource.loop = true;
            }
        }
    }

    public void changeTrack(string track)
    {
        bchangeTrack = true;
        volume = audioSource.volume;
        StartCoroutine(changeTrackCoroutine(track));
    }

    private IEnumerator changeTrackCoroutine(string track)
    {
        yield return FadeOut(1f);
        if (track == "capa1") audioSource.clip = capa1;
        if (track == "capa2") audioSource.clip = capa2;
        if (track == "capa3") audioSource.clip = capa3;
        if (track == "ouroboros") audioSource.clip = ouroboros;
        if (track == "intro") audioSource.clip = intro;
        if (track == "pociones") audioSource.clip = pociones;
        if (track == "escena2") audioSource.clip = pociones;
        audioSource.Play();
        yield return FadeIn(.5f);
        bchangeTrack = false;
    }

    public void setChangeTrack(bool value)
    {
        bchangeTrack = value;
    }

    public Coroutine FadeOut(float time)
    {
        return Fade(0, time);
    }

    public Coroutine FadeIn(float time)
    {
        return Fade(1, time);
    }

    public Coroutine Fade(float target, float time)
    {
        if (currentActiveFade != null)   // para que no haya un caso en el que las dos corrutinas se activen a la vez al ir y volver r�pido de un nivel
        {
            StopCoroutine(currentActiveFade);
        }
        currentActiveFade = StartCoroutine(FadeRoutine(target, time));
        return currentActiveFade;
    }

    private IEnumerator FadeRoutine(float target, float time)
    {
        while (!Mathf.Approximately(volume, target))
        {
            volume = Mathf.MoveTowards(volume, target, Time.deltaTime / time);  // suma o resta dependiendo del target. As� me ahorro repetici�n de c�digo
            audioSource.volume = volume;
            yield return null;
        }
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}