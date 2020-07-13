using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectLook : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(gameObject.transform.position, Vector3.forward, out hitInfo))
        {
            Debug.Log(hitInfo.collider.gameObject.transform.parent.ToString());
        }
    }
}
