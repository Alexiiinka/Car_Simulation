using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarStats : MonoBehaviour
{
    public int health = 3;
    public float speed = 0.0f;
    [SerializeField] Image hp1, hp2, hp3;
    [SerializeField] Color orange, red;
    public bool canCrash = true;
    [SerializeField] AudioSource crashSource;
    [SerializeField] AudioClip crashClip;

    public IEnumerator ChangeHealthBar()
    {
        crashSource.PlayOneShot(crashClip);
        canCrash = false;
        switch (health)
        {
            case 2:
                hp1.enabled = false;
                hp2.color = orange;
                hp3.color = orange;
                break;
            case 1:
                hp2.enabled = false;
                hp3.color = red;
                break;
            case 0:
                hp3.enabled = false;
                break;
        }
        yield return new WaitForSeconds(1);
        canCrash = true;
    }
}
