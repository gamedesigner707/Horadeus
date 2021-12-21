using MicroCrew.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour {

    public SO_PlayerInventory inventory;

    public PlayerMovement movement;

    public Transform shootPoint;

    public float shootForce = 1000f;

    private ItemData arrowItem;

    private HCamera playerCamera;

    public void Init(HCamera cam) {
        Debug.Log("Init player");

        playerCamera = cam;

        SwitchCursorLock();

        arrowItem = inventory.GetItem(ItemType.Arrow);

        movement.Init(cam);
    }

    public void InternalUpdate() {
        movement.InternalUpdate();

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SwitchCursorLock();
        }

        SomeCursorCheck();

        if (Input.GetMouseButtonDown(0) & arrowItem.count > 0)
        {
            ShootArrow();
        }
    }

    private void ShootArrow()
    {
        // Coordinates for the center of the game window
        float mid_x = Screen.width / 2;
        float mid_y = Screen.height / 2;

        //Makes a Ray pointing out towards the middle of the screen
        Ray ray = playerCamera.cameraComponent.ScreenPointToRay(new Vector3(mid_x, mid_y, 0));
        RaycastHit hit;
        Vector3 destination;

        // Detects if the ray hits an object, then sets where the arrow should hit
        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        } else
        {
            destination = playerCamera.transform.position + ray.direction * 10;
        }

        // Fires the arrow towards the destination
        Vector3 shootDirection = destination - shootPoint.position;
        shootDirection.Normalize();

        Arrow arrow = MPool.Get<Arrow>();
        arrow.transform.position = shootPoint.position;
        arrow.transform.forward = shootDirection;
        arrow.Shoot(arrow.transform.forward * shootForce);

        inventory.TakeItem(arrowItem, 1);
    }

    private void SomeCursorCheck()
    {
        // Gets the cursor position relative to the game window
        Vector2 cursorPosition = playerCamera.cameraComponent.ScreenToViewportPoint(Input.mousePosition);
        // Scales the position properly
        cursorPosition.x *= playerCamera.cameraComponent.scaledPixelWidth;
        cursorPosition.y *= playerCamera.cameraComponent.scaledPixelHeight;

        // Makes cursor invisible only when it's within the game window
        //if (movement.isInWindow(cursorPosition))
        //{
        //    Cursor.visible = false;
        //} else
        //{
        //    Cursor.visible = true;
        //} //This dose not help during building up the game it just keeps unlocking the cursor fromt he game.
    }

    public void InternalFixedUpdate()
    {
        movement.InternalFixedUpdate();
    }

    private void SwitchCursorLock() {
        if (Cursor.lockState == CursorLockMode.Locked) {
            Cursor.lockState = CursorLockMode.None;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

}
