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
        if(other.tag == "projectile")
        {
            Destroy(other.gameObject); // Destroy projectile
            destroySelf(true);
            return;
        }
        
        if (alive)
        {
            PlayerControl playerControl = other.GetComponent<PlayerControl>();
            if (playerControl.jumpedEnemy > 0 && playerControl.jumpedEnemy < 0.1) return;

            if (other.GetComponent<Collider>().bounds.min.y >= GetComponent<Collider>().bounds.center.y) {
                playerControl.jumpedEnemy = 0;
                playerControl.Hop();
                destroySelf(true);
                return;
            }

            Destroy(other.gameObject); //Kill user
            alive = false;
        }
    }

    private void destroySelf(bool score = true) 
    {
        Destroy(gameObject);

        if (!target||!score) return;
        target.GetComponent<PlayerControl>().score += 1;
        GameObject.Find("ScoreText").GetComponent<Text>().text = "Score: " + target.GetComponent<PlayerControl>().score;
    }
}
