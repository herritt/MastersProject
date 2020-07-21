using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orientate : MonoBehaviour
{
    public GameObject other;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookPosition = other.transform.position - transform.position;
        lookPosition.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(lookPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);

    }
}
