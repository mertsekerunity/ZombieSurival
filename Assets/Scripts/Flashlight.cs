using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] float lightDecay = 0.1f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float minAngle = 10f;

    Light myLight;


    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        myLight.spotAngle = myLight.innerSpotAngle + 10f;
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseLightAngle();
        DecreaseLightIntensity();
    }

    void DecreaseLightAngle()
    {
        if (myLight.spotAngle <= minAngle)
        {
            return;
        }

        else
        {
            myLight.innerSpotAngle -= angleDecay * Time.deltaTime;
            myLight.spotAngle = myLight.innerSpotAngle + 10f;
        }
    }

    void DecreaseLightIntensity()
    {
        myLight.intensity -= lightDecay * Time.deltaTime;
    }
        public void RestoreLightAngle(float restoreAngle)
    {
        myLight.spotAngle = restoreAngle;
        myLight.innerSpotAngle = myLight.innerSpotAngle + 10f;
    }

    public void AddLightIntensity(float intensityAmount)
    {
        myLight.intensity += intensityAmount;
    }
}
