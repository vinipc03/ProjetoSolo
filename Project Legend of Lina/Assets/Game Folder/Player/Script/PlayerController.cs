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

    public int comboNum;
    float comboTime;
    private float dashTime;
    private bool onAttack;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        if (!onAttack)
        {
            Movement();
        }
        Attack();
        Dash();
        Death();

       
    }

    private void FixedUpdate()
    {
        if(dashTime > 0.5)
        {
            rb.velocity = vel;
        }
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

    void Dash() //DASH
    {
        dashTime = dashTime + Time.deltaTime;
        if (Input.GetButtonDown("Fire2") && dashTime > 1)
        {
            dashTime = 0;
            skin.GetComponent<Animator>().Play("PlayerDash", -1);
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(skin.localScale.x * 300, 0));
            rb.gravityScale = 0;
            Invoke("RestoreGravityScale", 0.3f);
        }

        
    }

    #endregion

    #region Attacks
    void Attack() //ATAQUES DO PERSONAGEM 
    {
        // ATAQUE NO AR
        bool canJump = Physics2D.OverlapCircle(floorCollider.position, 0.3f, floorLayer);
        comboTime = comboTime + Time.deltaTime;
        if(canJump == false && Input.GetButtonDown("Fire1") && comboTime > 0.5f)
        {
            comboNum++;
            if (comboNum > 1)
            {
                comboNum = 1;
            }
            comboTime = 0;
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, 200)); 
            comboTime = 0;
            skin.GetComponent<Animator>().Play("PlayerJumpAttack", -1);
        }
        else
        {
            // ATAQUE NO CHÃO
            if (Input.GetButtonDown("Fire1") && comboTime > 0.5f && !onAttack)
            {
                vel = new Vector2(Input.GetAxisRaw("Horizontal") * 0, rb.velocity.y);
                comboNum++;
                if (comboNum > 3)
                {
                    comboNum = 1;
                }
                comboTime = 0;
                skin.GetComponent<Animator>().Play("PlayerAttack" + comboNum, -1);
                onAttack = true;
            }
        }
       

        if (comboTime >= 1f)
        {
            comboNum = 0;
        }
    }
    #endregion

    void Death() //MORTE 
    {
        if (GetComponent<Character>().life <= 0)
        {
            this.enabled = false;
        }
    }

    void RestoreGravityScale()
    {
        rb.gravityScale = 3;
    }

    public void FinishAttack()
    {
        vel = new Vector2(Input.GetAxisRaw("Horizontal") * 4, rb.velocity.y);
        onAttack = false;
    }
}
