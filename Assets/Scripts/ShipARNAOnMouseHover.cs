using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipARNAOnMouseHover : MonoBehaviour
{
    public GameObject ARNA;

    // Start is called before the first frame update
    void Start()
    {
        ARNA.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnMouseEnter()
    {
        ARNA.SetActive(true);
    }

    public void OnMouseExit()
    {
        ARNA.SetActive(false);
    }
}
