using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlace : MonoBehaviour
{
    public static GameObject what_the_fuck;
    public GameObject new_objToMove;
    public GameObject objToMove;
    public GameObject objToPlace;
    public LayerMask mask;
    public float LastPosY;
    public Vector3 mousepos;

    private Renderer rend;
    public Material matGrid, matDefault;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousepos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousepos);
        RaycastHit hit;
        
        if(Physics.Raycast(ray,out hit, Mathf.Infinity, mask))
		{
            int Posx = (int)Mathf.Round(hit.point.x);
            int Posz = (int)Mathf.Round(hit.point.z);
            objToMove.transform.position = new Vector3(Posx, LastPosY, Posz);
            rend.material = matGrid;
		}

		if (Input.GetMouseButtonDown(0)) 
        {
            Instantiate(objToPlace, objToMove.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            //Destroy(gameObject);
			rend.material = matDefault;
		}
    }
}
