using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile {

    protected override void Interact(Collision c) {
        base.Interact(c);

        Fish fish = c.gameObject.GetComponent<Fish>();

        if(fish != null) {
            fish.GetComponent<Rigidbody>().useGravity = true;
            // Map.inst.DestroyFish(fish);
        }
    }

    public override Type GetPoolObjectType() {
        return typeof(Arrow);
    }

}
