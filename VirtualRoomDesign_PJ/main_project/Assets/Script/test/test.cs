using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    //�n�\�񪺪�������
    public Transform gridCellPrefab;
    public Transform cube;
    public Transform onMousePrefab;
    //�w�q�ѽL�檺�j�p
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

        ////�V�W�� //�i�H�����ϥ�Vector���ݩ�Vector3.up�A�N���ݭnnew�@���ܼ�
        //transform.Position = new Vector3(0, 0, 0);  //�H�@�ɬ����ߪ��y��
        plane = new Plane(Vector3.up, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePositionOnGrid();
    }


    void GetMousePositionOnGrid()
	{
        //���Camera�Ӯg�쪺�ƹ�����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //����Ƴ]�menter���u�g�u�P�����ۥ檺�Z���C�p�G�g�u����󥭭��A��ƪ�^false�ó]�menter���s�C
        //�p�G�g�u���V�P�����ۤϪ���V�A�h��ƪ�^false/ �ó]�menter���u�g�u���Z���]�t�ȡ^�C
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
        //�w�q�ѽL�檺�j�p
        nodes = new Node[width, height];
        var name = 0;

        
        for (int word_width = 0; word_width < width; word_width++)
        {
            for (int word_height = 0; word_height < height; word_height++)
			{
                //�]�w��n��Panel����m
                Vector3 worldPosition = new Vector3(word_width, 0, word_height);
                //�ͦ�����Instantiate(Prefab, ��m,����);
                Transform obj = Instantiate(gridCellPrefab, worldPosition, Quaternion.identity);
                //�]�wobj�W��
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
