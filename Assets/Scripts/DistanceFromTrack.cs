using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceFromTrack : MonoBehaviour
{
    public GameObject ownshipTrackCollider;
    public GameObject ownship;
    public List<GameObject> legs;
    private TrackDriver trackDriver;

    // Start is called before the first frame update
    void Start()
    {
        trackDriver = ownship.GetComponent<TrackDriver>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 shipHeading = trackDriver.waypoint - ownship.transform.position;

        //to port
        Vector3 portDirectionVector = Quaternion.AngleAxis(-90, Vector3.up) * shipHeading;

        RaycastHit[] portHits = Physics.RaycastAll(ownshipTrackCollider.transform.position, portDirectionVector);

        float portHitDistance = 0.0f;
        for (int i = 0; i < portHits.Length; i++)
        {
            RaycastHit hit = portHits[i];

            if (hit.transform.gameObject.name.Equals("track"))
            {
                portHitDistance = hit.distance;
                ProcessDistancePopUp(portDirectionVector, hit.distance);
            }

        }

        //to stbd
        Vector3 stbdDirectionVector = Quaternion.AngleAxis(90, Vector3.up) * shipHeading;

        RaycastHit[] stbdHits = Physics.RaycastAll(ownshipTrackCollider.transform.position, stbdDirectionVector);

        for (int i = 0; i < stbdHits.Length; i++)
        {
            RaycastHit hit = stbdHits[i];

            if (hit.transform.gameObject.name.Equals("track"))
            {
                if (hit.distance < portHitDistance)
                {
                    ProcessDistancePopUp(stbdDirectionVector, hit.distance);
                }
                
            }

        }


    }

    private void ProcessDistancePopUp(Vector3 direction, float distance)
    {
        direction = direction.normalized;
        direction = direction * distance;
        Vector3 position = ownshipTrackCollider.transform.position + direction;

        gameObject.transform.position = position;

    }

}
