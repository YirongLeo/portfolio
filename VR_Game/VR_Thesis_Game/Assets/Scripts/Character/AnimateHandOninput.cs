using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOninput : MonoBehaviour
{

    //InputActionProperty(設定輸入動作屬性)
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;

    //Animator(設定動畫)
    public Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //按鈕按下的值，以float表示，越接近1代表按鈕按壓幅度越大
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        //設定按鈕按下的數值，動畫設定為(Hand Model內的Trigger)
        handAnimator.SetFloat("Trigger", triggerValue);

        //設定動畫為Grip
        float gripValue = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);

    }
}
