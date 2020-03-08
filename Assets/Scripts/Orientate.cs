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

        //Vector3 relativePos = other.transform.position - gameObject.transform.position;
        //gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.LookRotation(-relativePos), Time.deltaTime);
        transform.LookAt(other.transform);
    }
}
