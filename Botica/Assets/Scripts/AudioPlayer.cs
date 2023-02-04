using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    Coroutine currentActiveFade = null;

    AudioSource audioSource;
    [SerializeField] AudioClip capa1;
    [SerializeField] AudioClip capa2;
    [SerializeField] AudioClip capa3;
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

    public void changeTrack()
    {
        bchangeTrack = true;
        volume = audioSource.volume;
        StartCoroutine(changeTrackCoroutine());
    }

    private IEnumerator changeTrackCoroutine()
    {
        yield return FadeOut(2f);
        audioSource.clip = capa3;
        audioSource.Play();
        yield return FadeIn(1.5f);
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
        if (currentActiveFade != null)   // para que no haya un caso en el que las dos corrutinas se activen a la vez al ir y volver rápido de un nivel
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
            volume = Mathf.MoveTowards(volume, target, Time.deltaTime / time);  // suma o resta dependiendo del target. Así me ahorro repetición de código
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