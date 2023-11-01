using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    //要擺放的物件類型
    public Transform gridCellPrefab;
    public Transform cube;
    public Transform onMousePrefab;
    //定義棋盤格的大小
    [SerializeField] private int height;
    [SerializeField] int width;
    private Node[,] nodes;
    private Vector3 mousePosition;
    public Vector3 smoothMousePostion;
    private Plane plane;

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();

        ////向上走 //可以直接使用Vector的屬性Vector3.up，就不需要new一個變數
        //transform.Position = new Vector3(0, 0, 0);  //以世界為中心的座標
        plane = new Plane(Vector3.up, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePositionOnGrid();
    }


    void GetMousePositionOnGrid()
	{
        //獲取Camera照射到的滑鼠坐標
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //此函數設置enter為沿射線與平面相交的距離。如果射線平行於平面，函數返回false並設置enter為零。
        //如果射線指向與平面相反的方向，則函數返回false/ 並設置enter為沿射線的距離（負值）。
        if (plane.Raycast(ray, out var enter))
        {
            mousePosition = ray.GetPoint(enter);
            //print(mousePosition);

            smoothMousePostion = mousePosition;
            mousePosition.y = 0;
            mousePosition = Vector3Int.RoundToInt(mousePosition);
        }
        /*
        foreach(var node in nodes)
		{
            if(node.cellPosition == mousePosition && node.isPlaceable)
			{
                if (Input.GetMouseButtonUp(0)&&onMousePrefab!=null)
				{
                    node.isPlaceable = false;
                    onMousePrefab.GetComponent<Putobj>().isOnGrid = true;
                    onMousePrefab.position = node.cellPosition + new Vector3(0, 0.5f, 0);
                    onMousePrefab = null;
				}
			}
		}  */          
    }

    public void OnMouseClickOnUI()
	{
        if(onMousePrefab == null)
		{
            onMousePrefab = Instantiate(cube, mousePosition, Quaternion.identity);
		}
	}

    private void CreateGrid()
    {
        //定義棋盤格的大小
        nodes = new Node[width, height];
        var name = 0;

        
        for (int word_width = 0; word_width < width; word_width++)
        {
            for (int word_height = 0; word_height < height; word_height++)
			{
                //設定第n個Panel的位置
                Vector3 worldPosition = new Vector3(word_width, 0, word_height);
                //生成物件Instantiate(Prefab, 位置,角度);
                Transform obj = Instantiate(gridCellPrefab, worldPosition, Quaternion.identity);
                //設定obj名稱
                obj.name = "Cell" + name;
                nodes[word_width, word_height] = new Node(true, worldPosition, obj);
                name++;
			}
		}
    }
}

public class Node
{
    public bool isPlaceable;
    public Vector3 cellPosition;
    public Transform obj;

    public Node (bool isPlaceable, Vector3 cellPosition, Transform obj)
	{
        this.isPlaceable = isPlaceable;
        this.cellPosition = cellPosition;
        this.obj = obj;
	}
}
