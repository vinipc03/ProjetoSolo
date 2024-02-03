using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    public Transform a;
    public Transform b;
    Rigidbody2D rb;

    public Transform skin;
    public Transform skeletonRange;

    public bool goRight;

    public bool activeMovement;

    public float raioVisao = 5f; // Raio de visão do inimigo
    private Transform target;

    // Calcula a direção para o jogador
    public float posX; 

    private enum Estado { Patrulhando, Perseguindo };
    private Estado estadoAtual = Estado.Patrulhando;

    // Start is called before the first frame update
    void Start()
    {
        activeMovement = true;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (skin.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("SkeletonAttack"))
        {
            return;
        }

        posX = target.transform.position.x - transform.position.x;
        switch (estadoAtual)
        {
            case Estado.Patrulhando:
                Patrulhar();
                break;
            case Estado.Perseguindo:
                Perseguir();
                AtualizarRotacao();
                break;
        }

        /*if (activeMovement == true)
        {
            Movement();
        }  */
        
        Death();
    }

    void Patrulhar()
    {
        // Lógica de patrulha entre os pontos A e B
        // Por exemplo, use algum tipo de interpolação linear para suavizar o movimento entre os pontos.
        if (goRight == true)
        {
            skin.localScale = new Vector3(1, 1, 1);

            if (Vector2.Distance(transform.position, b.position) < 0.1f)
            {
                goRight = false;
            }
            transform.position = Vector2.MoveTowards(transform.position, b.position, 1f * Time.deltaTime);
        }
        else
        {
            skin.localScale = new Vector3(-1, 1, 1);
            if (Vector2.Distance(transform.position, a.position) < 0.1f)
            {
                goRight = true;
            }
            transform.position = Vector2.MoveTowards(transform.position, a.position, 1f * Time.deltaTime);
        }

        // Verifica se o jogador está dentro do raio de visão
        if (Vector3.Distance(transform.position, target.position) < raioVisao)
        {
            estadoAtual = Estado.Perseguindo;
        }
    }

    void Perseguir()
    {
        // Lógica de perseguição ao jogador
        transform.position = Vector3.MoveTowards(transform.position, target.position, 2 * Time.deltaTime);

        // Verifica se o jogador saiu do raio de visão
        if (Vector3.Distance(transform.position, target.position) > raioVisao)
        {
            transform.eulerAngles = new Vector2(0, 0);
            estadoAtual = Estado.Patrulhando;
        }
    }

    #region Movements
    public void Movement()
    {
        if (goRight == true)
        {
            skin.localScale = new Vector3(1, 1, 1);

            if (Vector2.Distance(transform.position, b.position) < 0.1f)
            {
                goRight = false;
            }
            transform.position = Vector2.MoveTowards(transform.position, b.position, 1f * Time.deltaTime);
        }
        else
        {
            skin.localScale = new Vector3(-1, 1, 1);
            if (Vector2.Distance(transform.position, a.position) < 0.1f)
            {
                goRight = true;
            }
            transform.position = Vector2.MoveTowards(transform.position, a.position, 1f * Time.deltaTime);
        }
    }

    public void DesactiveMovement()
    {
        activeMovement = false;
    }
    public void ActiveMovement()
    {
        activeMovement = true;
    }
    #endregion

    void Death() 
    {
        if (GetComponent<Character>().life <= 0)
        {
            skeletonRange.GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            rb.gravityScale = 0;
            this.enabled = false;
        }
    }

    void AtualizarRotacao()
    {
        // Define a rotação para apontar para o eixo X ou Y, dependendo da direção
        if (posX > 0)
        {
            // aponta para direita
            skin.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            // aponta para esquerda
            skin.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, raioVisao);

    }

}
