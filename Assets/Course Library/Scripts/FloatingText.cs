using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float destroyTime = 1f;
    public Vector3 Offset = new Vector3(0, 0, 0);
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);

        transform.localPosition += Offset;
    }
    
}
