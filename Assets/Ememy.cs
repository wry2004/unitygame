using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Ememy : MonoBehaviour
{
    public int life = 4;
    public GameObject enermyPrefab;
    private Renderer objectRenderer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && gameObject.CompareTag("Ememy"))
        {
            Destroy(collision.gameObject);
            MOB.Instance.EOS--;
            life--;
            Color currentColor = objectRenderer.material.color;
            if(life==3)
            {
                currentColor.a = 0.75f;
            }
            if(life==2)
            {
                currentColor.a = 0.5f;
            }
            if(life==1)
            {
                currentColor.a = 0.25f;
            }
            objectRenderer.material.color = currentColor;
            if (life <= 0)
            {
                MOB.Instance.DE++;
                System.Random rand = new System.Random();
                int r1 = rand.Next(-170, 170);
                int r2 = rand.Next(-75, 75);
                Vector3 randomPosition = new Vector3(r1, r2, 0f);
                currentColor.a = 1f;
                objectRenderer.material.color = currentColor;
                life = 4;
                Instantiate(enermyPrefab, randomPosition, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }
    void Update()
    {   
    }
}
