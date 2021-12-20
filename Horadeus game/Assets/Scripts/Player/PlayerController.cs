using MicroCrew.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Character_movement movement;

    public Transform shootPoint;
    public float shootForce = 1000f;

    public void Init() {
        SwitchCursorLock();
    }

    public void InternalUpdate() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            SwitchCursorLock();
        }

        if(Input.GetMouseButtonDown(0)) {
            Camera camera = gameObject.GetComponentInChildren<Camera>();

            float x = Screen.width / 2;
            float y = Screen.height / 2;

            Ray ray = camera.ScreenPointToRay(new Vector3(x, y, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 destination = hit.point;

                Vector3 shootDirection = destination - shootPoint.position;
                shootDirection.Normalize();

                Arrow arrow = MPool.Get<Arrow>();
                arrow.transform.position = shootPoint.position;
                arrow.transform.forward = shootDirection;
                arrow.Shoot(arrow.transform.forward * shootForce);
            }
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
