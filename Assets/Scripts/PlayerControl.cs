using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public Gun currentGun;
    public GameObject Gameover;
    public float jumpHeight;
    public float GroundDistance = 0.1f;
    public float DashDistance = 5f;
    public LayerMask Ground;
    private bool _isGrounded = true;
    private Transform _groundChecker;
    private int _health = 100;
    public Slider healthBar;

    private float distToGround = 0;
    public int score = 0;
    public float jumpedEnemy;

    private Vector3 _moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        _groundChecker = transform.GetChild(0);
    }

    void FixedUpdate()
    {
        if (_moveDirection != Vector3.zero)
            rb.MovePosition(rb.position + _moveDirection * speed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);

        //Store movement direction to use in FixedUpdate
        _moveDirection = Vector3.zero;
        _moveDirection.x = Input.GetAxis("Horizontal");
        _moveDirection.z = Input.GetAxis("Vertical");

        float mouseAxisX = MinMax(Input.mousePosition.x, 0, Screen.width, -1, 1);
        float mouseAxisY = MinMax(Input.mousePosition.y, 0, Screen.height, -1, 1);

        float aimX = Input.GetMouseButton(0) ? mouseAxisX : Input.GetAxis("Mouse X");
        float aimY = Input.GetMouseButton(0) ? mouseAxisY : Input.GetAxis("Mouse Y");
        Vector3 _aimDirection = Vector3.zero;
        _aimDirection = Vector3.zero;
        _aimDirection.x = aimX;
        _aimDirection.z = aimY;

        //Rotate and set running animation while moving but not aiming
        if (_moveDirection != Vector3.zero && _aimDirection == Vector3.zero)
        {
            transform.forward = _moveDirection;
            setAnimationState("Run");
        } else
        {
            if (_isGrounded) setAnimationState("");
        }
        //Play Run animation while falling to flap wings
        if (!_isGrounded) setAnimationState("Run");


        currentGun.UpdateGun();
        if (_aimDirection != Vector3.zero)
        {
            currentGun.Shoot(transform, aimX, aimY);
            setAnimationState("Run");
        } else
        {
            currentGun.IdleGun();
        }

        if (Input.GetButton("Jump"))
        {
            Jump();
        }

        
        jumpedEnemy += Time.deltaTime;
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
    }

    public void Hop()
    {
        rb.AddForce(Vector3.up * Mathf.Sqrt(0.5f * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    }

    public void Damage(int damage = 10)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
        healthBar.value = _health;
    }

    private void OnDestroy() {
        Gameover.active = true;

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

