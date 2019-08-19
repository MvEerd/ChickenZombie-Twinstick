using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public Gun currentGun;
    public GameObject Gameover;

    private float distToGround = 0;
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

        float moveX = gamePadState.ThumbSticks.Left.X;
        float moveY = gamePadState.ThumbSticks.Left.Y;

        float mouseAxisX = MinMax(Input.mousePosition.x, 0, Screen.width, -1, 1);
        float mouseAxisY = MinMax(Input.mousePosition.y, 0, Screen.height, -1, 1);

        float aimX = Input.GetMouseButton(0) ? mouseAxisX : gamePadState.ThumbSticks.Right.X;
        float aimY = Input.GetMouseButton(0) ? mouseAxisY : gamePadState.ThumbSticks.Right.Y;


        if (moveX != 0 || moveY != 0)
        {
            setAnimationState("Run");
            Vector3 tempVect = new Vector3(moveX * speed, rb.velocity.y, moveY * speed);
            tempVect = tempVect.normalized * speed * Time.deltaTime;
            rb.MovePosition(transform.position + tempVect);

            if (aimX == 0 && aimY == 0)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3((float)moveX * speed, 0, (float)moveY * speed)), 1000 * Time.deltaTime);
        } else
        {
            if(IsGrounded()) setAnimationState("");
        }

        currentGun.cdUpdate();

        if (aimX != 0 || aimY != 0)
        {
            currentGun.Shoot(transform, aimX, aimY);
        }

        if (gamePadState.Buttons.A == ButtonState.Pressed)
        {
            if (IsGrounded())
            {
                rb.AddForce(Vector3.up, ForceMode.Impulse);
            }
        }

        if (!IsGrounded()) setAnimationState("Run");

    }

    private void OnDestroy() {
        Gameover.active = true;
        GamePad.SetVibration(PlayerIndex.One, 0,0);

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

    public float MinMax(float x, float min, float max, float new_min, float new_max)
    {
        x = (x - min) * (new_max - new_min) / (max - min) + new_min;
        return x;
    }

}

