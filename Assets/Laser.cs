using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Gun
{
    public int fireBullets = 3;
    public int fireSpread = 10;
    public LineRenderer lr;
    public GameObject player;
    public LayerMask layerMask;
    public GameObject sparkObj;

    override public void UpdateGun()
    {
        lr.SetPosition(0, player.GetComponent<Collider>().bounds.center);
    }

    override public void IdleGun()
    {
        lr.startWidth = 0;
        lr.endWidth = 0;
        sparkObj.SetActive(false);
    }


    override public void Shoot(Transform transform, float aimX, float aimY)
    {
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3((float)aimX, 0, (float)aimY));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 1000 * Time.deltaTime);

        //lr.SetPosition(0, player.GetComponent<Collider>().bounds.center);
        lr.SetPosition(1, lookRotation * (Vector3.forward*20) + player.GetComponent<Collider>().bounds.center);
        lr.startWidth = 0.3f;
        lr.endWidth = 0.3f;
        RaycastHit hit;
        //Debug.DrawRay(lr.GetPosition(0), new Vector3((float)aimX, 0, (float)aimY) * 10000f, Color.blue);
        if (Physics.SphereCast(lr.GetPosition(0), 0.3f, new Vector3((float)aimX, 0, (float)aimY), out hit, 20,  layerMask))
        {
            if (hit.collider)
            {
                    lr.SetPosition(1, hit.point);
                    sparkObj.SetActive(true);
                    sparkObj.transform.position = hit.point;
                    sparkObj.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);

                    Enemy enemy = hit.collider.GetComponent<Enemy>();
                    if(enemy)
                        enemy.Damage(9); //Damage per frame

                return;
            }
        }
        sparkObj.SetActive(false);//Hide spark if nothing is hit
    }

}
