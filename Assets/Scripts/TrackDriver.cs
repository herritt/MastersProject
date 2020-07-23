using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackDriver : MonoBehaviour
{
    private const int WAYPOINT_CHECK_DISTANCE = 20;
    private const float KTS_TO_MPS = 1.944f;
    private const int METRES_PER_NAUTICAL_MILE = 1852;
    private const float YARDS_PER_METRE = 1.094f;

    public GameObject[] waypoints;
    public Vector3 waypoint;
    int currentWaypointIndex = 0;
    public float shipSpeed = 11;
    public GameObject ship;
    public Rigidbody m_Rigidbody;
    public Vector3 relativePos;
    public Vector3 tidalSet = Quaternion.AngleAxis(-135, Vector3.up) * Vector3.forward * 0.5f;

    private static int COUNT_SIZE = 5;
    private float[] courses = new float[COUNT_SIZE];
    public float CMG = 0f;

    public float SMG = 0f;

    private float[] speeds = new float[COUNT_SIZE];
    private int speedIndex;
    

    Vector3 previousPosition = Vector3.zero;

    // Start is called before the first frame update
    void Awake()
    {
        ship = gameObject;
        waypoint = waypoints[0].transform.position;
        waypoint = new Vector3(waypoint.x, ship.transform.position.y, waypoint.z);

        // convert shipSpeed from kts to units per second
        shipSpeed /= KTS_TO_MPS;
        m_Rigidbody = GetComponent<Rigidbody>();


    }

    private void Start()
    {
        StartCoroutine(UpdateKinematics(0.25f));

    }

    private IEnumerator UpdateKinematics(float seconds)
    {
        while (true)
        {

            CalculateShipSpeed(seconds);
            SMG = AverageSpeed();
            previousPosition = ship.transform.position;

            yield return new WaitForSeconds(seconds);
        }

    }

    // Update is called once per frame
    void Update()
    {
        UpdateKinematics(Time.deltaTime);
        

        if (currentWaypointIndex == waypoints.Length) return;

        if (Vector3.Distance(waypoints[currentWaypointIndex].transform.position, ship.transform.position) < WAYPOINT_CHECK_DISTANCE)
        {
            if (currentWaypointIndex < waypoints.Length)
            {
                currentWaypointIndex++;
            }

            if (currentWaypointIndex < waypoints.Length)
            {
                waypoint = waypoints[currentWaypointIndex].transform.position;
                waypoint = new Vector3(waypoint.x, ship.transform.position.y, waypoint.z);
            }


        }

        var t = shipSpeed * Time.deltaTime;

        relativePos = waypoint - ship.transform.position;
        ship.transform.rotation = Quaternion.Lerp(ship.transform.rotation, Quaternion.LookRotation(-relativePos), shipSpeed / 30 * Time.deltaTime);

        //calculate a position ahead of the ship based on current heading and move towards it
        Vector3 newPosition = ship.transform.rotation * Vector3.forward;

        //ship.transform.position = Vector3.MoveTowards(ship.transform.position, newPosition, t);
        m_Rigidbody.velocity = -transform.forward * shipSpeed;

        


    }

    public float AverageSpeed()
    {
        int num_non_zeros = 0;
        float sum = 0.0f;

        for (int i = 0; i < COUNT_SIZE; i++)
        {
            if (Math.Abs(speeds[i]) < 0.001f)
            {
                num_non_zeros++;
            }
            sum += speeds[i];

        }

        return sum / (COUNT_SIZE - num_non_zeros);
    }

    public float CalculateShipSpeed(float seconds)
    {
        if (previousPosition == Vector3.zero)
        {
            return 0f;
        }

        Vector3 currentPosition = transform.position;
        float distance = Vector3.Distance(previousPosition, currentPosition);

        float time = seconds;
        float speed = 0;

        if (time != 0)
        {
            speed = distance / time;
        }

        //conver m_p_s to kts
        speed *= KTS_TO_MPS;

        speeds[speedIndex] = speed;
        speedIndex = (speedIndex + 1) % COUNT_SIZE;

        return speed;
    }


}
