using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlledCamera : MonoBehaviour
{
    public GameManager currentGameState;
    public Transform player;
    public float mouseSensitivity;
    [SerializeField] HudManager pauseScreen;
    float rotationX = 0f;
    bool followMouse;

    // Start is called before the first frame update
    void Start()
    {
        //Lock cursor to the center of screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentGameState!= null)
        {
            if (currentGameState.gameState == 1 && pauseScreen.isPaused == false)
            {
                followMouse = true;

                if(followMouse == true)
                {
                    //Get axises and multiply it by the mouseSensitivity
                    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
                    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

                    //Clamp x rotation
                    rotationX -= mouseY;
                    rotationX = Mathf.Clamp(rotationX, -90f, 90f);
                    //Set local rotation to rotationX;
                    transform.localRotation = Quaternion.Euler(new Vector3(rotationX, 0f, 0f));
                    player.transform.Rotate(new Vector3(0f, mouseX, 0f));
                }
              
            }

            if (currentGameState.gameState == 2 || pauseScreen.isPaused == true)
            {
                //Return cursor to normal during game over screen
                Cursor.lockState = CursorLockMode.None;
                followMouse = false;
            }

        }
       
       
    }
}
