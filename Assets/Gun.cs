using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject projectile;

    public float fireDelay = 0.2f;
    public float fireForce = 70;
    public float fireTTL = 2;
    public float fireDamage = 100;

    virtual public void Shoot(Transform transform, float aimX, float aimY)
    {

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3((float)aimX, 0, (float)aimY));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 1000 * Time.deltaTime);

        if (PlayerControl.timeSinceLastBullet < fireDelay) return;
        PlayerControl.timeSinceLastBullet = 0;

        Vector3 bulletBuffer = lookRotation * new Vector3(0, 0, 0.5f);

        GameObject bullet = Instantiate(projectile, transform.position + bulletBuffer + new Vector3(0, 0.2f, 0), lookRotation) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * fireForce);
        Destroy(bullet, fireTTL);
    }

}
