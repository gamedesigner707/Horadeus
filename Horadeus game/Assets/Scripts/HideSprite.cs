using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSprite : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        //hide the sprite
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
