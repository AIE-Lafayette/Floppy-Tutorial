using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappingBehavior : MonoBehaviour
{
    public float power = 1.0f;

    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        //Get a reference to the Rigidbody
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 flap = new Vector3(0, 1, 0) * power;
            rigidbody.velocity = flap;
        }
    }
}
