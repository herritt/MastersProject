using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooFarOffTrack : MonoBehaviour
{

    private int collisionCount = 0;
    public GameObject userManagerObject;

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("on track collider"))
        {
            
            collisionCount++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name.Equals("on track collider"))
        {
            collisionCount--;
        }

        if (collisionCount <= 0)
        {
            userManagerObject.transform.GetComponent<UserDrivenManager>().OnTooFarOffTrack();
        }
    }

}
