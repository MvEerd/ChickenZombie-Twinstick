using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public Gun powerupGun;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<PlayerControl>().currentGun = powerupGun;
    }
}
