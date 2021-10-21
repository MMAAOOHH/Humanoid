using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    [SerializeField] private float bulletForce = 15f;
    [SerializeField] private float fireRate = 1f;
    private float fireDelay = 0f;
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        fireDelay -= Time.deltaTime * fireRate;
        if (Input.GetButton("Fire1") && Time.time >= fireDelay)
        {
            fireDelay = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        _audioManager.Play("Shoot");
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity); 
        Rigidbody rb = bullet.GetComponent<Rigidbody>(); 
        rb.AddForce(transform.right * bulletForce, ForceMode.Impulse);
    }
}

