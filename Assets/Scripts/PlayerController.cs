using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float speed = 15;
    private float turnSepeed = 35;
    private Animator animator;

    //Tiros
    public int tiros;
    //Tiro de objetos
    //Tiro de objetos
    public GameObject rocaPrefab;
    private float fire = 1; //Es el tiempo que el jugador debe de esperar para tirar de nuevo
    private float next = 0; //Es el tiempo despues de tirar el jugador

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        tiros = 0;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        transform.Rotate(Vector3.up * Time.deltaTime * turnSepeed * horizontalInput);
        animator.SetFloat("SpeedX", horizontalInput);
        animator.SetFloat("SpeedY", verticalInput);
        //Permite tirar una trampa con espacio
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > next && tiros>0)
        {
            next = Time.time + fire;
            Instantiate(rocaPrefab, transform.position, transform.rotation);
            tiros--;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Star"))
        {
            Debug.Log("Mas Tiros");
            Destroy(other.gameObject);
            tiros++;
        }

    }
}
