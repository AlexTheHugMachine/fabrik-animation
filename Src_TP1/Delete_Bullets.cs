using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete_Bullets : MonoBehaviour
{
    public float balleLifeTime = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, balleLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
