using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : Gun
{
    public float fireSpread = 100;
    override public void Shoot(Transform transform, float aimX, float aimY)
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3((float)aimX, 0, (float)aimY));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 1000 * Time.deltaTime);

        Quaternion randomSpread = Quaternion.Euler(0, Random.Range(-fireSpread,fireSpread), 0);

        FireBullet(transform, lookRotation * randomSpread, Vector3.zero, 0.7f, true);
    }
}
