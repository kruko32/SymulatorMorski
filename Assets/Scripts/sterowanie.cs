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
    public int pause = 0;
    public int reset = 0;

    public GameObject ArduinoConnector;
    public GameObject Kamera;

    private float CamX;
    private float CamY;
    private Vector3 targetRotation;
    private Vector3 rotateValue;
    private Quaternion rotation;

    public string[] vec3;


    ArduinoConnector arduino;

    public void  Start()
    {
        arduino = ArduinoConnector.GetComponent<ArduinoConnector>();  //this is used to connect into arduino board
    }


    void Update()
    {
        string value = arduino.ReadFromArduino(); //Read the information
        
        if (value == null)  //if data is null [...]
        {
           // [...] then do nothing
        }
        else
        {
            vec3 = value.Split(','); //My arduino script returns a 4 part value (IE: 0,0,18,100,0,1)
            //////////////////////////////////////////////////////////////  x,y,speed,rudder,button1,button2


            if (vec3[0] != "" && vec3[1] != "" && vec3[2] != "" && vec3[3] != "" && vec3[4] != "" && vec3[5] != "") //Check if all values are recieved
            {
                predkoscNastawa.value = float.Parse(vec3[2]);   //parse spped value from serial
                pletwaNastawa.value = float.Parse(vec3[3]);     //parse rudder value from serial
                CamX = 19 + float.Parse(vec3[0]);   //parse X axis value
                CamY = 90 + float.Parse(vec3[1]);   //parse Y axis value
                Kamera.transform.rotation = Quaternion.Euler(CamX, CamY + COG, 0);  //rotates camera

                if(int.Parse(vec3[4]) == 0 && pause == 0)   //pause simulation
                {
                    Time.timeScale = 0;
                    pause = 1;
                }

                if (int.Parse(vec3[5]) == 0 && pause == 1)  //unpause simulation
                {
                    Time.timeScale = 1;
                    pause = 0;
                }
            }
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






