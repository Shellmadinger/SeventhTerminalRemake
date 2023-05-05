using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlledCamera : MonoBehaviour
{
    
    public Transform player;

    public float mouseSensitivity;

    float rotationX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        transform.localRotation = Quaternion.Euler(new Vector3(rotationX, 0f, 0f));
        player.transform.Rotate(new Vector3(0f, mouseX, 0f));
    }
}
