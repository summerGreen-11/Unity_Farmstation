using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArduinoBluetoothAPI;
using System;
using System.Text;

public class LedControl : MonoBehaviour
{

    // Use this for initialization
    BluetoothHelper bluetoothHelper;
    string deviceName;
    string received_message;
    private string tmp;


    void Start()
    {


        deviceName = "HELLO"; //bluetooth should be turned ON;
        try
        {
            BluetoothHelper.BLE = true;
            bluetoothHelper = BluetoothHelper.GetInstance(deviceName);
            bluetoothHelper.OnConnected += OnConnected;
            bluetoothHelper.OnConnectionFailed += OnConnectionFailed;
            bluetoothHelper.OnDataReceived += OnMessageReceived; //read the data
            bluetoothHelper.OnScanEnded += OnScanEnded;

            bluetoothHelper.setTerminatorBasedStream("\n");

            if (!bluetoothHelper.ScanNearbyDevices())
            {
                //scan didnt start (on windows desktop (not UWP))
                //try to connect
                bluetoothHelper.Connect();//this will work only for bluetooth classic.
                //scanning is mandatory before connecting for BLE.

            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            write(ex.Message);
        }
    }

    private void write(string msg)
    {
        tmp += ">" + msg + "\n";
    }

    void OnMessageReceived(BluetoothHelper helper)
    {
        received_message = helper.Read();
        Debug.Log(received_message);
        write("Received : " + received_message);
    }

    void OnConnected(BluetoothHelper helper)
    {
        try
        {
            helper.StartListening();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            write(ex.Message);

        }
    }

    void OnScanEnded(BluetoothHelper helper, LinkedList<BluetoothDevice> devices)
    {

        if (helper.isDevicePaired()) //we did found our device (with BLE) or we already paired the device (for Bluetooth Classic)
            helper.Connect();
        else
            helper.ScanNearbyDevices(); //we didn't
    }

    void OnConnectionFailed(BluetoothHelper helper)
    {
        write("Connection Failed");
        Debug.Log("Connection Failed");
    }


    //Call this function to emulate message receiving from bluetooth while debugging on your PC.
    void OnGUI()
    {
        GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
        myButtonStyle.fontSize = 40;

        tmp = GUI.TextField(new Rect(Screen.width / 10, Screen.height / 3, Screen.width * 0.8f, Screen.height / 3), tmp, myButtonStyle);

        if (bluetoothHelper != null)
            bluetoothHelper.DrawGUI();
        else
            return;

        if (!bluetoothHelper.isConnected())
            if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 10, Screen.height / 10, Screen.width / 5, Screen.height / 10), "Connect", myButtonStyle))
            {
                if (bluetoothHelper.isDevicePaired())
                    bluetoothHelper.Connect(); // tries to connect
                else
                    write("Cannot connect, device is not found, for bluetooth classic, pair the device\n\tFor BLE scan for nearby devices");
            }

        if (bluetoothHelper.isConnected())
        {
            if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 10, Screen.height - 2 * Screen.height / 10, Screen.width / 5, Screen.height / 10), "Disconnect", myButtonStyle))
            {
                bluetoothHelper.Disconnect();
                write("Disconnected");
            }

            if (GUI.Button(new Rect(Screen.width / 2 - Screen.width / 4, Screen.height / 10, Screen.width / 5, Screen.height / 10), "Turn On", myButtonStyle))
            {
                bluetoothHelper.SendData("1");
                write("Sending 1");
            }

            if (GUI.Button(new Rect(Screen.width / 2 + Screen.width / 10, Screen.height / 10, Screen.width / 5, Screen.height / 10), "Turn Off", myButtonStyle))
            {
                bluetoothHelper.SendData("2");
                write("Sending 2");
            }

        }

    }

    void OnDestroy()
    {
        if (bluetoothHelper != null)
            bluetoothHelper.Disconnect();
    }
}

