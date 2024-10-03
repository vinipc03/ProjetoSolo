using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public Transform a;
    public Transform b;
    public Transform skin;
    public bool goRight;
    public float speed;
    private float initialSpeed;
    public GameObject zombie;

    private void Start()
    {
        initialSpeed = speed;
    }
    // Update is called once per frame
    void Update()
    {
        Patrol();
        Death();
    }

    void Patrol()
    {
        // Lógica de patrulha entre os pontos A e B
        if (goRight == true)
        {
            skin.localScale = new Vector3(-1, 1, 1);

            if (Vector2.Distance(transform.position, b.position) < 0.3f)
            {
                goRight = false;
            }
            transform.position = Vector2.MoveTowards(transform.position, b.position, speed * Time.deltaTime);
        }
        else
        {
            skin.localScale = new Vector3(1, 1, 1);
            if (Vector2.Distance(transform.position, a.position) < 0.3f)
            {
                goRight = true;
            }
            transform.position = Vector2.MoveTowards(transform.position, a.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Character>().PlayerDamage(1);
        }
    }

    void Death()
    {
        if (GetComponent<Character>().life <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            this.enabled = false;
            Invoke("Revive", 5f);
        }
    }

    public void Revive()
    {
        GetComponent<CapsuleCollider2D>().enabled = true;
        this.enabled = true;
        skin.GetComponent<Animator>().Play("ZombieRevive", -1); //SetBool("isReviving", true);
        zombie.GetComponent<Character>().life = zombie.GetComponent<Character>().maxLife;
    }
}
