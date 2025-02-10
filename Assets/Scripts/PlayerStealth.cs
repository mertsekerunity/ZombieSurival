using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStealth : MonoBehaviour
{
    public bool isStealth = false;
    Material material;

    // Start is called before the first frame update
    void Start()
    {
        //material = GameObject.FindGameObjectWithTag("Capsule").GetComponent<Material>();
        material = GameObject.FindGameObjectWithTag("Capsule").GetComponent<Renderer>().material;
        //material = GameObject.FindWithTag("Capsule").GetComponent <Material>();
        //material = GameObject.Find("Capsule").GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        ActivateStealthMode();
        ChangeMaterial();
    }

    void ChangeMaterial()
    {
        if (isStealth)
        {
            material.color = Color.white;
        }
        else
        {
            material.color = Color.red;
        }
    }

    void ActivateStealthMode()
    {
        if (Input.GetKeyDown(KeyCode.V) && !isStealth)
        {
            isStealth = true;
        }
    }
}
