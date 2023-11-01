/*
 * 
 * 用來切換使用者選擇item的function，
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnitem : MonoBehaviour
{
    public GameObject gameObject;//放置point的gameobject
    public int id = 0;
	public void Openclick()
	{

	   if (gameObject != null)
	   {    
            switch(id){
                case 0 :
                    gameObject.GetComponent<CubePlace>().objToMove = Resources.Load<GameObject>("Wall");
                    
                    break;
                case 1:
                    gameObject.GetComponent<CubePlace>().objToMove = Resources.Load<GameObject>("ayame");
                    gameObject.GetComponent<CubePlace>().objToPlace = Resources.Load<GameObject>("ayame");
                    break;
                case 2:
                    gameObject.GetComponent<CubePlace>().objToMove = Resources.Load<GameObject>("karin");
                    gameObject.GetComponent<CubePlace>().objToPlace = Resources.Load<GameObject>("karin");
                    break;
            }

			gameObject.SetActive(true);
		}
	}
}
