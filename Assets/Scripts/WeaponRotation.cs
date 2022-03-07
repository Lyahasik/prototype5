using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    public GameObject Camera;
    public float SpeedRotate = 5.0f;

    void Update()
    {
        Rotation();
    }

    void Rotation()
    {
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x,
                                                    transform.localRotation.eulerAngles.y + Input.GetAxis("Mouse X") * SpeedRotate,
                                                    transform.localRotation.eulerAngles.z);
    }
}
