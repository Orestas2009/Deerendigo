using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Wendigo : MonoBehaviour
{
    GameObject Player;
    NavMeshAgent agent;
    Rigidbody rb;
    Animator animate;
    [SerializeField] LayerMask groundLayer, Playerlayer;
    Material nightime;
    
    void Start()
    {
        RenderSettings.ambientIntensity = 0f;
        RenderSettings.reflectionIntensity = 0f;
        RenderSettings.skybox = nightime;
        animate = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Chase();
    }
    void Chase()
    {
        if(Player.active)
        {
            agent.SetDestination(Player.transform.position);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (Player)
        {
            SceneManager.LoadScene("Menu");
            
        }
    }
}