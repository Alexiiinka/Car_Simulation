using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLightsControll : MonoBehaviour
{
    CarControllerrSimple carContrSc;
    [SerializeField] Material reverseM, brakeM;
    [SerializeField] ParticleSystem smokePs;
    // Start is called before the first frame update
    void Start()
    {
        carContrSc = gameObject.GetComponent<CarControllerrSimple>();
    }

    // Update is called once per frame
    void Update()
    {
        if (carContrSc.speedOnKmh < 0 && Input.GetKey(KeyCode.DownArrow))
        {
            brakeM.DisableKeyword("_EMISSION");
            reverseM.EnableKeyword("_EMISSION");
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            brakeM.EnableKeyword("_EMISSION");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            reverseM.DisableKeyword("_EMISSION");
            brakeM.DisableKeyword("_EMISSION");
        }
        if (Input.GetKeyDown(KeyCode.Space) && carContrSc.speedOnKmh > 15)
        {
            brakeM.EnableKeyword("_EMISSION");
            smokePs.Play();
        }
        else if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.UpArrow))
        {
            brakeM.EnableKeyword("_EMISSION");
            smokePs.Play();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            brakeM.DisableKeyword("_EMISSION");
        }
    }
}
