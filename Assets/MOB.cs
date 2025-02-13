using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class MOB : MonoBehaviour
{
    public int TE = 0;
    public int EOS = 0;
    public int DE = 0;
    public string DH;
    public GameObject bulletPrefab;
    public GameObject enermyPrefab;
    public float spawnInterval = 0.2f;
    public float moveSpeed = 20f;
    public float rotationSpeed = 45f;
    public bool Drag = true;
    public int NumE = 1;
    public int Maxnum = 10;
    public Text textComponent;
    private float timer = 0f;
    private static MOB instance;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ememy")&&gameObject.CompareTag("Brain"))
        {
            Destroy(collision.gameObject);
            NumE--;
            TE++;
            DE++;
        }
        if (collision.gameObject.CompareTag("Bullet")&&gameObject.CompareTag("Ememy"))
        {
            Destroy(collision.gameObject);
        }
    }
    public static MOB Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<MOB>();
                if(instance == null)
                {
                    GameObject go = new GameObject("MOB");
                    instance=go.AddComponent<MOB>();
                }
            }
            return instance;
        }
    }
    void Awake( )
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
    }

    void Update()
    {
        for (; NumE <= Maxnum; NumE++)
        {
            System.Random rand = new System.Random();
            int r1 = rand.Next(-170, 170);
            int r2 = rand.Next(-75, 75);
            Vector3 randomPosition = new Vector3(r1, r2, 0f);
            Instantiate(enermyPrefab, randomPosition, Quaternion.identity);
        }
        if (Drag)
        {
            timer += Time.deltaTime;
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(worldMousePosition.x, worldMousePosition.y, transform.position.z);
            if (Input.GetKey(KeyCode.Space))
            {
                if (timer >= spawnInterval)
                {
                    Instantiate(bulletPrefab, transform.position, transform.rotation);
                    EOS++;
                    timer = 0f;
                }
            }
            DH = "mouse";
        }
        if(!Drag)
        {
            timer += Time.deltaTime;
            if (Input.GetKey(KeyCode.Space))
            {
                if (timer >= spawnInterval)
                {
                    Instantiate(bulletPrefab, transform.position, transform.rotation);
                    EOS++;
                    timer = 0f;
                }
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
            }
            Vector3 p = transform.position;
            float zRotation = transform.rotation.eulerAngles.z;
            double radian = zRotation * Mathf.Deg2Rad;
            double cosValue = Math.Cos(radian);
            double sinValue = Math.Sin(radian);
            double Speedx = moveSpeed * cosValue;
            double Speedy = moveSpeed * sinValue;
            double tmp = Speedx * Time.smoothDeltaTime;
            float ftmp = (float)tmp;
            p.x += ftmp;
            tmp = Speedy * Time.smoothDeltaTime;
            ftmp = (float)tmp;
            p.y += ftmp;
            transform.localPosition = p;
            if (Input.GetKey(KeyCode.W))
            {
                moveSpeed += 1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveSpeed -= 1f;
            }
            DH = "key";
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Drag = !Drag;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
        textComponent.text = "Hero:Drive(" + DH.ToString() + ")   TouchedEnemy(" + TE.ToString() + ")   EGG: OnScreen(" + EOS.ToString() + ")   Enemy: Count(" + 10 + ") Destroyed(" + DE.ToString() + ")";
    }
}
