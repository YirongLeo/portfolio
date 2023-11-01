using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //設定可控變數分別為重力、跳躍高度、二段跳高度、角色移動速度
    [SerializeField] private float gravity = 30f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float doubleJumpForce = 0f;
    [SerializeField] private float defaultMoveSpeed = 23f;

    //儲存想要應用的移動量
    private Vector3 motionStep;
    //設定二段跳一開始為停用
    private bool jumpedTwice = false;
    //速度
    private float velocity = 0f;
    //當前速度
    private float currentSpeed = 0f;
    //宣告一個變數CharacterController，控制角色
    private CharacterController controller;

    //設定為true時，通過設備移動玩家角色，設定為false則不能
    private bool CanMove { get; set; } = true;

    void Awake()
    {
        //只傳controller的值出來，不必在這裡定義類型，因為它會自動獲取與CharacterController關聯的類型
        TryGetComponent(out controller);
    }

    // Start is called before the first frame update
    void Start()
    {
        //設定當前速度為可控的角色移動速度
        currentSpeed = defaultMoveSpeed;
    }

    //一偵更新不止一次
    private void FixedUpdate()
    {   
        //檢查移動角色是否執行
        if(CanMove == true)
        {
            //檢查玩家是否在地上
            if (controller.isGrounded == true)
            {
                //如果玩家在地上，計算重力(使角色保持在一致的水平)
                velocity = -gravity * Time.deltaTime;
            }
            else
            {
                //如果玩家不在地上，從速度中減去一小部分
                velocity -= gravity * Time.deltaTime;
            }
        }
    }

    //一偵更新一次
    void Update()
    {
        //檢查移動角色是否執行
        if(CanMove == true)
        {
            //檢查玩家是否在地上
            if(controller.isGrounded == true)
            {
                //檢查二段跳狀態(在地上時二段跳應該為false)
                if(jumpedTwice == true)
                {
                    //將二段跳變更為停止
                    jumpedTwice = false;
                }

                //檢查是否按下空白鍵
                if(Input.GetButtonDown("Jump") == true)
                {
                    //如果按下空白鍵將速度改為可控的跳躍力
                    velocity = jumpForce;
                }
            }

            //玩家目前在空間，並檢查二段跳狀態
            else if (jumpedTwice == false)
            {
                //檢查是否按下空白鍵
                 if(Input.GetButtonDown("Jump") == true)
                {
                    //激活二段跳
                    jumpedTwice = true;
                    //如果按下空白鍵將速度改為可控的二段跳躍力
                    velocity = doubleJumpForce;
                }
            }
            //角色移動
            ApplyMovement();
        }
    }

    //角色移動
    private void ApplyMovement()
    {
        //將xyz軸都重製為0
        motionStep = Vector3.zero;
        //Input.GetAxisRaw套件用來記錄wasd的移動，Horizontal為紀錄as，Vertical為紀錄wd
        motionStep += transform.forward * Input.GetAxisRaw("Vertical");
        motionStep += transform.right * Input.GetAxisRaw("Horizontal");
        //計算角色當前移動的速度
        motionStep = currentSpeed * motionStep.normalized;
        motionStep.y += velocity;
        controller.Move(motionStep * Time.deltaTime);
    }
}
