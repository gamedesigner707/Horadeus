using MicroCrew.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Character_movement movement;

    public Transform shootPoint;
    public float shootForce = 300f;

    public void Init() {
        SwitchCursorLock();
    }

    public void InternalUpdate() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            SwitchCursorLock();
        }

        if(Input.GetMouseButtonDown(0)) {
            Arrow arrow = MPool.Get<Arrow>();
            arrow.transform.position = shootPoint.position;
            arrow.transform.forward = movement.cam.forward;
            arrow.Shoot(movement.cam.forward * shootForce);
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
