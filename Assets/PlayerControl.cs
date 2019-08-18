using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public GameObject projectile;

    public int score = 0;
    private float timeSinceLastBullet = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    public float MinMax(float x, float min, float max, float new_min, float new_max)
    {
        x = (x - min) * (new_max - new_min) / (max - min) + new_min;
        return x;
    }
    
    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        float mouseAxisX = MinMax(Input.mousePosition.x, 0, Screen.width, -1, 1);
        float mouseAxisY = MinMax(Input.mousePosition.y, 0, Screen.height, -1, 1);

        float aimX = Input.GetMouseButton(0) ? mouseAxisX : Input.GetAxis("Mouse X"); 
        float aimY = Input.GetMouseButton(0) ? mouseAxisY : Input.GetAxis("Mouse Y");
        

        timeSinceLastBullet += Time.deltaTime;


        if (moveX != 0 || moveY != 0)
        {
            setAnimationState("Run");
            Vector3 tempVect = new Vector3((float)moveX * speed, rb.velocity.y, (float)moveY * speed);
            tempVect = tempVect.normalized * speed * Time.deltaTime;
            rb.MovePosition(transform.position + tempVect);

            if (aimX == 0 && aimY == 0)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3((float)moveX * speed, 0, (float)moveY * speed)), 1000 * Time.deltaTime);
        } else
        {
            setAnimationState("");
        }


        if (aimX != 0 || aimY != 0)
        {
            float firerate = 0.09f; //0.015 ~ 0.5f
            float fireforce = 70f;
            float firettl = 2;

            Quaternion lookRotation = Quaternion.LookRotation(new Vector3((float)aimX * speed, 0, (float)aimY * speed));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 1000 * Time.deltaTime);

            if (timeSinceLastBullet < firerate) return;
            timeSinceLastBullet = 0;

            Vector3 bulletBuffer = lookRotation * new Vector3(0, 0, 0.5f);

            GameObject bullet = Instantiate(projectile, transform.position + bulletBuffer + new Vector3(0, 0.2f,0), lookRotation) as GameObject;
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * fireforce);
            Destroy(bullet, firettl);


        }
    }
    void setAnimationState(string animation)
    {
        Animator animator = GetComponent<Animator>();
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            animator.SetBool(parameter.name, false);
        }

        if (animation == "") return;
        animator.SetBool(animation, true);
    }

}

