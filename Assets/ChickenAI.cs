using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ChickenAI : MonoBehaviour
{

    [System.Serializable]
    public class Lootobject
    {
        public string name;
        public GameObject prefab;
        public int dropRate;
    }

    public GameObject target;
    public NavMeshAgent agent;
    public Text scoreText;
    public List<Lootobject> lootTable = new List<Lootobject>();
    public int dropRate;
    private bool alive = true;
    public int health = 100;

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
        if (health <= 0) destroySelf();

        if (!target) return;
        agent.destination = target.transform.position;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "spawner") return;
        if (other.tag == "projectile")
        {
            Destroy(other.gameObject); // Destroy projectile
            destroySelf(true);
            return;
        }

        if (alive)
        {
            PlayerControl playerControl = other.GetComponent<PlayerControl>();

            if (other.GetComponent<Collider>().bounds.min.y >= GetComponent<Collider>().bounds.center.y) {
                if (playerControl.jumpedEnemy > 0 && playerControl.jumpedEnemy < 0.1) return;
                playerControl.Hop();
                playerControl.jumpedEnemy = 0;
                destroySelf(true);
                return;
            }

            Destroy(other.gameObject); //Kill user
            alive = false;
        }
    }

    private void destroySelf(bool score = true) 
    {
        dropLoot();

        if (!target||!score) return;
        target.GetComponent<PlayerControl>().score += 1;
        GameObject.Find("ScoreText").GetComponent<Text>().text = "Score: " + target.GetComponent<PlayerControl>().score;

        Destroy(gameObject);
    }

    public void Damage(int damage = 100)
    {
        health -= damage;
    }

    private void dropLoot()
    {
        if (Random.Range(0, 100) > dropRate) return;


        for (int i = 0; i < lootTable.Count; i++)
        {
            if (Random.Range(0, 100) > lootTable[i].dropRate) return;
                GameObject drop = Instantiate(lootTable[i].prefab, transform.position, transform.rotation) as GameObject;
            }
        }


    }
