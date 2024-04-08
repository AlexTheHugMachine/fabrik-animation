using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Controller_Script : MonoBehaviour
{
    private Animator myAnimator;

    [Range (0, 1f)]
    public float DistanceToGround;

    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start () {
        myAnimator = GetComponent<Animator>();
        Debug.Log("MyAniConScript: start => Animator");
}

    // Update is called once per frame
    void Update () {
        myAnimator.SetFloat("VSpeed", Input.GetAxis("Vertical"));
        myAnimator.SetFloat("HSpeed", Input.GetAxis("Horizontal"));
        Debug.Log("VSpeed = " + Input.GetAxis("Vertical"));
        Debug.Log("HSpeed = " + Input.GetAxis("Horizontal"));

        if(Input.GetKeyDown(KeyCode.C))
        {
            myAnimator.SetBool("Crouch", true);
        }
        else
        {
            myAnimator.SetBool("Crouch", false);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            myAnimator.SetTrigger("Jump");
        }
    }

    void OnAnimatorIK(int layerIndex) {
        if(myAnimator) {
            myAnimator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            myAnimator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);

            RaycastHit hit;
            Ray ray = new Ray(myAnimator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
            if(Physics.Raycast(ray, out hit, DistanceToGround + 1f, layerMask))
            {
                Vector3 footPosition = hit.point;
                footPosition.y += DistanceToGround;
                myAnimator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                myAnimator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
            }

            ray = new Ray(myAnimator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
            if(Physics.Raycast(ray, out hit, DistanceToGround + 1f, layerMask))
            {
                Vector3 footPosition = hit.point;
                footPosition.y += DistanceToGround;
                myAnimator.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                myAnimator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));
            }
        }
    }
}
