using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public Gun powerupGun;
    public string gunString;

    private void Start()
    {
        powerupGun = GameObject.Find(gunString).GetComponent<Gun>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<PlayerControl>()) return;
        other.gameObject.GetComponent<PlayerControl>().currentGun = powerupGun;

        Destroy(gameObject);
    }
}
