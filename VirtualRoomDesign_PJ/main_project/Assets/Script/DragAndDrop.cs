using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    public GameObject objToPlace;
    public LayerMask mask;
    public float LastPosY;
    public Vector3 mousepos;

    private Renderer rend;
    public Material matGrid, matDefault;
    public bool isdraging;

    void Start()
    {
        rend = GameObject.Find("Ground").GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButton(0))
		{
            isdraging = true;
            mousepos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousepos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                int Posx = (int)Mathf.Round(hit.point.x);
                int Posz = (int)Mathf.Round(hit.point.z);

                objToPlace.transform.position = new Vector3(Posx, LastPosY, Posz);
            }
         
        }
        else
        {
            isdraging = false;
        }
        if (isdraging)
        {
            rend.material = matGrid;
        }
        else if (!isdraging)
        {
            rend.material = matDefault;
        }
    }
}
