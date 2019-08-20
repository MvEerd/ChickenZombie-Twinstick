using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Gun
{
    public int fireBullets = 3;
    public int fireSpread = 10;
    public LineRenderer lr;
    public GameObject player;

    public void Update()
    {
        lr.startWidth = 0;
        lr.endWidth = 0;

        lr.SetPosition(0, player.GetComponent<Collider>().bounds.center);
        cdUpdate();
    }

    override public void Shoot(Transform transform, float aimX, float aimY)
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3((float)aimX, 0, (float)aimY));
        lr.SetPosition(1, lookRotation * (Vector3.forward*20) + player.GetComponent<Collider>().bounds.center);
        lr.startWidth = 0.3f;
        lr.endWidth = 0.3f;
        RaycastHit hit;
        if (Physics.SphereCast(lr.GetPosition(0), 2, new Vector3((float)aimX, 0, (float)aimY), out hit, 20))
        {
            if (hit.collider)
            {
                if (hit.collider.tag == "enemy")
                {
                    lr.SetPosition(1, hit.point);
                    hit.collider.GetComponent<ChickenAI>().Damage(12); //Damage per frame
                }
            }


        }

    }
}
