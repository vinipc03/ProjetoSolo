﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float speed;
    public GameObject fireImpact;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        //Vector3 direction = new Vector3(transform.localScale.x, 0);
         
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, 0).normalized * speed;
        
        //ATIRA PARA QUALQUER DIREÇÃO
        //rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        
        //ROTACIONA PARA QUALQUER DIREÇÃO
        //float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;

        float rotation = Mathf.Atan2(0, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Character>().PlayerDamage(1);
            Instantiate(fireImpact, transform.position, transform.rotation);
        }
        //Destroy(fireImpact);
        Destroy(gameObject);
    }
}
