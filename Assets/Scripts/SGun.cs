using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SGun : Gun
{
    public float maxAngle = 20;
    public float maxOffset = 0.5f;
    public float sinSpeed = 10;

    override public void Shoot(Transform playerTransform, float aimX, float aimY)
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3((float)aimX, 0, (float)aimY));
        playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, lookRotation, 1000 * Time.deltaTime);
        
        Quaternion sinRotation = Quaternion.Euler(0, maxAngle * Mathf.Sin(Time.fixedTime * sinSpeed), 0);
        Quaternion negSinRotation = Quaternion.Euler(0, -(maxAngle * Mathf.Sin(Time.fixedTime * sinSpeed)), 0);
        Vector3 sinOffset = new Vector3(maxOffset * Mathf.Sin(Time.fixedTime * sinSpeed), 0, 0);

        FireBullet(playerTransform, lookRotation, sinOffset, 0.4f, false);
        FireBullet(playerTransform, lookRotation, -sinOffset, 0.4f, true);


    }

}
