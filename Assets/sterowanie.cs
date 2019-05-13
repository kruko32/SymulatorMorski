using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;


public class sterowanie : MonoBehaviour
{
    public Slider predkoscNastawa;
    public Slider pletwaNastawa;
    public Text predkosc;
    public Text pletwa;
    public Text cog;
    public Text predkoscAktHUD;
    public Text wychylenieAktHUD;
    public Text COGAktHUD;
    public Text ROTAktHUD;

    public float silnik = 0.0f;
    public float pletwaAkt = 0.0f;
    public float predkoscAktualna = 0.0f;
    public float x = 2100.0f;
    public float z = 1900.0f;
    public const float predkoscMax = 21.0f;
    public const float wychylenieMax = 35.0f;
    public float ROT = 0.0f;
    public float COG = 0.0f;
    public float K = 0.07f;

    public GameObject ArduinoConnector;
    public GameObject Kamera;

    private float CamX;
    private float CamY;
    private Vector3 targetRotation;
    private Vector3 rotateValue;
    private Quaternion rotation;

    ArduinoConnector arduino;

    public void  Start()
    {
        rotation = transform.rotation;
        arduino = ArduinoConnector.GetComponent<ArduinoConnector>();
        StartCoroutine(arduino.example());
    }


    void Update()
    {
        



        string value = arduino.ReadFromArduino(1); //Read the information
        if (arduino.ReadFromArduino(1) != " " || arduino.ReadFromArduino(1) != null)
        {
            string[] vec3 = value.Split(','); //My arduino script returns a 3 part value (IE: 12,30,18)


            if (vec3[0] != "" && vec3[1] != "" && vec3[2] != "" && vec3[3] != "") //Check if all values are recieved
            {
                predkoscNastawa.value = float.Parse(vec3[2]);
                pletwaNastawa.value = float.Parse(vec3[3]);
            }
            CamX = 19 + float.Parse(vec3[0]);
            CamY = 90 + float.Parse(vec3[1]);
            Kamera.transform.rotation = Quaternion.Euler(CamX, CamY+COG, 0);
        }


        


        predkosc.text = predkoscNastawa.value.ToString() + " %";
        pletwa.text = pletwaNastawa.value.ToString() + " °";
        
        if (predkoscAktualna != predkoscNastawa.value * predkoscMax / 100)
        {
            predkoscAktualna = Mathf.Lerp(predkoscAktualna, (predkoscMax * predkoscNastawa.value) / 100, 0.05f * Time.deltaTime);
            predkoscAktHUD.text = predkoscAktualna.ToString("0.0") + " kn";
        }

        pletwaAkt = Mathf.MoveTowards(pletwaAkt, pletwaNastawa.value, 1f * Time.deltaTime);
        predkoscAktualna = Mathf.MoveTowards(predkoscAktualna, (predkoscMax * predkoscNastawa.value) / 100, 0.05f * Time.deltaTime);

        if (predkoscAktualna != 0)
        {
            ROT += (K * (pletwaAkt) - ROT) * Time.deltaTime;
            ROTAktHUD.text = ROT.ToString("0.0") + "  °/s";
            
            COG += ROT * Time.deltaTime;
            COGAktHUD.text = COG.ToString("0.0") + "  °";
            {
                x += predkoscAktualna * Time.deltaTime * Mathf.Sin(COG * Mathf.PI / 180);
                z += predkoscAktualna * Time.deltaTime * Mathf.Cos(COG * Mathf.PI / 180);
                transform.position = new Vector3(2100+z, 500, 1900-x);
                transform.rotation = Quaternion.Euler(0, COG, 0);
                wychylenieAktHUD.text = pletwaAkt.ToString("0") + " °";
            }
        }

        arduino.WriteToArduino(predkoscAktHUD.text);
    }
}






