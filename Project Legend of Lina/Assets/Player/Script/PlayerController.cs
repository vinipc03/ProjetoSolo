using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 vel;

    public Transform floorCollider;
    public LayerMask floorLayer;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

        Jump();
        Movement();

    }

    private void FixedUpdate()
    {
        rb.velocity = vel;
    }


    #region Movements

    void Movement() //MOVIMENTAÇÃO DIREITA E ESQUERDA 
    {
        vel = new Vector2(Input.GetAxisRaw("Horizontal") * 4, rb.velocity.y);
    }

    void Jump() //PULO 
    {
        bool canJump = Physics2D.OverlapCircle(floorCollider.position, 0.3f, floorLayer);
        if (canJump && Input.GetButtonDown("Jump")) // && comboTime > 0.5f
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, 600));
        }
    }

    #endregion
}
