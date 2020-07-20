using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HELM_STATE
{

    PORT,
    MIDSHIPS,
    STBD

}

public class RotateWheel : MonoBehaviour
{
    public GameObject ownship;
    private TrackDriver trackDriver;
    float previousAngle;
    bool canStartAnimation = true;
    public HELM_STATE helmState;

    // Start is called before the first frame update
    void Start()
    {
        trackDriver = ownship.GetComponent<TrackDriver>();
        helmState = HELM_STATE.MIDSHIPS;
    }

    // Update is called once per frame
    void Update()
    {

        //determine the angle of the turn
        Vector3 targetDir = trackDriver.waypoint - trackDriver.ship.transform.position;
        float angleBetween = Vector3.Angle(-trackDriver.ship.transform.forward, targetDir);

        //determine the direction of the turn
        Vector3 cross = Vector3.Cross(-trackDriver.ship.transform.forward, targetDir);

        if (canStartAnimation)
        {
            //determine if starting turn or steadying up
            if (previousAngle > angleBetween && angleBetween < 5.0f)
            {
                if (helmState == HELM_STATE.PORT)
                {
                    StartCoroutine("AnimateStbd", 4f);
                }
                else if (helmState == HELM_STATE.STBD)
                {
                    StartCoroutine("AnimatePort", 4f);
                }
                helmState = HELM_STATE.MIDSHIPS;
            }

            if (previousAngle < angleBetween && angleBetween > 5.0f)
            {
                if (cross.y > 0.1f)
                {
                
                    StartCoroutine("AnimateStbd", 4f);
                    helmState = HELM_STATE.STBD;
                }
                else if (cross.y < 0.1f)
                {
                    StartCoroutine("AnimatePort", 4f);
                    helmState = HELM_STATE.PORT;
                }

            }

        }

        previousAngle = angleBetween;

    }

    private IEnumerator AnimateForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canStartAnimation = true;
    }

    private IEnumerator AnimateStbd(float timer)
    {
        if (canStartAnimation)
        {
            canStartAnimation = false;
            StartCoroutine("AnimateForSeconds", timer);          
        }


        while (!canStartAnimation)
        {
            Debug.Log("Animating stbd: " + Time.deltaTime);
            transform.RotateAround(gameObject.transform.position, trackDriver.ship.transform.forward, 20 * Time.deltaTime);
            yield return null;
        }

        yield return null;
   
    }

    private IEnumerator AnimatePort(float timer)
    {
        if (canStartAnimation)
        {
            canStartAnimation = false;
            StartCoroutine("AnimateForSeconds", timer);
        }

        while (!canStartAnimation)
        {
            Debug.Log("Animating port: " + Time.deltaTime);
            transform.RotateAround(gameObject.transform.position, trackDriver.ship.transform.forward, -20 * Time.deltaTime);
            yield return null;

        }

        yield return null;
        
    }
}
