using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build_sec_id : MonoBehaviour
{
    public int id_from_main_panel;
    // Start is called before the first frame update
    public void my_OnClicked(Button button)
    {
        //�Ψө�m���~���u���ܾ��v
        GameObject gameObject = GameObject.Find("Point");
        gameObject.SetActive(true);
        
        //��X�Ĥ@�Ӫ����K����
        string str = loadimage.object_list[int.Parse(button.name)];
        GameObject obj =  Resources.Load<GameObject>(str);
        obj.transform.parent = gameObject.transform;

        gameObject.GetComponent<CubePlace>().objToMove = obj;

    }
}
