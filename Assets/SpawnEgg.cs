using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEgg : MonoBehaviour
{
    public float spawnRate = 3; //Spawns per second
    public GameObject enemy;
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float spawnInterval = 1 / spawnRate;
        float spawnsThisFrame = (timer / spawnInterval);
        float completedSpawns = (float)Math.Floor(timer / spawnInterval);

        if(spawnsThisFrame >= 1){
            timer -= spawnInterval * completedSpawns;
            GameObject Enemy = Instantiate(enemy, transform.position + new Vector3(0, 0.2f, 0), Quaternion.LookRotation(transform.forward)) as GameObject;
            GetComponent<ParticleSystem>().Play();
        }

    }
}
