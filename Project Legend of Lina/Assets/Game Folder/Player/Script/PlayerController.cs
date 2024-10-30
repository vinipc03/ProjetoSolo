using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    Rigidbody2D rb;
    public Transform skin;

    [Header("Movement")]
    Vector2 vel;
    public float speed;
    private float moveInput;
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;
    public bool isJumping;
    public Transform floorCollider;
    public Transform plataformCollider;
    public LayerMask floorLayer;
    private float dashTime;
    public bool isTalking;
    
    [Header("Combat")]
    public Image lifeBar;
    public int comboNum;
    float comboTime;
    public bool onAttack;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTalking)
        {
            Jump();
        }
        if (!onAttack)
        {
            Movement();
        }
        Attack();
        Dash();
        LifeBarControl();
        Death();

        //skin.GetComponent<Animator>().SetFloat("yVelocity", rb.velocity.y);
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
        vel = new Vector2(moveInput * speed, rb.velocity.y);
        moveInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetAxisRaw("Horizontal") != 0)
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
        bool canJump = Physics2D.OverlapCircle(floorCollider.position, 0.23f, floorLayer);
        //bool canJump2 = Physics2D.OverlapCircle(plataformCollider.position, 0.3f, floorLayer);
        if (canJump && Input.GetButtonDown("Jump")) // && comboTime > 0.5f canJump2 && Input.GetButtonDown("Jump")
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            onAttack = false;
            skin.GetComponent<Animator>().Play("PlayerJump", -1);
            //skin.GetComponent<Animator>().Play("PlayerJump1", -1);
            skin.GetComponent<Animator>().SetBool("Jump", true);

            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetButton("Jump"))
        {
            if(jumpTimeCounter > 0 && isJumping == true)
            {
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false; 
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y < 0)
        {
            isJumping = false;
            //skin.GetComponent<Animator>().Play("PlayerFall", -1);
            skin.GetComponent<Animator>().SetBool("Jump", false);
        }
    }
    

    void Dash() //DASH
    {
        dashTime = dashTime + Time.deltaTime;
        if (Input.GetButtonDown("Fire2") && dashTime > 1)
        {
            dashTime = 0;
            onAttack = false;
            skin.GetComponent<Animator>().Play("PlayerDash", -1);
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(skin.localScale.x * 250, 0));
            rb.gravityScale = 0;
            Invoke("RestoreGravityScale", 0.25f);
        }
    }

    void RestoreGravityScale()
    {
        rb.gravityScale = 3;
    } //RESTAURA GRAVIDADE

    #endregion

    #region Attacks
    void Attack() //ATAQUES DO PERSONAGEM 
    {
        // ATAQUE NO AR
        bool canJump = Physics2D.OverlapCircle(floorCollider.position, 0.3f, floorLayer);
        comboTime = comboTime + Time.deltaTime;
        if(canJump == false && Input.GetButtonDown("Fire1"))
        {
            comboNum++;
            if (comboNum > 1)
            {
                comboNum = 1;
            }
            comboTime = 0;
            //rb.velocity = Vector2.zero;
            //rb.AddForce(new Vector2(0, 200)); 
            skin.GetComponent<Animator>().Play("PlayerJumpAttack", -1);
            FinishAttack();
        }
        else
        {
            // ATAQUE NO CHÃO
            if (Input.GetButtonDown("Fire1") && comboTime > 0.2f && !onAttack)
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

    public void FinishAttack()
    {
        vel = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);
        onAttack = false;
    }  //PERSONAGEM VOLTA A SE MOVER QUANDO TERMINA ATAQUE
    #endregion

    #region HealthControls
    void Death() //MORTE 
    {
        if (GetComponent<Character>().life <= 0)
        {
            this.enabled = false;
            rb.simulated = false;
        }
    }

    void LifeBarControl() //CONTROLE BARRA DE VIDA
    {
        lifeBar.fillAmount = GetComponent<Character>().life / GetComponent<Character>().maxLife;
    }


    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(floorCollider.position, 0.23f);
    }
}
