using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{

    public Transform player;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if(player.GetComponent<PlayerController>().comboNum == 1)
            {
                collision.GetComponent<Character>().life--;
                collision.GetComponentInChildren<Animator>().Play("TakeHit", 1);
            }

            if (player.GetComponent<PlayerController>().comboNum == 2)
            {
                collision.GetComponent<Character>().life--;
                collision.GetComponentInChildren<Animator>().Play("TakeHit", 1);
            }

            if (player.GetComponent<PlayerController>().comboNum == 3)
            {
                collision.GetComponent<Character>().life -= 2;
                collision.GetComponentInChildren<Animator>().Play("TakeHit", 1);
            }

        }
    }
}
