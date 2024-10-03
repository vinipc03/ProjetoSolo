using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public float life;
    [SerializeField] public float maxLife;

    public Transform skin;
    public Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        maxLife = life;
    }

    // Update is called once per frame
    void Update()
    {
        LifeControl();
        Death();
    }

    public void Death()
    {
        if (life <= 0)
        {
            skin.GetComponent<Animator>().Play("Die", -1);
        }
    }

    public void PlayerDamage(int value)
    {
        life = life - value;
        skin.GetComponent<Animator>().Play("TakeHit", 1);
        cam.GetComponent<Animator>().Play("CameraTakeHit", -1);

    }

    public void LifeControl()
    {
        if(life < 1)
        {
            life = 0;
        }

        if(life > maxLife)
        {
            life = maxLife;
        }
    }
}
