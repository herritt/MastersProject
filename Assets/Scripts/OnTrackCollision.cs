using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrackCollision : MonoBehaviour
{

    public Material redMaterial;
    public Material greenMaterial;
    public Material blackmMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter + " + other.gameObject);

        GameObject otherParent = other.gameObject.transform.parent.gameObject;

        MeshRenderer meshRenderer = otherParent.GetComponent<MeshRenderer>();

        if (other.name.Contains("left"))
        {
            meshRenderer.material = redMaterial;
        }
        else
        {
            meshRenderer.material = greenMaterial;
        }
        
    }


    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        MeshRenderer meshRenderer = other.gameObject.GetComponentInParent<MeshRenderer>();
        meshRenderer.material = blackmMaterial;
    }


}
