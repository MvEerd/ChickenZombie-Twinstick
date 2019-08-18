using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    public GameObject target;
    float mFollowRate = 4f;
    float mFollowHeight = 5f;


    void LateUpdate()
    {
        if (!target) return;
        //Follow target GameObject from above using a top-down camera
        transform.position = Vector3.Lerp(transform.position, target.transform.position + new Vector3(0f, (mFollowHeight - target.transform.position.y), -5f), Time.deltaTime * mFollowRate);
        //transform.position = target.transform.position + new Vector3(0f, (mFollowHeight - target.transform.position.y), -5f);


        Quaternion targetLookRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetLookRotation, Time.deltaTime * 1000);

        transform.LookAt(target.transform);
    }
}
