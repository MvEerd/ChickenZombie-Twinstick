using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
   public int fireBullets = 3;
   public int fireSpread = 10;
   // new private float fireDelay = 0.8f;
    override public void Shoot(Transform transform, float aimX, float aimY)
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3((float)aimX, 0, (float)aimY));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 1000 * Time.deltaTime);

        if (PlayerControl.timeSinceLastBullet < fireDelay) return;
        PlayerControl.timeSinceLastBullet = 0;

        Vector3 bulletBuffer = lookRotation * new Vector3(0, 0, 0.5f);

        List<GameObject> bullets = new List<GameObject>();

        for(int i = 0; i<fireBullets; i++)
        {
            int maxAngle = fireSpread / 2;
            int minAngle = -(fireSpread / 2);

            float Angle = minAngle + ((maxAngle - minAngle) / fireBullets * i);

            GameObject bullet = Instantiate(projectile, transform.position + bulletBuffer + new Vector3(0, 0.2f, 0), lookRotation * Quaternion.Euler(0, Angle, 0));
            bullet.transform.localScale = bullet.transform.localScale * 0.8f;
            bullets.Add(bullet);
        }
        

        bullets.ForEach(b => {
            b.GetComponent<Rigidbody>().AddForce(b.transform.forward * fireForce);
            Destroy(b, fireTTL);
        });

    }
}
