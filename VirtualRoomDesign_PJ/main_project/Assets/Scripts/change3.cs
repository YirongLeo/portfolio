using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change3 : MonoBehaviour
{
    //�ŧi�nŪ��������(�o��OŪ��Material)
	public Material[] material = null;
    public int x;
    //�󴫧��誺�w�q
    Renderer rend;
 
    void Start()
    {
        //�N��ƱqResources����char_yuiŪ���X��
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
