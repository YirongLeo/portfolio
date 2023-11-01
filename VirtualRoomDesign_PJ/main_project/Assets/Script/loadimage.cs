/*
    這裡主要是按鈕讀取圖片使用
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using B83.Image.BMP;

public class loadimage : MonoBehaviour
{
    string path;
    public RawImage image;
    public static List<string> object_list;
    public void on__open(){
        path = EditorUtility.OpenFilePanel("123","","png,jpg,bmp");
        GetImage();
    }
    void GetImage(){
        if(path != null){
            updateImage();
        }
    }
    void updateImage(){
        Texture2D BMP_image = LoadTexture(path);
        GameObject gameObject = GameObject.Find("map_create");
        gameObject.GetComponent<ReadImg>().set_pictor(BMP_image);
        image.texture = BMP_image;
    }
    public void check_update()
    {
        GameObject gameObject = GameObject.Find("map_create");
        gameObject.GetComponent<ReadImg>().creat_map();
        gameObject.SetActive(true);
        gameObject = GameObject.Find("start_ui");
        gameObject.SetActive(false);
        gameObject = GameObject.Find("player");
        gameObject.GetComponent<PlayerController>().game_start = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public static Texture2D LoadTexture(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);

            BMPLoader bmpLoader = new BMPLoader();
            //bmpLoader.ForceAlphaReadWhenPossible = true; //Uncomment to read alpha too

            //Load the BMP data
            BMPImage bmpImg = bmpLoader.LoadBMP(fileData);

            //Convert the Color32 array into a Texture2D
            tex = bmpImg.ToTexture2D();
        }
        return tex;
    }
}
