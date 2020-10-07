using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEndOfPassageCollider : MonoBehaviour
{
    public GameObject userManagerObject;

    public void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("TrackCollider"))
        {
            userManagerObject.transform.GetComponent<UserDrivenManager>().OnEndOfPassage();

        }
    }
}
