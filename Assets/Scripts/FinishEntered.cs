using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishEntered : MonoBehaviour
{
    LevelManagerScript lvlSc;
    // Start is called before the first frame update

    void Start()
    {
        lvlSc = GameObject.Find("LevelManager").GetComponent<LevelManagerScript>();
    }
    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lvlSc.GameEndedFinish();
        }
    }
}
