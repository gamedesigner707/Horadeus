using MicroCrew.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour {

    public Character_movement movement;

    public Transform shootPoint;
    public float shootForce = 1000f;
    public TMP_Text arrowCounterUI;
    private int arrowCount = 30;

    public void Init() {
        SwitchCursorLock();
        arrowCounterUI.text = "Arrows: " + arrowCount.ToString();
    }

    public void InternalUpdate() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SwitchCursorLock();
        }

        // Gets the cursor position relative to the game window
        Vector2 cursorPosition = movement.camera.ScreenToViewportPoint(Input.mousePosition);
        // Scales the position properly
        cursorPosition.x *= movement.camera.scaledPixelWidth;
        cursorPosition.y *= movement.camera.scaledPixelHeight;

        // Makes cursor invisible only when it's within the game window
        if (movement.isInWindow(cursorPosition))
        {
            Cursor.visible = false;
        } else
        {
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(0) & arrowCount > 0) {

            // Coordinates for the center of the game window
            float mid_x = Screen.width / 2;
            float mid_y = Screen.height / 2;

            //Makes a Ray pointing out towards the middle of the screen
            Ray ray = movement.camera.ScreenPointToRay(new Vector3(mid_x, mid_y, 0));
            RaycastHit hit;
            Vector3 destination;

            // Detects if the ray hits an object, then sets where the arrow should hit
            if (Physics.Raycast(ray, out hit))
            {
                destination = hit.point;
            } else
            {
                destination = movement.camera.transform.position + ray.direction * 10;
            }

            // Fires the arrow towards the destination
            Vector3 shootDirection = destination - shootPoint.position;
            shootDirection.Normalize();

            Arrow arrow = MPool.Get<Arrow>();
            arrow.transform.position = shootPoint.position;
            arrow.transform.forward = shootDirection;
            arrow.Shoot(arrow.transform.forward * shootForce);

            arrowCount--;
            arrowCounterUI.text = "Arrows: " + arrowCount.ToString();
        }
    }

    private void SwitchCursorLock() {
        if (Cursor.lockState == CursorLockMode.Locked) {
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

}
