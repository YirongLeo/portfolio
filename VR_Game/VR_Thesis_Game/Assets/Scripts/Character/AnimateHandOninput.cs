using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOninput : MonoBehaviour
{

    //InputActionProperty(�]�w��J�ʧ@�ݩ�)
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;

    //Animator(�]�w�ʵe)
    public Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //���s���U���ȡA�Hfloat��ܡA�V����1�N����s�����T�׶V�j
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        //�]�w���s���U���ƭȡA�ʵe�]�w��(Hand Model����Trigger)
        handAnimator.SetFloat("Trigger", triggerValue);

        //�]�w�ʵe��Grip
        float gripValue = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);

    }
}
