using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
    public float Speed = 10.0f;
    public float SpeedRotate = 1.0f;
    
    private Vector3 _vectorMove;
    private Rigidbody _rb;

    private bool _boost = false;
    private float _boostPower = 10.0f;
    private float _boostCurrentPower;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _boostCurrentPower = _boostPower;
    }

    private void Update()
    {
        InputKey();
        Move();
    }

    void InputKey()
    {
        if (Input.GetKey("left shift")
            && _boostCurrentPower > 5.0f)
        {
            _boost = true;
        }
        
        if (Input.GetKeyUp("left shift"))
        {
            _boost = false;
        }
        
        _vectorMove = transform.forward * Input.GetAxis("Vertical");
    }

    void Move()
    {
        if (!GetComponent<Health>().Die)
        {
            Boost();
            _rb.MovePosition(_rb.transform.position + _vectorMove * Speed * Time.deltaTime);
            RotationAxis();
        }
    }

    void Boost()
    {
        if (_boost && _boostCurrentPower > 0)
        {
            _vectorMove *= 3.0f;
            _boostCurrentPower -= 2.0f * Time.deltaTime;
        }
        else
        {
            _boost = false;
        }

        if (_boostCurrentPower < _boostPower)
        {
            _boostCurrentPower += 0.3f * Time.deltaTime;
        }
    }
    
    void RotationAxis()
    {
        transform.Rotate(transform.up, Input.GetAxis("Horizontal") * SpeedRotate, Space.World);
    }
}
