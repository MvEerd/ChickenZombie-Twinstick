// Floater v0.0.2
// by Donovan Keith
//
// [MIT License](https://opensource.org/licenses/MIT)

using UnityEngine;
using System.Collections;

// Makes objects float up & down while gently spinning.
public class Floater : MonoBehaviour
{
    // User Inputs
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.05f;
    public float frequency = 1f;
    public float decay = 10f;
    private float _timeAlive = 0;
    private bool _destroying = false;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Use this for initialization
    void Start()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _timeAlive += Time.deltaTime;
        if (_timeAlive >= decay)
        {
            this.enabled = false;
            if (_destroying) return;
            _destroying = true;
            Destroy(gameObject, 3);
            var animator = GetComponent<Animator>();
            if(animator != null)
                animator.SetBool("blink", true);
            return;
        }

        if(_timeAlive >= decay * 0.8)
        {
            return; //Drop items when close to decaying
        }

        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);
        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}