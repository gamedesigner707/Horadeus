using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Entity {

    public Rigidbody rb;

    public override Type GetPoolObjectType() {
        return typeof(Fish);
    }

}
