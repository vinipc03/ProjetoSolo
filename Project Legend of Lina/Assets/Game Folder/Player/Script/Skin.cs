using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
    public Transform player;

    public void FinishAttack()
    {
        player.GetComponent<PlayerController>().FinishAttack();
    }
}
