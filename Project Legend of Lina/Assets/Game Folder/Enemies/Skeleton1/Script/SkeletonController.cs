﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    public Transform a;
    public Transform b;
    public float patrolSpeed;
    public float chaseSpeed;

    public Transform skin;
    public Transform skeletonRange;
    public GameObject skeleton;

    public bool goRight;

    public bool activeMovement;

    public float visionRadius = 5f; // Raio de visão do inimigo
    private Transform target;

    // Calcula a direção para o jogador
    public float posX; 

    private enum State { Patrol, Chase };
    private State atualState = State.Patrol;

    // Start is called before the first frame update
    void Start()
    {
        activeMovement = true;
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
        /*switch (atualState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                UpdateRotation();
                break;
        }
        */
        if(GetComponent<Character>().life >= 0)
        {
            Patrol();
        }
        Death();
    }

    void Patrol()
    {
        // Lógica de patrulha entre os pontos A e B
        if (goRight == true)
        {
            skin.localScale = new Vector3(1, 1, 1);

            if (Vector2.Distance(transform.position, b.position) < 0.3f)
            {
                goRight = false;
            }
            transform.position = Vector2.MoveTowards(transform.position, b.position, patrolSpeed * Time.deltaTime);
        }
        else
        {
            skin.localScale = new Vector3(-1, 1, 1);
            if (Vector2.Distance(transform.position, a.position) < 0.3f)
            {
                goRight = true;
            }
            transform.position = Vector2.MoveTowards(transform.position, a.position, patrolSpeed * Time.deltaTime);
        }

        // Verifica se o jogador está dentro do raio de visão
        if (Vector3.Distance(transform.position, target.position) < visionRadius)
        {
            atualState = State.Chase;
        }
    }

    void Chase()
    {
        // Lógica de perseguição ao jogador
        transform.position = Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
        skin.GetComponent<Animator>().Play("SkeletonRun", -1);
        // Verifica se o jogador saiu do raio de visão
        if (Vector3.Distance(transform.position, target.position) > visionRadius)
        {
            transform.eulerAngles = new Vector2(0, 0);
            atualState = State.Patrol;
        }
    }

    #region Movements
    public void Movement()
    {
        if (goRight == true)
        {
            skin.localScale = new Vector3(1, 1, 1);

            if (Vector2.Distance(transform.position, b.position) < 0.3f)
            {
                goRight = false;
            }
            transform.position = Vector2.MoveTowards(transform.position, b.position, 1f * Time.deltaTime);
        }
        else
        {
            skin.localScale = new Vector3(-1, 1, 1);
            if (Vector2.Distance(transform.position, a.position) < 0.3f)
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
            skeleton.GetComponent<CircleCollider2D>().enabled = false;
            this.enabled = false;
            //Invoke("Revive", 5f);
        }
    }

    public void Revive()
    {
        skeletonRange.GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = true;
        this.enabled = true;
        skin.GetComponent<Animator>().Play("SkeletonRevive", -1); //SetBool("isReviving", true);
        skeleton.GetComponent<Character>().life = skeleton.GetComponent<Character>().maxLife;
    }

    void UpdateRotation()
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
        Gizmos.DrawWireSphere(transform.position, visionRadius);

    }

}
