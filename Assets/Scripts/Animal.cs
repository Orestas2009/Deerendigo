using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEditor;

public class Animal : MonoBehaviour
{
    GameObject Player;
    NavMeshAgent agent;
    Rigidbody rb;
    Animator animate;
    [SerializeField] LayerMask groundLayer, Playerlayer;
    Vector3 Destpoint;
    bool WalkPointSet;
    [SerializeField] float range;
    float health = 1f;
    public bool wendigo = false; 
    public float type = 1;
    float speed = 5f;
    public GameObject Monster;
    public float jumpStrength = 2;
    public AudioSource Sound;
    public Material material;
    public GameObject skin;

    
    private static List<Animal> allAnimals = new List<Animal>();

    void Awake()
    {
        
        allAnimals.Add(this);
    }

    void OnDestroy()
    {
        
        allAnimals.Remove(this);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f && !wendigo)
        {
            Die();
        }
        if (health <= 0f && wendigo)
        {
            Instantiate(Monster, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        type = Random.Range(1, 11);
        material = GetComponent<Material>();
        Sound = GetComponent<AudioSource>();
        animate = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        CheckWendigoStatus();

        if (!wendigo)
        {
            Patrol();
        }
        if (type == 1 && wendigo)
        {
            Patrol();
        }
        if (type == 2 && wendigo)
        {
            animate.enabled = false;
        }
        if (type == 3 && wendigo)
        {
            Patrol();
        }
        if (type == 4 && wendigo)
        {
            Patrol();
            agent.speed = 10;
        }
        if (type == 5)
        {
            Patrol();
            animate.enabled = false;
        }
        if (type == 6 && !Sound.isPlaying && wendigo)
        {
            Patrol();
            Sound.Play();
        }
        if (type == 7 && wendigo)
        {
            animate.enabled = false;
            agent.enabled = false;
        }
        if (type == 8 && wendigo)
        {
            Patrol();
        }
        if (type == 9 && wendigo)
        {
            Patrol();
            skin.GetComponent<SkinnedMeshRenderer>().material = material;
        }
        if (type == 10 && wendigo)
        {
            agent.SetDestination(Player.transform.position);
        }
    }

    void CheckWendigoStatus()
    {
        
        int falseCount = 0;
        foreach (var animal in allAnimals)
        {
            if (!animal.wendigo)
            {
                falseCount++;
            }
        }

        
        if (falseCount == 8)
        {
            foreach (var animal in allAnimals)
            {
                if (!animal.wendigo) continue; 
                animal.wendigo = true; 
            }
        }
    }

    void Patrol()
    {
        if (!WalkPointSet) SearchForDest();
        if (WalkPointSet) agent.SetDestination(Destpoint);
        if (Vector3.Distance(transform.position, Destpoint) < 10) WalkPointSet = false;
    }

    void SearchForDest()
    {
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);
        Destpoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
        if (Physics.Raycast(Destpoint, Vector3.down, groundLayer))
        {
            WalkPointSet = true;
        }
    }

    void Die()
    {
        FindObjectOfType<Gun>().points += 1;
        agent.enabled = false;
        rb.isKinematic = false;
        animate.enabled = false;
        Destroy(this);
    }

    void Jump()
    {
        agent.enabled = false;
        animate.enabled = false;
        rb.isKinematic = false;
        rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && wendigo == true && type == 1)
        {
            transform.LookAt(Player.transform);
            animate.enabled = false;
            agent.enabled = false;
        }
        if (collision.gameObject.CompareTag("Player") && wendigo == true && type == 2)
        {
            Patrol();
        }
        if (collision.gameObject.CompareTag("Player") && wendigo == true && type == 3)
        {
            Jump();
        }
        if (collision.gameObject.CompareTag("Player") && wendigo == true && type == 8)
        {
            animate.enabled = false;
            agent.enabled = false;
        }
    }
}