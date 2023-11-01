using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//使用TMP要用
using TMPro;

public class CubeMake : MonoBehaviour
{

	//抓取XYZ軸的文本
	public TMP_InputField X;
	public TMP_InputField Y;
	public TMP_InputField Z;
	public GameObject Cube;

	public void MakeCube()
	{
		Vector3 CubeScale = new Vector3();

		//將文本的數字設定為整數
		CubeScale.x = Convert.ToSingle(X.text);
		CubeScale.y = Convert.ToSingle(Y.text);
		CubeScale.z = Convert.ToSingle(Z.text);

		//創建一個Cube
		//GameObject Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

		//創建牆壁
		GameObject Front_Wall = Instantiate(Cube);
		GameObject Back_Wall = Instantiate(Cube);
		GameObject Left_Wall = Instantiate(Cube);
		GameObject Right_Wall = Instantiate(Cube);
		GameObject Up_Wall = Instantiate(Cube);
		GameObject Down_Wall = Instantiate(Cube);

		//Cube.name = "cube1";
		Front_Wall.name = "Front_Wall";
		Back_Wall.name = "Back_Wall";
		Left_Wall.name = "Left_Wall";
		Right_Wall.name = "Right_Wall";
		Up_Wall.name = "Up_Wall";
		Down_Wall.name = "Down_Wall";

		//設定大小
		//Cube.transform.localScale = new Vector3 (CubeScale.x,CubeScale.y,CubeScale.z);
		Front_Wall.transform.localScale = new Vector3(CubeScale.x, CubeScale.y, CubeScale.z);
		Back_Wall.transform.localScale = new Vector3(CubeScale.x, CubeScale.y, CubeScale.z);
		Left_Wall.transform.localScale = new Vector3(CubeScale.x, CubeScale.y, CubeScale.z);
		Right_Wall.transform.localScale = new Vector3(CubeScale.x, CubeScale.y, CubeScale.z);
		Up_Wall.transform.localScale = new Vector3(CubeScale.x, CubeScale.y, CubeScale.z);
		Down_Wall.transform.localScale = new Vector3(CubeScale.x, CubeScale.y, CubeScale.z);

		//設定位置
		//Cube.transform.position = new Vector3(50f,50f,1f);
		//更改在世界中的座標
		Front_Wall.transform.position = new Vector3(0f, 25f, 25f);
		Back_Wall.transform.position = new Vector3(0f, 25f, -25f);
		Left_Wall.transform.position = new Vector3(-25f, 25f, 0f);
		//更改在世界中的旋轉角度
		Left_Wall.transform.eulerAngles = new Vector3(0f, 90f, 0f);
		Right_Wall.transform.position = new Vector3(25f, 25f, 0f);
		Right_Wall.transform.eulerAngles = new Vector3(0f, 90f, 0f);
		Up_Wall.transform.position = new Vector3(0f, 50f, 0f);
		Up_Wall.transform.eulerAngles = new Vector3(90f, 0f, 0f);
		Down_Wall.transform.position = new Vector3(0f, 0f, 0f);
		Down_Wall.transform.eulerAngles = new Vector3(90f, 0f, 0f);
	}

	//public void ChangePosition()
	//{
	//	GameObject change_cub = GameObject.Find("cube1");
	//	Vector3 CubePosition = new Vector3();

	//	CubePosition.x = float.Parse(X.text);
	//	CubePosition.y = float.Parse(Y.text);
	//	CubePosition.z = float.Parse(Z.text);

	//	change_cub.transform.position = new Vector3(CubePosition.x,CubePosition.y,CubePosition.z);
	//}

}
