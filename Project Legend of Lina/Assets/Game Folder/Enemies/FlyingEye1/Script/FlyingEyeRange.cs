using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeRange : MonoBehaviour
{

    public float speed;
    public float stoppingDistance;
    public Transform player;
    public Transform flyingEyeController;
    public GameObject flyingEye;


    private void Start()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.parent.GetComponent<Animator>().Play("Attack1", -1);

        }
    }
}
