using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTargetMouvement2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            transform.position += Vector3.left;
        if (Input.GetKey(KeyCode.D))
            transform.position += Vector3.right;
        if (Input.GetKey(KeyCode.W))
            transform.position += Vector3.forward;
        if (Input.GetKey(KeyCode.S))
            transform.position += Vector3.back;
        if (Input.GetKey(KeyCode.Z))
            transform.position += Vector3.up;
        if (Input.GetKey(KeyCode.X))
            transform.position += Vector3.down;
    }
}
