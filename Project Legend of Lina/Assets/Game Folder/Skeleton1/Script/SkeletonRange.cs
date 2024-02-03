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
        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //float posX = collision.transform.position.x;
        if (collision.CompareTag("Player"))
        {
            transform.parent.GetComponent<Animator>().Play("SkeletonAttack", -1);
            
            /* skeletonController.GetComponent<SkeletonController>().DesactiveMovement();
            //skeletonController.GetComponentInChildren<Transform>().localScale = new Vector3(posX, 1, 1);



            if (Vector2.Distance(skeletonController.position, collision.GetComponent<Transform>().position) > stoppingDistance)
            {
                
                skeleton.transform.position = Vector2.MoveTowards(skeleton.transform.position, collision.GetComponent<Transform>().position, speed * Time.deltaTime);
                if(Vector2.Distance(skeleton.transform.position, collision.GetComponent<Transform>().position) < stoppingDistance)
                {
                    float posX = skeletonController.transform.position.x - collision.GetComponent<Transform>().position.x;

                    if (posX > collision.GetComponent<Transform>().position.x)
                    {
                        skeletonController.transform.eulerAngles = new Vector2(0, 180);
                    }
                    else
                    {
                        skeletonController.transform.eulerAngles = new Vector2(0, 0);
                    }
                    
                }
            } */

        }
        
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
            skeletonController.GetComponent<SkeletonController>().ActiveMovement();
            skeletonController.transform.eulerAngles = new Vector2(0, 0);
    }*/

}
