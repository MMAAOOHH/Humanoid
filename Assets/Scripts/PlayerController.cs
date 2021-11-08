using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Camera cam;
    private Rigidbody rb;
    private float _horizontal;
    private float _vertical;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;

    private MeshRenderer playerRenderer;

    private int playerHealth = 5;
    
    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        playerRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //Rotate player towards mouse
        Vector3 lookDirection = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDirection.z, lookDirection.x) *- Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation,rotation,rotationSpeed*Time.deltaTime);

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(_horizontal, 0f, _vertical) * moveSpeed;
    }
    
    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth<= 0)
        {
           PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        FindObjectOfType<GameManager>().PlayerDeath();
        Destroy(gameObject);
    }
}

