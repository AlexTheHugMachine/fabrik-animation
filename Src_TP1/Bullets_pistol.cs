using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets_pistol : MonoBehaviour
{
    public float balleSpeed = 10.0f;
    public GameObject BallePrefab;
    Vector3 decal_Balle = new Vector3(0.0f, 0.0f, -0.4f);
    Quaternion decal_Balle2 = Quaternion.Euler(90.0f, 0.0f, 0.0f);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject balle = Instantiate(BallePrefab, transform.position + transform.TransformDirection(Vector3.forward) + decal_Balle, transform.rotation * decal_Balle2);
            Rigidbody rb = balle.GetComponent<Rigidbody>();
            rb.AddForce(transform.TransformDirection(Vector3.forward) * balleSpeed, ForceMode.Impulse);
        }*/
    }
}
