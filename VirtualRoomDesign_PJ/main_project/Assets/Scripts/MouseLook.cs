using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 1000f;

    public Transform playerBody;

    float xRotation = 0f;

    //�]�w��true�ɡA�i�H�z�L���ʷƹ��Ӳ��ʬ۾��A�]�w��false�h����
    public bool LockEnabled { get; set; } = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //�T�O����ɥi�H�z�L���ʷƹ��Ӳ��ʬ۾�
        LockEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(LockEnabled ==true)
		{
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playerBody.Rotate(Vector3.up * mouseX);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && LockEnabled == true)
        {
            Debug.Log("esc key was pressed.");
            LockEnabled = false;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && LockEnabled == false)
		{
            Debug.Log("esc key was pressed.");
            LockEnabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }

	}
}
