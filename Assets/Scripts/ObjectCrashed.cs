using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCrashed : MonoBehaviour
{
    [SerializeField] CarStats playerStatsSc;

    void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.CompareTag("Player") && playerStatsSc.speed > 60 && playerStatsSc.canCrash)
        {
            playerStatsSc.health--;
            StartCoroutine(playerStatsSc.ChangeHealthBar());
            // it needs to be coroutine because car cannot have more than 1 damage in "1 second"
            // so it doesn't take more than 1 dmg from car on crash
        }
    }
}
