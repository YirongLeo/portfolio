/*
 * 用來生成空間用的class
 * 已知問題：方塊大小的參數需要調整
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImagetoTerrain : MonoBehaviour
{

    public RawImage rawimage;
    public GameObject gameObject;
    public void on_click()
    {
        if (rawimage.texture != null)
        {
            make_mesh();
        }
    }


    void make_mesh()
    {
        Mesh mesh = new Mesh();
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();
        Texture2D image = rawimage.texture as Texture2D;

        Vector3 Pos = gameObject.transform.position;
        for (int w = 1; w < image.width - 1; w++)
        {
            for (int h = 1; h < image.height - 1; h++)
            {
                int numFaces = 0;
                if (image.GetPixel(w, h) == Color.black)
                {
                    Pos = new Vector3(w - 1, h - 1, 0);
                    //左側
                    if (image.GetPixel(w - 1, h) != Color.black)
                    {
                        verts.Add(Pos + new Vector3(0, 0, 1));
                        verts.Add(Pos + new Vector3(0, 1, 1));
                        verts.Add(Pos + new Vector3(0, 1, 0));
                        verts.Add(Pos + new Vector3(0, 0, 0));
                        numFaces++;

                    }
                    //右側
                    if (image.GetPixel(w + 1, h) != Color.black)
                    {
                        verts.Add(Pos + new Vector3(1, 0, 0));
                        verts.Add(Pos + new Vector3(1, 1, 0));
                        verts.Add(Pos + new Vector3(1, 1, 1));
                        verts.Add(Pos + new Vector3(1, 0, 1));
                        numFaces++;
                    }
                    //上方
                    if (image.GetPixel(w, h - 1) != Color.black)
                    {
                        verts.Add(Pos + new Vector3(1, 0, 1));
                        verts.Add(Pos + new Vector3(1, 1, 1));
                        verts.Add(Pos + new Vector3(0, 1, 1));
                        verts.Add(Pos + new Vector3(0, 0, 1));
                        numFaces++;
                    }
                    //前方
                    if (image.GetPixel(w, h + 1) != Color.black)
                    {
                        verts.Add(Pos + new Vector3(0, 0, 0));
                        verts.Add(Pos + new Vector3(0, 1, 0));
                        verts.Add(Pos + new Vector3(1, 1, 0));
                        verts.Add(Pos + new Vector3(1, 0, 0));
                        numFaces++;
                    }
                    int tl = verts.Count - 4 * numFaces;
                    for (int i = 0; i < numFaces; i++)
                    {
                        tris.AddRange(new int[] {
                            0,1,2,0,2,3});
                    }
                }
            }
        }
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.RecalculateNormals();
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}