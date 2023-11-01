using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadImg : MonoBehaviour
{

    public Material met;
    public Transform Parent;

    public Texture2D image;

    public GameObject wallObject;
    public GameObject groundObject;


    // Start is called before the first frame update
    void Start()
    {
        creat_map();
    }
    public void set_pictor(Texture2D in_put)
    {
        this.GetComponent<ReadImg>().image = in_put;
    }
    public void creat_map()
    {
        print("creat");
        int step = 1;
        int worldX = image.width;
        int worldZ = image.height;
        print(worldX);
        Vector3 startingSpawnPosition = new Vector3(0, 1.5f, 0);
        Vector3 currentSpawnPos = startingSpawnPosition;
        int count = 0;
        for (int z = 0; z < worldZ; z += step)
        {
            for (int x = 0; x < worldX; x += step)
            {
                if (image.GetPixel(x, z).Equals(Color.black))
                {
                    GameObject InstantiatedGameObject = Instantiate(wallObject, currentSpawnPos, Quaternion.identity);
                    InstantiatedGameObject.transform.SetParent(Parent);
                    count++;
                }
                currentSpawnPos.x += 0.05f;
            }

            currentSpawnPos.x = startingSpawnPosition.x;
            currentSpawnPos.z += 0.05f;
        }
        Debug.Log(count);
        GameObject duplicate = Instantiate(groundObject);
        Debug.Log(currentSpawnPos.x); Debug.Log(currentSpawnPos.z);
        Debug.Log((worldX / 2) * 0.05f); Debug.Log((worldZ / step) * 0.05f);
        duplicate.transform.position = new Vector3(startingSpawnPosition.x, 0, startingSpawnPosition.z) + new Vector3((worldX / (step * 2)) * 0.05f, 0, (worldZ / (step * 2)) * 0.05f);
        duplicate.transform.localScale = new Vector3((worldX / step) * 0.05f / 10, 1, (worldZ / step) * 0.05f / 10);
        duplicate.name = "Groud_panel";
        duplicate.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2((worldX / step) * 0.05f, (worldZ / step) * 0.05f);
    }

}
