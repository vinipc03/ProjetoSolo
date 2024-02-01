using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 vel;

    public Transform floorCollider;
    public LayerMask floorLayer;
    public Transform skin;

    

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
        
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            skin.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            skin.GetComponent<Animator>().SetBool("PlayerRun", true);
        }
        else
        {
            skin.GetComponent<Animator>().SetBool("PlayerRun", false);
        }
    }

    void Jump() //PULO 
    {
        bool canJump = Physics2D.OverlapCircle(floorCollider.position, 0.3f, floorLayer);
        if (canJump && Input.GetButtonDown("Jump")) // && comboTime > 0.5f
        {
            skin.GetComponent<Animator>().Play("PlayerJump", -1);
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, 600));
        }
    }

    #endregion
}
