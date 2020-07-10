using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// created using formulas and examples found at http://geomalgorithms.com/a07-_distance.html

public class CPA
{
    public Track track1;

    public float CalculateCPATime(Track track1, Track track2)
    {
        Vector3 dv = track1.v - track2.v;

        float dv2 = Vector3.Dot(dv, dv);

        if (Mathf.Approximately(dv2, 0f)) 
        {
            return 0.0f;
        }

        Vector3 w0 = track1.p0 - track2.p0;
        float cpatime = -Vector3.Dot(w0, dv) / dv2;

        return cpatime;
    }

    public float CalculateCPADistance(Track track1, Track track2)
    {
        float ctime = CalculateCPATime(track1, track2);

        Vector3 p1 = track1.p0 + (ctime * track1.v);
        Vector3 p2 = track2.p0 + (ctime * track2.v);

        return Vector3.Distance(p1, p2);

    }

}

public class Track
{
    public Vector3 p0;
    public Vector3 v;
}

