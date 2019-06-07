using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;

public class ArduinoConnector : MonoBehaviour
{
    public string port = "COM3";
    public int baudrate = 9200;
    private SerialPort stream;

    void Awake()
    {
        Open();
    }

    public void Open()
    {
        stream = new SerialPort(port, baudrate);
        stream.ReadTimeout = 10;
        stream.Open();
    }

    public void WriteToArduino(string message)
    {
        stream.WriteLine(message);
        stream.BaseStream.Flush();
    }

    public string ReadFromArduino(int timeout = 10)
    {
        stream.ReadTimeout = timeout;
        try
        {
            return stream.ReadLine();
        }
        catch (TimeoutException)
        {
            return null;
        }
    }
}