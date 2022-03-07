using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShot : MonoBehaviour
{
    public GameObject Camera;
    public GameObject PrefabShotLite;
    public GameObject PrefabShotHard;

    public Image ImageCrosshair;
    public Text TextRocket;
    
    public int NumberRocket = 8;

    private bool _redCrosshair = false;
    private float _timeRed;
    
    void Update()
    {
        InputKey();
        SwitchCrosshair();
    }

    void SwitchCrosshair()
    {
        if (_timeRed >= Time.time)
        {
            ImageCrosshair.color = Color.red;
        }
        else
        {
            ImageCrosshair.color = Color.white;
        }
    }

    void InputKey()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShotLite();
        }
        if (Input.GetMouseButtonDown(1))
        {
            ShotHard();
        }
    }

    void ShotLite()
    {
        Vector3 point = Shot(1);
        
        if (point != Vector3.zero)
        {
            Instantiate(PrefabShotLite, point, Quaternion.identity);
        }
    }

    void ShotHard()
    {
        if (NumberRocket > 0)
        {
            Vector3 point = Shot(3);
        
            if (point != Vector3.zero)
            {
                NumberRocket -= 1;
                TextRocket.text = "x" + NumberRocket;
                Instantiate(PrefabShotHard, point, Quaternion.identity);
            }
        }
    }

    Vector3 Shot(int value)
    {
        float minDist = 1000.0f;
        RaycastHit minHit = new RaycastHit();
        minHit.point = Vector3.zero;
        
        RaycastHit[] hits = Physics.RaycastAll(Camera.transform.position, Camera.transform.forward, 1000.0f);

        foreach (RaycastHit hit in hits)
        {
            Debug.DrawLine(Camera.transform.position, hit.point, Color.blue);
            if (hit.distance < minDist)
            {
                minDist = hit.distance;
                minHit = hit;
            }
        }

        if (minHit.point != Vector3.zero
            && minHit.collider.CompareTag("tank"))
        {
            _timeRed = Time.time + 0.2f;
            minHit.collider.gameObject.GetComponent<Health>().TakeDamage(value);
        }
        
        return minHit.point;
    }
}
