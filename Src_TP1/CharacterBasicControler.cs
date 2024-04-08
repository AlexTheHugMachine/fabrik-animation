using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBasicControler : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float balleSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1.0f;
        rotationSpeed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward_world = transform.TransformDirection(Vector3.forward);
        transform.position += forward_world * speed * Time.deltaTime;

        //Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        //transform.Rotate(-mouseInput.y * rotationSpeed, 0.0f, 0.0f);
        if(Input.GetKey(KeyCode.W))
        {
            if(speed <= 0.0f)
            {
                speed = 0.1f;
            }
            else
            {
                speed *= 1.1f;
            }
        }
        if(Input.GetKey(KeyCode.S))
        {
            speed *= 0.9f;
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -rotationSpeed);
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, rotationSpeed);
        }
    }
}
