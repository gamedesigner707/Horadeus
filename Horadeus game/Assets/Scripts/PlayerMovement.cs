using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    public float m_Speed = 5f;
    public float faceCamSpeed = 0.1f;

    public Vector2 offsetInPlane;
    public float sensX, sensY;
    public float rotX, rotY;
    public float dst = 10;

    public Animator animator;

    private HCamera playerCamera;

    public void Init(HCamera cam)
    {
        this.playerCamera = cam;
    }

    private void Awake()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; //Player can't tip over   
    }

    public void InternalUpdate()
    {
        Look();
    }

    public void InternalFixedUpdate()
    {
        Move();
    }

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * sensY;

        rotY += mouseX * Time.deltaTime;
        rotX = Mathf.Clamp(rotX + mouseY * Time.deltaTime, -70, 70);

        Quaternion rot = Quaternion.Euler(0, rotY, 0) * Quaternion.Euler(rotX, 0, 0);
        Vector3 camPos = transform.position + rot * Vector3.forward * dst;

        playerCamera.cameraTransform.rotation = rot;
        playerCamera.cameraTransform.forward = -playerCamera.cameraTransform.forward;

        camPos += playerCamera.cameraTransform.right * offsetInPlane.x + playerCamera.cameraTransform.up * offsetInPlane.y;

        playerCamera.cameraTransform.position = camPos;
    }

    private bool CursorIsInsideGameWindow()
    {
        // Gets the cursor position relative to the game window
        Vector2 cursorPosition = playerCamera.cameraComponent.ScreenToViewportPoint(Input.mousePosition);

        // Scales the position properly
        cursorPosition.x *= playerCamera.cameraComponent.scaledPixelWidth;
        cursorPosition.y *= playerCamera.cameraComponent.scaledPixelHeight;

        return isInWindow(cursorPosition);
    }

    private void Move()
    {
        //make character smootly face camera
        Vector3 lookPos = (playerCamera.cameraTransform.position - transform.position) * -1; //-1 because want the back to face the cam. not the front
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, faceCamSpeed);

        Vector3 cam_orientation = new Vector3(playerCamera.cameraTransform.forward.x, 0, playerCamera.cameraTransform.forward.z); //get camera orientation
        Vector3 m_Input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); //Store user input as a input vector
        m_Input.Normalize();
        Vector3 m_Movement = cam_orientation * m_Input.z + transform.right * m_Input.x;
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Speed * Time.fixedDeltaTime); //move character


        animator.SetFloat("Foward/Backward", m_Input.z); //if > 0: foward, if < 0: backward
        animator.SetFloat("Left/Right", m_Input.x); //if > 0: right, if < 0: left
    }

    // Returns whether or not the given position is in the game window
    public bool isInWindow(Vector2 position)
    {
        return (position.x >= 0 & position.x <= Screen.width &
                position.y >= 0 & position.y <= Screen.height);
    }

}
