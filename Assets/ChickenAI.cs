using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ChickenAI : MonoBehaviour
{
    public GameObject target;
    public NavMeshAgent agent;
    public Text scoreText;
   

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = true;

        agent.destination = target.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        agent.destination = target.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject); //Destroy AI Enemy
        Destroy(other.gameObject); // Destroy projectile
        target.GetComponent<PlayerControl>().score += 1;
        scoreText.text = "Score: " + target.GetComponent<PlayerControl>().score;
    }
}
