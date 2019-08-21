using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
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
    public List<Lootobject> lootTable = new List<Lootobject>();
    public int dropRate;
    private bool alive = true;
    public int _health = 100;
    public float attackDelay = 1;
    float _lastAttack = 0;

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
        if (_health <= 0){
            alive = false;
            destroySelf();
            return;
        }

        if (!target) return;
        agent.destination = target.transform.position;
    }
    
    private void OnCollisionStay(Collision collision)
    {
        
        if (!alive) return;

        if (collision.other.tag == "projectile")
        {
            Destroy(collision.other.gameObject); // Destroy projectile
            destroySelf();
            return;
        }

        if (collision.other.tag=="Player")
        {
            PlayerControl playerControl = collision.other.GetComponent<PlayerControl>();

            /*if (other.GetComponent<Collider>().bounds.min.y >= GetComponent<Collider>().bounds.center.y) {
                if (playerControl.jumpedEnemy > 0 && playerControl.jumpedEnemy < 0.1) return;
                playerControl.Hop();
                playerControl.jumpedEnemy = 0;
                destroySelf();
                return;
            }*/

            if (Time.time < _lastAttack + attackDelay) return;

            collision.other.gameObject.GetComponent<PlayerControl>().Damage(10);
            _lastAttack = Time.time;
        }
    }

    private void destroySelf() 
    {
        dropLoot();

        Destroy(gameObject);
    }

    public void Damage(int damage = 100)
    {
        _health -= damage;
        if (_health <= 0)
        {
            alive = false;
            destroySelf();
        }
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
