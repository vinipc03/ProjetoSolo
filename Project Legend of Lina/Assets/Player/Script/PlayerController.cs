using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 vel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        vel = new Vector2(Input.GetAxisRaw("Horizontal"), 0);


    }

    private void FixedUpdate()
    {
        rb.velocity = vel;
    }
}
