using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class Gun : MonoBehaviour
{
    public TextMeshProUGUI Quota;
    public float points = 0;
    public float damage = 1f;
    public float range = 100f;
    public Camera cam;
    public ParticleSystem MuzzleFlash;
    public GameObject ImpactEffect;
    public float fireCooldown;
    public float fireInterval = 0.5f;
    public float impactForce;
    public AudioSource shootSFX;
    private void Start()
    {
        shootSFX = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && fireCooldown <= 0)
        {
            Shoot();
            shootSFX.Play(0);
            fireCooldown = fireInterval;
            
        }
        fireCooldown -= Time.deltaTime;
        Quota.text = $"Quota {points.ToString()}/8";
        if (points == 8)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    void Shoot()
    {
        MuzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Animal target = hit.transform.GetComponent<Animal>();
            if (target != null )
            {
                target.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            GameObject ImpactGo =Instantiate(ImpactEffect, hit.point,Quaternion.LookRotation(hit.normal));
            Destroy(ImpactGo,2f);
        }
    }
}
