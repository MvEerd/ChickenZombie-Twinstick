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

    virtual public void UpdateGun()
    {
        timeSinceLastBullet += Time.deltaTime;
        if (radialGunProgress.fillAmount < 1)
        {
            timeLeft = fireDelay - timeSinceLastBullet;
            timeLeft = timeLeft < 0 ? 0 : timeLeft;
            radialGunProgress.fillAmount = 1- (timeLeft / fireDelay);
        }
    }

    virtual public void IdleGun()
    {

    }

    virtual public void FireBullet(Transform playerTransform, Quaternion bulletRotation, Vector3 positionOffset, float scale, bool coolDown)
    {
        if ((timeSinceLastBullet < fireDelay)) return;
        if (coolDown)
        {
            timeSinceLastBullet = 0;
            radialGunProgress.fillAmount = 0;
        }

        Vector3 bulletBuffer = bulletRotation * (new Vector3(0, 0, 0.5f) + positionOffset);

        GameObject bullet = Instantiate(projectile, playerTransform.position + bulletBuffer + new Vector3(0, 0.2f, 0), bulletRotation) as GameObject;
        bullet.transform.localScale = bullet.transform.localScale * scale;

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.damage = fireDamage;

        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * fireForce);
        Destroy(bullet, fireTTL);
    }

    virtual public void Shoot(Transform playerTransform, float aimX, float aimY)
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3((float)aimX, 0, (float)aimY));
        playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, lookRotation, 1000 * Time.deltaTime);

        FireBullet(playerTransform, lookRotation, Vector3.zero, 1, true);
    }

}
