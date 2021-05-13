using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent nav;
    private float valor_velocidad;
    private Transform player;
    private Animator animator;
    private float speed;
    private float limit;
    public int posicion;

    // Start is called before the first frame update
    void Start()
    {
        limit = -157;
        player = GameObject.FindWithTag("Meta").transform;
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        posicion = 0;
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.position);
        //animator.SetFloat("SpeedX", 1.5f);
        if(transform.position.x>limit)
        {
            animator.SetFloat("SpeedY", 1.5f);
        }
        else
        {
           animator.SetFloat("SpeedY", 0.0f);
        }
        //animator.SetFloat("SpeedY", 1.5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stop"))
        {
            posicion++;
            animator.SetFloat("SpeedY", 0.0f);
            Debug.Log("Colision Stop" + posicion);
        }
        if (other.gameObject.CompareTag("Pelota"))
        {
            valor_velocidad = nav.speed - 5;
            animator.SetFloat("SpeedY", 0.5f);
            nav.speed = valor_velocidad;
            Debug.Log("Colision Pelota");
            Destroy(other.gameObject);
        }
    }
}
