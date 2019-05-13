using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoCamera : MonoBehaviour
{

    public GameObject ArduinoConnector;

    void Start()
    {
        
    }


    void Update()
    {
        var arduino = ArduinoConnector.GetComponent<ArduinoConnector>();
        string value = arduino.ReadFromArduino(1); //Read the information
        if (arduino.ReadFromArduino(1) != "" || arduino.ReadFromArduino(1) != "null" )
            {
            string[] vec3 = value.Split(','); //My arduino script returns a 3 part value (IE: 12,30,18)

                if (vec3[0] != "" && vec3[1] != "" && vec3[2] != "" && vec3[3] != "") //Check if all values are recieved
                {
                    transform.rotation = Quaternion.Euler(float.Parse(vec3[0]), float.Parse(vec3[1]), 0);
                }
        }


        
    }
}
