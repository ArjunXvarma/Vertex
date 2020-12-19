using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAnimation : MonoBehaviour
{
    [SerializeField]
    private Transform targetA, targetB;
    [SerializeField]
    private float speed = 1.0f;
    private bool switching = false;

    // Update is called once per frame
    void Start() 
    {
        Time.timeScale = 1;    
    }
    void FixedUpdate()
    {
        if (switching == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetB.position, speed * Time.deltaTime);
        }

        else if (switching == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetA.position, speed * Time.deltaTime);
        }

        if (transform.position == targetA.position)
        {
            switching = false;
        }

        else if (transform.position == targetB.position)
        {
            switching = true;
        }
    }
}
