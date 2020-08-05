using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCanvasManager : MonoBehaviour
{
    private GameObject ARNA;
    private List<GameObject> objectsInSight = new List<GameObject>();
    public GameObject ownship;
    public SceneTransition sceneTransition;

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
                if (hit.collider.gameObject.name == "ARNA_HighlightOnHover" ||
                    hit.collider.gameObject.name == "EndPointInfo" ||
                    hit.collider.gameObject.name == "Distance Pop Up")
                {
                    GameObject obj = hit.collider.gameObject;

                    GameObject toggleObject = obj.transform.Find("CanvasToggle").gameObject;

                    if (toggleObject != null)
                    {
                        if (hit.distance > 40f / 1.094)
                        {
                            objectsInSight.Add(toggleObject);

                        }

                    }

                }
            }

            ProcessCanvasObjects(objectsInSight);
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            sceneTransition.TransitionOut("UserStudyMenu");
        }
    }

    private void ProcessCanvasObjects(List<GameObject> avoidanceARNA)
    {
        // raycast doesn't guarantee order, so sort based on how far from ownship
        SortedList sortedList = SortBasedOnDistanceFromOwnship(objectsInSight);

        if (sortedList.Count > 0)
        {
            GameObject obj = (GameObject)sortedList.GetByIndex(0);

            obj.GetComponentInChildren<Canvas>().enabled = !obj.GetComponentInChildren<Canvas>().isActiveAndEnabled;
            Transform post = obj.transform.FindChildRecursive("Post");

            if (post != null)
            {
                post.gameObject.SetActive(obj.GetComponentInChildren<Canvas>().enabled);
            }


        }

    }

    private SortedList SortBasedOnDistanceFromOwnship(List<GameObject> objects)
    {
        SortedList sortedList = new SortedList();

        foreach (GameObject obj in objects)
        {
            sortedList.Add(Vector3.Distance(obj.transform.position, ownship.transform.position), obj);
        }

        return sortedList;
    }

}

