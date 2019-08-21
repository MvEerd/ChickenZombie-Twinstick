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

        if (timeSinceLastBullet < fireDelay) return;
        timeSinceLastBullet = 0;
        radialGunProgress.fillAmount = 0;

        Vector3 bulletBuffer = lookRotation * new Vector3(0, 0, 0.5f);
        Quaternion randomSpread = Quaternion.Euler(0, Random.Range(-fireSpread,fireSpread), 0);

        GameObject bullet = Instantiate(projectile, transform.position + bulletBuffer + new Vector3(0, 0.2f, 0), lookRotation * randomSpread) as GameObject;
        bullet.transform.localScale = bullet.transform.localScale * 0.7f; //Reduce bullet size

        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * fireForce);
        Destroy(bullet, fireTTL);

    }
}
