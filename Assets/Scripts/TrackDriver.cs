using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum DRIVE_MODE {
    AUTO,
    MANUAL
}

public class TrackDriver : MonoBehaviour
{
    private const int WAYPOINT_CHECK_DISTANCE = 20;
    public const float KTS_TO_MPS = 1.944f;
    private const int METRES_PER_NAUTICAL_MILE = 1852;
    private const float YARDS_PER_METRE = 1.094f;

    public GameObject[] waypoints;
    public Vector3 waypoint;
    public int currentWaypointIndex = 0;
    public float shipSpeed = 11;
    public GameObject ship;
    private Rigidbody m_Rigidbody;
    public Vector3 relativePos;

    public int tidalSetBearing = 135;
    public float tidalSetSpeed = 2;
    private Vector3 tidalSet;

    private static int COUNT_SIZE = 5;
    private float[] courses = new float[COUNT_SIZE];
    public float CMG;
    public float SMG;

    private float[] speeds = new float[COUNT_SIZE];
    private int speedIndex;

    public DRIVE_MODE drive_mode;

    Vector3 previousPosition = Vector3.zero;

    private float MAX_SPEED = 30.0f / KTS_TO_MPS;
    private float MIN_SPEED = -15.0f / KTS_TO_MPS;

    public TextMeshProUGUI orderedSpeed;
    private Coroutine showTextRoutine;

    public GameObject[] legs;

    // Start is called before the first frame update
    void Awake()
    {
        ship = gameObject;
        waypoint = waypoints[0].transform.position;
        waypoint = new Vector3(waypoint.x, ship.transform.position.y, waypoint.z);

        // convert shipSpeed from kts to units per second
        shipSpeed /= KTS_TO_MPS;
        m_Rigidbody = GetComponent<Rigidbody>();
        tidalSet = new Vector3(1, 0, 0);
        tidalSetSpeed /= KTS_TO_MPS;

        float tidalAngleInRadians = Mathf.Deg2Rad * tidalSetBearing;

        float x = Mathf.Sin(tidalAngleInRadians);
        float y = Mathf.Cos(tidalAngleInRadians);

        tidalSet = new Vector3(x, 0, y);

        if (orderedSpeed != null)
        {
            orderedSpeed.alpha = 0f;
        }

        
    }

    private void Start()
    {
        StartCoroutine(UpdateKinematics(0.5f));

    }

    private IEnumerator UpdateKinematics(float seconds)
    {
        while (true)
        {

            CalculateShipSpeed(seconds);
            SMG = AverageSpeed();
            CMG = UpdateCMG();
            previousPosition = ship.transform.position;

            yield return new WaitForSeconds(seconds);
        }

    }

    public void OnColliderEnter(string legName)
    {
        //update the current waypoint
        if (drive_mode == DRIVE_MODE.MANUAL)
        {
            foreach (GameObject leg in legs)
            {
                if (leg.name.Equals(legName))
                {
                    Transform endPoint = leg.transform.Find("endpoint");

                    for (int i = 0; i < waypoints.Length; i++)
                    {
                        if (waypoints[i].transform.Equals(endPoint))
                        {
                            currentWaypointIndex = i;
                        }
                    }
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        //UpdateKinematics(Time.deltaTime);

        if (currentWaypointIndex == waypoints.Length) return;

        if (drive_mode == DRIVE_MODE.AUTO)
        {
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


            relativePos = waypoint - ship.transform.position;
            ship.transform.rotation = Quaternion.Lerp(ship.transform.rotation,
                Quaternion.LookRotation(-relativePos), shipSpeed / 30 * Time.deltaTime);

        }
        else
        {
            //drive mode is manual

            //handle turning
            float rotateSpeed = 10f;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                ship.transform.rotation *= Quaternion.Euler(0, 1f * rotateSpeed * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                ship.transform.rotation *= Quaternion.Euler(0, -1f * rotateSpeed * Time.deltaTime, 0);
            }

            //handle speed
            bool speedChanged = false;

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                speedChanged = true;
                shipSpeed += 1/KTS_TO_MPS;

                if (shipSpeed > MAX_SPEED)
                {
                    shipSpeed = MAX_SPEED;
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                speedChanged = true;
                shipSpeed -= 1/KTS_TO_MPS;

                if (shipSpeed < MIN_SPEED)
                {
                    shipSpeed = MIN_SPEED;
                }
            }




            if (speedChanged)
            {
                int displaySpeed = Mathf.RoundToInt(shipSpeed * KTS_TO_MPS);

                orderedSpeed.text = "Ordered Speed: " + displaySpeed + " kts";

                if (showTextRoutine != null)
                {
                    StopCoroutine(showTextRoutine);
                }



                showTextRoutine = StartCoroutine(ShowForTime(orderedSpeed, 1f));

                speedChanged = false;
            }

            
        }

        //calculate a position ahead of the ship based on current heading and tidal set and move towards it
        m_Rigidbody.velocity = tidalSet * tidalSetSpeed - (transform.forward * shipSpeed);


    }

    private IEnumerator ShowForTime(TextMeshProUGUI text, float time)
    {
        text.alpha = 1.0f;
        yield return new WaitForSeconds(time);

        for (float i = 1; i > 0; i-=0.01f)
        {
            if (i < 0.1f) i = 0;

            text.alpha = i;
            yield return new WaitForSeconds(0.01f);
        }


    }

    public float GetHeading()
    {
        float heading = ship.transform.eulerAngles.y - 180;
        if (heading < 0) heading += 360;

        return heading;
    }

    private float UpdateCMG()
    {

        if (previousPosition == Vector3.zero)
        {
            return 0f;
        }

        Vector3 targetDir = transform.position - previousPosition;
        float angle = Vector3.SignedAngle(new Vector3(1, 0, 0), targetDir, Vector3.up) + 90;

        if (angle < 0) angle += 360;

        return angle;
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
