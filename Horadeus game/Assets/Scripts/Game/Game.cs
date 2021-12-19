using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public PlayerController player;

    public Map map;

    private void InternalStart() {
        player.Init();
        map.Init();
    }

    private void InternalUpdate() {
        player.InternalUpdate();
        map.InternalUpdate();
    }

    private void Start() {
        InternalStart();
    }

    private void Update() {
        InternalUpdate();
    }

}
