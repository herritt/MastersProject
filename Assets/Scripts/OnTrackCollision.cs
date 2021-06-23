using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrackCollision : MonoBehaviour
{

    public Material redMaterial;
    public Material greenMaterial;
    public Material blackMaterial;

    private TrackDriver trackDriver;

    // Start is called before the first frame update
    void Start()
    {
        trackDriver = GetComponentInParent<TrackDriver>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == null) return;

        MeshRenderer meshRenderer = other.transform.parent.Find("track").GetComponent<MeshRenderer>();

        if (meshRenderer != null)
        {
            trackDriver.OnColliderEnter(other.transform.parent.name);
        }

        if (other.name.Contains("left"))
        {          
            meshRenderer.material = redMaterial;
        }
        else if (other.name.Contains("right"))
        {
            meshRenderer.material = greenMaterial;
        }
        else
        {
            meshRenderer.material = blackMaterial;
        }
        
    }



}
