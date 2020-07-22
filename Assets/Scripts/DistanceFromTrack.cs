using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceFromTrack : MonoBehaviour
{
    public GameObject ownshipTrackCollider;
    public GameObject ownship;
    public List<GameObject> legs;
    private TrackDriver trackDriver;
    private Vector3 ownshipLastPostion;

    // Start is called before the first frame update
    void Start()
    {
        trackDriver = ownship.GetComponent<TrackDriver>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 shipHeading = (ownship.transform.position - ownshipLastPostion).normalized;
        ownshipLastPostion = ownship.transform.position;
        List<float> distances = new List<float>();

        //to port
        Vector3 portDirectionVector = Quaternion.AngleAxis(-90, Vector3.up) * shipHeading;

        RaycastHit[] portHits = Physics.RaycastAll(ownshipTrackCollider.transform.position, portDirectionVector);

        bool portTrackFound = false;
        bool stbdTrackFound = false;
        float portHitDistance = -1f, stbdHitDistance = -1f;

        for (int i = 0; i < portHits.Length; i++)
        {
            RaycastHit hit = portHits[i];

            if (hit.transform.gameObject.name.Equals("track"))
            {
                Debug.Log(hit.transform);
                portTrackFound = true;
                distances.Add(hit.distance);
            }
        }

        if (portTrackFound)
        {
            distances.Sort();
            portHitDistance = distances[0];
            distances.Clear();
        }

        //to stbd
        Vector3 stbdDirectionVector = Quaternion.AngleAxis(90, Vector3.up) * shipHeading;

        RaycastHit[] stbdHits = Physics.RaycastAll(ownshipTrackCollider.transform.position, stbdDirectionVector);

        for (int i = 0; i < stbdHits.Length; i++)
        {
            RaycastHit hit = stbdHits[i];

            if (hit.transform.gameObject.name.Equals("track"))
            {
                stbdTrackFound = true;
                distances.Add(hit.distance);
            }

        }

        if (stbdTrackFound)
        {
            distances.Sort();
            stbdHitDistance = distances[0];
        }

        if (portTrackFound && stbdTrackFound)
        {
            if (portHitDistance < stbdHitDistance)
            {
                ProcessDistancePopUp(portDirectionVector, portHitDistance);
            }
            else
            {
                ProcessDistancePopUp(stbdDirectionVector, stbdHitDistance);
            }
        } else if (portTrackFound)
        {
            ProcessDistancePopUp(portDirectionVector, portHitDistance);
        }
        else if (stbdTrackFound)
        {
            ProcessDistancePopUp(stbdDirectionVector, stbdHitDistance);
        }


}

private void ProcessDistancePopUp(Vector3 direction, float distance)
    {
        direction = direction * distance;
        Vector3 position = ownshipTrackCollider.transform.position + direction;

        gameObject.transform.position = position;

    }

}
