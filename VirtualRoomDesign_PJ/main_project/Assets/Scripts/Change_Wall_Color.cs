using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Change_Wall_Color : MonoBehaviour
{
    private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
	{
        renderer.material.color = Color.red;
	}

    private void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }
}
