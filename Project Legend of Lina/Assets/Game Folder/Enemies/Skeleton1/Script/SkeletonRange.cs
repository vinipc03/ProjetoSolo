using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRange : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    //private Transform target;
    public Transform player;
    public Transform skeletonController;
    public GameObject skeleton;


    private void Start()
    {
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //float posX = collision.transform.position.x;
        if (collision.CompareTag("Player"))
        {
            transform.parent.GetComponent<Animator>().Play("SkeletonAttack", -1);

        }
    }

    private void Attack()
    {
        transform.parent.GetComponent<Animator>().Play("SkeletonAttack", -1);
    }
}
