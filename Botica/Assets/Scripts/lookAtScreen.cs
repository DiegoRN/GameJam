using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtScreen : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (gameObject) gameObject.transform.parent.transform.rotation = Quaternion.identity;
    }
}
