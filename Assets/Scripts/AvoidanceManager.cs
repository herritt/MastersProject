using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidanceManager : MonoBehaviour
{
    private GameObject ARNA;
    private List<GameObject> objectsInSight = new List<GameObject>();
    public GameObject ownship;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            RaycastHit[] allHit = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
            objectsInSight.Clear();
            foreach (RaycastHit hit in allHit)
            {
                if (hit.collider.gameObject.name == "ARNA_HighlightOnHover")
                {
                    GameObject obj = hit.collider.gameObject;

                    GameObject toggleObject = obj.transform.Find("CanvasToggle").gameObject;

                    if (toggleObject != null)
                    {
                        objectsInSight.Add(toggleObject);
                    }
                    
                }
            }

            ProcessCanvasObjects(objectsInSight);
 
        }
    }

    private void ProcessCanvasObjects(List<GameObject> avoidanceARNA)
    {
        // raycast doesn't guarantee order, so sort based on how far from ownship
        SortedList sortedList = SortBasedOnDistanceFromOwnship(objectsInSight);

        GameObject obj = (GameObject)sortedList.GetByIndex(0);

        obj.SetActive(!obj.activeInHierarchy);

    }

    private SortedList SortBasedOnDistanceFromOwnship(List<GameObject> objects)
    {
        SortedList sortedList = new SortedList();

        foreach(GameObject obj in objects)
        {
            sortedList.Add(Vector3.Distance(obj.transform.position, ownship.transform.position), obj);
        }

        return sortedList;
    }

    public static void PrintKeysAndValues(SortedList myList)
    {
        Debug.Log("\t-KEY-\t-VALUE-");
        for (int i = 0; i < myList.Count; i++)
        {
            Debug.Log(("\t{0}:\t{1}", myList.GetKey(i), myList.GetByIndex(i)));
        }
    }
}

