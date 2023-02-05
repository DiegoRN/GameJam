using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotator : MonoBehaviour
{
    [SerializeField]
    float rotationRate = 60;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update() {
        transform.Rotate (Vector3.up,  Time.deltaTime * rotationRate);
        transform.Rotate (Vector3.forward,  Time.deltaTime * rotationRate);
    }

}
