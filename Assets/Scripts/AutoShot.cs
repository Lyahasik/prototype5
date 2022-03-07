using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using Random = UnityEngine.Random;

public class AutoShot : MonoBehaviour
{
    public GameObject Parent;
    public Health Health;
    public float Distance = 40.0f;
    public GameObject EmptyWeapon;
    public GameObject PrefabFire;
    
    private GameObject[] _tanks;
    public bool Visible = false;

    private GameObject minDistTank;
    private bool _delay = false;

    private void Start()
    {
    }

    void Update()
    {
        _tanks = GameObject.FindGameObjectsWithTag("tank");
        
        if (!Health.Die)
        {
            CheckVision();
            LookShot();
            Shoting();
            
            minDistTank = null;
        }
    }

    void Shoting()
    {
        if (minDistTank != null
            && Vector3.Distance(minDistTank.transform.position, EmptyWeapon.transform.position) < Distance)
        {
            float minDist = Distance;
            GameObject minHit = null;
            Vector3 point = Vector3.zero;

            bool randomDefl = Random.Range(-1, 1) < 0 ? false : true;
            Vector3 deflection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.5f), 0.0f);
            RaycastHit[] hits =
                Physics.RaycastAll(EmptyWeapon.transform.position, 
                    EmptyWeapon.transform.forward + (randomDefl ? deflection : Vector3.zero),
                    Distance);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject != Parent
                    && hit.distance < minDist)
                {
                    minDist = hit.distance;
                    minHit = hit.collider.gameObject;
                    point = hit.point;
                }
            }

            if (!_delay && minHit)
            {
                _delay = true;
                Invoke("Delay", Random.Range(0.3f, 1.5f));
                Instantiate(PrefabFire, point, Quaternion.identity);

                if (minHit.CompareTag("tank"))
                {
                    minHit.GetComponent<Health>().TakeDamage(1);
                }
            }
        }
    }

    void Delay()
    {
        _delay = false;
    }

    void LookShot()
    {
        if (minDistTank)
        {
            transform.LookAt(minDistTank.transform, Vector3.up);
        }
    }

    void CheckVision()
    {
        float minDist = Distance;
        
        foreach (GameObject tank in _tanks)
        {
            if (tank != Parent
                && Vector3.Distance(tank.transform.position, transform.position) < Distance
                && Vector3.Distance(tank.transform.position, transform.position) < minDist
                && Mathf.Abs(tank.transform.position.y - EmptyWeapon.transform.position.y) < 2.0f)
            {
                Visible = true;
                minDist = Vector3.Distance(tank.transform.position, transform.position);
                minDistTank = tank;
            }
        }
        
        if (minDistTank == null
            || minDistTank.GetComponent<Health>().Die)
        {
            minDistTank = null;
            transform.localRotation = Quaternion.identity;
            Visible = false;
        }
    }
}
