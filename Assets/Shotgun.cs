using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    public new static float fireDelay = 0.8f;
    override public void Shoot(Transform transform, float aimX, float aimY)
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3((float)aimX, 0, (float)aimY));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 1000 * Time.deltaTime);

        if (PlayerControl.timeSinceLastBullet < fireDelay) return;
        PlayerControl.timeSinceLastBullet = 0;

        Vector3 bulletBuffer = lookRotation * new Vector3(0, 0, 0.5f);

        List<GameObject> bullets = new List<GameObject>();

        GameObject bullet = Instantiate(projectile, transform.position + bulletBuffer + new Vector3(0, 0.2f, 0), lookRotation);
        bullets.Add(bullet);

        GameObject bullet2 = Instantiate(projectile, transform.position + bulletBuffer + new Vector3(0, 0.2f, 0), lookRotation * Quaternion.Euler(0, 5, 0));
        bullets.Add(bullet2);
    
        GameObject bullet3 = Instantiate(projectile, transform.position + bulletBuffer + new Vector3(0, 0.2f, 0), lookRotation * Quaternion.Euler(0, -5, 0));
        bullets.Add(bullet3);
        

        bullets.ForEach(b => {
            b.GetComponent<Rigidbody>().AddForce(b.transform.forward * fireForce);
            Destroy(b, fireTTL);
        });

    }
}
