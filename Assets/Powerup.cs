using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public Gun powerupGun;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<PlayerControl>()) return;
        other.gameObject.GetComponent<PlayerControl>().currentGun = powerupGun;
    }
}
