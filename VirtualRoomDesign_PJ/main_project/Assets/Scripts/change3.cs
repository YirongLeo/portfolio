using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change3 : MonoBehaviour
{
    //宣告要讀取的類型(這邊是讀取Material)
	public Material[] material = null;
    public int x;
    //更換材質的定義
    Renderer rend;
 
    void Start()
    {
        //將資料從Resources內的char_yui讀取出來
		material = Resources.LoadAll<Material>("char_yui3");
    }

    public void ChangeTexture()
    {
        x = 0;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[x];
    }

}
