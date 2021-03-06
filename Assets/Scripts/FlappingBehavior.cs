using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappingBehavior : MonoBehaviour
{
    //The strength of each flap
    public float Power = 1.0f;

    //A reference to the object's Rigidbody, used for physics calculations
    private Rigidbody _body;

    // Start is called before the first frame update
    void Start()
    {
        //Get a reference to the Rigidbody
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the button is pressed
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            //Calculate the new velocity
            _body.velocity = new Vector3(0, 1, 0) * Power;
        }
    }
}
