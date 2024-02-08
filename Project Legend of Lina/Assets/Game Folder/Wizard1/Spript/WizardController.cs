using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour
{
    public float visionRadius = 5f; // Raio de visão do inimigo
    public GameObject fireball;
    public Transform firePoint;
    private float timer;

    public Transform skin; 
    private Transform target;
    // Calcula a direção para o jogador
    public float posX;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        AggroVision();
        UpdateRotation();
        Death();
    }

    public void Fireball()
    {
        Instantiate(fireball, firePoint.position, Quaternion.identity);
    }

    void AggroVision()
    {
        if (Vector3.Distance(transform.position, target.position) < visionRadius)
        {
            skin.GetComponent<Animator>().Play("WizardAttack");
        }
    }
    
void UpdateRotation()
    {
        posX = target.transform.position.x - transform.position.x;
        // Define a rotação para apontar para o eixo X ou Y, dependendo da direção
        if (posX > 0)
        {
            // aponta para direita
            skin.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            // aponta para esquerda
            skin.localScale = new Vector3(1, 1, 1);
        }
    }

    void Death()
    {
        if (GetComponent<Character>().life <= 0)
        {
            //AggroVision().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            this.enabled = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, visionRadius);

    }
}
