using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool game_start = false;

    //設定為true時，通過設備移動玩家角色，設定為false則不能
    private bool CanMove { get; set; } = true;
    //切換上帝視角或是普通視角
    private bool switch_camera { get; set; } = false;
    //設定鏡頭的目標物
    private Vector3 target_position { get; set; }
    private Vector3 target_rotation { get; set; }


    //紀錄第一人稱視角相關設定
    private Vector3 First_view_position { get; set; }
    private Vector3 First_view_rotation { get; set; }

    //移動的速度
    public float normal_speed = 1;
    public float runing_speed = 3;
    //切換相機視角的速度
    public float camera_speed = 3;


    public float sensX;
    public float sensY;

    public float yRotation;
    public float xRotation;


    public GameObject player_object;
    public GameObject camera_object;
    public GameObject Build_panel;
   
   
    void Start()
    {
    }
    void Update()
    {
        if (game_start)
        {
            float working_speed = 0;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                working_speed = runing_speed;
            }
            else
            {
                working_speed = normal_speed;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                    CanMove = !CanMove;
                    if (CanMove)
                    {
                        switch_camera = true;
                        target_position = First_view_position;
                        target_rotation = First_view_rotation;
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                        Build_panel.SetActive(false);

                    }
                    else
                    {
                        switch_camera = true;
                        //取得地圖中心點
                        GameObject gameObject = GameObject.Find("Groud_panel");
                        Vector3 pos = gameObject.transform.position + new Vector3(0, 30, 0);

                        //鏡頭向下看
                        target_position = pos;
                        target_rotation = new Vector3(90, 0, 0);
                        //保存使用者位置
                        First_view_position = player_object.transform.position;

                        First_view_rotation = new Vector3(camera_object.transform.rotation.eulerAngles.x, camera_object.transform.rotation.eulerAngles.y, camera_object.transform.rotation.eulerAngles.z);
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                        Build_panel.SetActive(true);
                    }
            }
            //檢查移動角色是否執行
            if (switch_camera == false && CanMove == true)
            {
                float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensX;
                float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensX;
                yRotation += mouseX;
                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);
                //角色移動
                Vector3 move = camera_object.transform.forward * Input.GetAxis("Vertical") + camera_object.transform.right * Input.GetAxis("Horizontal");

                player_object.transform.Translate(new Vector3((move.x * working_speed * Time.deltaTime), 0, (move.z * working_speed * Time.deltaTime)));
                camera_object.transform.rotation = Quaternion.Euler(new Vector3(xRotation, yRotation, 0));
            }
            if (switch_camera)
            {
                player_object.transform.position = Vector3.Lerp(player_object.transform.position, target_position, camera_speed * Time.deltaTime);
                camera_object.transform.rotation = Quaternion.Euler(target_rotation);
                if (player_object.transform.position.x.ToString("0.0") == target_position.x.ToString("0.0") &&
                    player_object.transform.position.y.ToString("0.0") == target_position.y.ToString("0.0") &&
                    player_object.transform.position.z.ToString("0.0") == target_position.z.ToString("0.0")
                    )
                {
                    switch_camera = false;
                }
            }
            //switch to 上帝視角

        }
    }
}
