using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void OnStartGame(GameObject Target)
    {
        Debug.Log(Target.name);
		this.Set_Texture_Wall(Target);
    }

	private void Set_Texture_Wall(GameObject Cube)
	{
		//�ʺA�ͦ�change��script
		Cube.AddComponent<change>();
		Cube.AddComponent<change1>();
		Cube.AddComponent<change2>();
		Cube.AddComponent<change3>();
		
		//�z�L����W�ٴM�䪫��
		GameObject find_btn2 = GameObject.Find("main_sec_btn");
		GameObject find_btn3 = GameObject.Find("main_third_btn");
		GameObject find_btn4 = GameObject.Find("main_forth_btn");
		GameObject find_btn5 = GameObject.Find("main_fifth_btn");

		//�j�Mbtn2�����s����
		Button button2 = find_btn2.GetComponent<Button>();
		Button button3 = find_btn3.GetComponent<Button>();
		Button button4 = find_btn4.GetComponent<Button>();
		Button button5 = find_btn5.GetComponent<Button>();

		//�]�w���s���檺�ʧ@
		button2.onClick.AddListener(delegate ()
		{
			//���change�o��script
			change testclick = GameObject.FindObjectOfType<change>();
			//����change���U��function
			testclick.ChangeTexture();
		});

		button3.onClick.AddListener(delegate ()
		{
			change1 testclick = GameObject.FindObjectOfType<change1>();
			testclick.ChangeTexture();
		});

		button4.onClick.AddListener(delegate ()
		{
			change2 testclick = GameObject.FindObjectOfType<change2>();
			testclick.ChangeTexture();
		});

		button5.onClick.AddListener(delegate ()
		{
			change3 testclick = GameObject.FindObjectOfType<change3>();
			testclick.ChangeTexture();
		});
	}
}
