using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public GameObject projectile;
    public Image radialGunProgress;

    public float fireDelay = 0.2f;
    public float fireForce = 70;
    public float fireTTL = 2;
    public float fireDamage = 100;
    protected float timeLeft;
    protected float timeSinceLastBullet = 0f;

    public void cdUpdate()
    {
        timeSinceLastBullet += Time.deltaTime;
        if (radialGunProgress.fillAmount < 1)
        {
            timeLeft = fireDelay - timeSinceLastBullet;
            timeLeft = timeLeft < 0 ? 0 : timeLeft;
            radialGunProgress.fillAmount = 1- (timeLeft / fireDelay);
        }
    }

    virtual public void Shoot(Transform transform, float aimX, float aimY)
    {

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3((float)aimX, 0, (float)aimY));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 1000 * Time.deltaTime);

        if (timeSinceLastBullet < fireDelay) return;
        timeSinceLastBullet = 0;
        radialGunProgress.fillAmount = 0;

    Vector3 bulletBuffer = lookRotation * new Vector3(0, 0, 0.5f);

        GameObject bullet = Instantiate(projectile, transform.position + bulletBuffer + new Vector3(0, 0.2f, 0), lookRotation) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * fireForce);
        Destroy(bullet, fireTTL);
    }

}
