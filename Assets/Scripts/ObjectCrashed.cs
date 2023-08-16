using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCrashed : MonoBehaviour
{
    [SerializeField] CarStats playerStatsSc;

    void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.CompareTag("Player") && playerStatsSc.speed > 40)
        {
            Debug.Log("hehe");
        }
    }
}
