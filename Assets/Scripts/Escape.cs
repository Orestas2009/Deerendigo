using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escape : MonoBehaviour
{
    GameObject wendigo;
    private void Update()
    {
        wendigo = GameObject.FindWithTag("Enemy");
    }
    
        
    
    private void OnTriggerEnter(Collider other)
    {
        if (wendigo.active && other.gameObject.CompareTag("Player"))
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        //else if( && other.gameObject.CompareTag("Player"))
        
        
    }

}
