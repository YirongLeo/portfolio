using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build_main_ui : MonoBehaviour
{
    public GameObject sec_object;

    public void open_sec_panel(Button button)
    {
        sec_object.SetActive(true);

        sec_object.GetComponent<Build_sec_id>().id_from_main_panel = int.Parse(button.name);
    }
}
