using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LDLDistancePopUp : MonoBehaviour
{
    public GameObject ownshipTrackCollider;
    public GameObject ownship;
    private Vector3 ownshipLastPostion;

    public enum DIRECTION
    {
        AHEAD,
        ASTERN,
        STBD,
        PORT
    }

    public DIRECTION direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 shipHeading = (ownship.transform.position - ownshipLastPostion).normalized;
        ownshipLastPostion = ownship.transform.position;
        List<float> distances = new List<float>();

        Vector3 directionVector = new Vector3();

        switch (direction)
        {
            case DIRECTION.AHEAD:
                directionVector = shipHeading;
                break;
            case DIRECTION.ASTERN:
                directionVector = Quaternion.AngleAxis(180, Vector3.up) * shipHeading;
                break;
            case DIRECTION.STBD:
                directionVector = Quaternion.AngleAxis(90, Vector3.up) * shipHeading;
                break;
            case DIRECTION.PORT:
                directionVector = Quaternion.AngleAxis(-90, Vector3.up) * shipHeading;
                break;

        }

        RaycastHit[] hits = Physics.RaycastAll(ownshipTrackCollider.transform.position, directionVector);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];

            if (hit.transform.gameObject.name.Equals("LDL"))
            {
                distances.Add(hit.distance);
            }
        }

        distances.Sort();

        if (distances.Count > 0)
        {
            ProcessDistancePopUp(directionVector, distances[0]);
        }
    }

    private void ProcessDistancePopUp(Vector3 direction, float distance)
    {
        direction = direction * distance;
        Vector3 position = ownshipTrackCollider.transform.position + direction;

        gameObject.transform.position = position;

    }
}
