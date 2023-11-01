using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousemove : MonoBehaviour
{   
	
	//宣告滑鼠移動、敏感度
	// [SerializeField]是序列化，讓這個宣告的變數可以顯示在Unity上，透過Unity進行改動。

	//控制相機將移動多少以響應檢測到的鼠標輸入。
	[SerializeField] private float sensitivity = 0.5f;
	//控制鼠標輸入停止後相機將繼續移動的程度。
	[SerializeField] private float drag = 0.5f;

	//Vector向量是在坐標系中有方向、大小的值。
	//Vector2，二維向量，在平面座標系中，與原點的差值。Vector2(x,y)。
	//Vector3，三維向量，在三維空間中，與原點的差值。Vector3(x,y,z)。
	
	//儲存滑鼠方向
	private Vector2 mouseDir;
	//儲存滑鼠平滑
	private Vector2 smoothing;
	//儲存計算結果
	private Vector2 result;
	//宣告一個變數Transform，Transform是針對物件進行轉換的功能，包含縮放、旋轉、扭曲、平移
	private Transform character;

	//設定為true時，可以透過移動滑鼠來移動相機，設定為false則不能
	public bool LockEnabled { get; set; } = true;
	
	//會比Start()早調用
	void Awake()
	{
		//父子物件關係儲存在transform，ex:當父物件的世界座標為「0,5,10」，子物件的世界座標為「1,6,11」時，在子物件的座標顯示中會呈現「1,1,1」，也就是以父物件作為基準。
		//設定獲取父物件
		character = transform.parent;
		//確保執行時可以透過移動滑鼠來移動相機
		LockEnabled = true;
	}

	// Update is called once per frame
	void Update()
	{
		 //確保執行時可以透過移動滑鼠來移動相機
		if(LockEnabled == true)
		{
			//Input.GetAxisRaw套件用來記錄滑鼠左右及上下移動的座標
			mouseDir = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
			// 可以藉由更改靈敏度調高滑鼠移動速度
			mouseDir *= sensitivity;
			 //計算平滑運動
			smoothing = Vector2.Lerp(smoothing, mouseDir, 1/drag);
			//持續記錄當前的滑動值
			result += smoothing;
			//避免y軸翻轉
			result.y = Mathf.Clamp(result.y, -70, 70);

			//使相機的旋轉角度與滑鼠的旋轉角度相同
			character.rotation = Quaternion.AngleAxis(result.x, character.up);
			//防止萬向節鎖定
			transform.localRotation = Quaternion.AngleAxis(-result.y, Vector3.right);

		}

		if(Input.GetKeyDown(KeyCode.Z))
		{
			Debug.Log("z key was pressed.");
			LockEnabled = false;
		}

		if (Input.GetKeyDown(KeyCode.X))
		{
			Debug.Log("x key was pressed.");
			LockEnabled = true;
		}
	}

	//public void stop_script()
	//{
	//	LockEnabled = false;
	//}

	//public void open_script()
	//{
	//	LockEnabled = true;
	//}
}
