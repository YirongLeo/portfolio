using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObjName : MonoBehaviour
{

    public GameObject ObjName;
    public InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetName()
	{
        Debug.Log(ObjName.name);
    }
}
