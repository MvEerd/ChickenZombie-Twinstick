using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
   public int fireBullets = 3;
   public int fireSpread = 10;
    override public void Shoot(Transform transform, float aimX, float aimY)
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3((float)aimX, 0, (float)aimY));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 1000 * Time.deltaTime);

        Vector3 bulletBuffer = lookRotation * new Vector3(0, 0, 0.5f);

        for(int i = 0; i<fireBullets; i++)
        {
            int maxAngle = fireSpread / 2;
            int minAngle = -(fireSpread / 2);

            float Angle = minAngle + ((maxAngle - minAngle) / fireBullets * i);
            Quaternion shotGunAngle = Quaternion.Euler(0, Angle, 0);

            FireBullet(transform, lookRotation * shotGunAngle, Vector3.zero, 0.8f, i == fireBullets-1);
        }
        


    }
}
