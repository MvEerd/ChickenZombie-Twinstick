using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    public GameObject target;
    float mFollowRate = 4f;
    float mFollowHeight = 5f;


    void FixedUpdate()
    {
        if (!target) return;
        //Follow target GameObject from above using a top-down camera
        transform.position = Vector3.Lerp(transform.position, target.transform.position + new Vector3(0f, (mFollowHeight - target.transform.position.y), -5f), Time.fixedDeltaTime * 2);

        //Quaternion targetLookRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetLookRotation, Time.fixedDeltaTime * mFollowRate*2);
    }
}
