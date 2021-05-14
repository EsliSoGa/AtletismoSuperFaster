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

    private GameObject playerGameObject;
    private PlayerController playerControllerScript;
    private Contador contadorControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        contadorControllerScript = GameObject.Find("Stop").GetComponent<Contador>();
        playerGameObject = GameObject.Find("Player");
        limit = -157;
        player = GameObject.FindWithTag("Meta").transform;
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        posicion = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (contadorControllerScript.navInabilitar==false)
        {
            nav.SetDestination(player.position);
            animator.SetFloat("SpeedY", 0.0f);
        }
        if (playerControllerScript.inicioCarrera ==false)
        {
            nav.speed = 0;
            animator.SetFloat("SpeedY", 0.0f);
        }
        else if(playerControllerScript.inicioCarrera == true)
        {
            nav.speed = 20;
            contadorControllerScript.navInabilitar = true;
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
            if (transform.position.x > limit)
            {
                animator.SetFloat("SpeedY", 1.5f);
            }
            else
            {
                animator.SetFloat("SpeedY", 0.0f);
            }
        }
        if(playerControllerScript.restartJuego == true)
        {
            nav.speed += 5;
        }
        /*if (contadorControllerScript.navInabilitar == true)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }
        else
        {

        }*/

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stop"))
        {
            posicion++;
            nav.speed = 0;
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
