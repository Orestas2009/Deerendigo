using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Gun : MonoBehaviour
{
    public float damage = 1f;
    public float range = 100f;
    public Camera cam;
    public ParticleSystem MuzzleFlash;
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
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
        }
    }
}
