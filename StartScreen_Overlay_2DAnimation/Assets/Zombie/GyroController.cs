using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroController : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;

    public GameObject objectToRotate;
    private Quaternion rot;

    private void Start()
    {

        objectToRotate.transform.position = transform.position;
        transform.SetParent(objectToRotate.transform);


        gyroEnabled = EnableGyro();

       //objectToRotate.SetActive(false);
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            //objectToRotate.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
            //rot = new Quaternion(0, 0, 1, 0);

            
            return true;
        }

        return false;
    }


    private void Update()
    {


        objectToRotate.transform.rotation = Quaternion.Euler(0, -Input.compass.trueHeading, 0);

        //if (gyroEnabled)
        {
            transform.localRotation = gyro.attitude * rot;
        }
    }
}