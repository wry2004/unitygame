using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public float speed = 40f;
    private void fun(float x, float y)
    {
        if ((y >= 95 || y <= -95 || x >= 195||x<=-195)&& gameObject.CompareTag("Bullet"))
        {
            MOB.Instance.EOS--;
            Destroy(gameObject);
        }
    }
    void Start()
    { 
    }
    void Update()
    {
        Vector3 p = transform.position;
        float zRotation = transform.rotation.eulerAngles.z;
        double radian = zRotation * Mathf.Deg2Rad;
        double cosValue = Math.Cos(radian);
        double sinValue = Math.Sin(radian);
        double Speedx = speed * cosValue;
        double Speedy = speed * sinValue;
        double tmp = Speedx * Time.smoothDeltaTime;
        float ftmp = (float)tmp;
        p.x += ftmp;
        tmp = Speedy * Time.smoothDeltaTime;
        ftmp = (float)tmp;
        p.y += ftmp;
        transform.localPosition = p;
        fun(p.x,p.y);
    }
}
