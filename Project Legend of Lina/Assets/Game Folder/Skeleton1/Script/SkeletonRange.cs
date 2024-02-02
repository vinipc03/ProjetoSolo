using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRange : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            transform.parent.GetComponent<Animator>().Play("SkeletonAttack", -1);
        }
    }
}
