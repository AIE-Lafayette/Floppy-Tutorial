using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeBehavior : MonoBehaviour
{
    public float Speed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-Speed * Time.deltaTime, 0.0f, 0.0f);
    }
}
