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
    private bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = true;

        if (!target) return;
        agent.destination = target.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if (!target) return;
        agent.destination = target.transform.position;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "spawner") return;
        if (alive)
        {
            alive = false;
            Destroy(other.gameObject); // Destroy projectile
            target.GetComponent<PlayerControl>().score += 1;
            Destroy(gameObject); //Destroy AI Enemy
            GameObject.Find("ScoreText").GetComponent<Text>().text = "Score: " + target.GetComponent<PlayerControl>().score;
        }
    }
}
