using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //Libreria para utilizar propiedades de texto
using UnityEngine.SceneManagement; //Controlar la escena
using UnityEngine.UI; //Libreria para controlar los botones

public class PlayerController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float speed = 15;
    private float turnSepeed = 35;
    private Animator animator;
    public bool inicioCarrera = false;

    //Limite del jugador
    private float rangoX = 158f;

    //Tiros
    public int tiros;
    public GameObject rocaPrefab;
    private float fire = 1; //Es el tiempo que el jugador debe de esperar para tirar de nuevo
    private float next = 0; //Es el tiempo despues de tirar el jugador

    //Audio
    public AudioClip enemySound;
    public AudioClip starSound;
    public AudioClip pelotaSound;
    private AudioSource playerAudio;


    //Animaciones
    private Animator playerAnim;
    public ParticleSystem rocaParticle;
    public ParticleSystem starParticle;

    //Velocidad
    public bool hasIncrementSpeed;
    private float incrementSpeed = 25;

    //Canvas
    public int nivel = 1;
    public TextMeshProUGUI nivelText;
    public Image rayoImage;
    public Button restartButton;
    public TextMeshProUGUI titleText;
    public Button iniciarButton;
    public Button podioButton;
    public bool iniciarJuego;
    public bool restartJuego;

    //Enemigos
    private GameObject enemy1;
    private GameObject enemy2;
    private GameObject enemy3;

    // Start is called before the first frame update
    public void Start()
    {
        inicioCarrera = false;
        iniciarJuego = false;
        restartJuego = false;
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
        enemy1 = GameObject.Find("Enemy");
        enemy2 = GameObject.Find("Enemy2");
        enemy3 = GameObject.Find("Enemy3");
    }

    // Update is called once per frame
    void Update()
    {
        if (iniciarJuego == true)
        {
            restartJuego = false;
            iniciarButton.gameObject.SetActive(false);
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            //transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
            //transform.Rotate(Vector3.up * Time.deltaTime * turnSepeed * horizontalInput);
            animator.SetFloat("SpeedX", horizontalInput);
            animator.SetFloat("SpeedY", verticalInput);
            if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0)
            {
                inicioCarrera = true;
            }
            //Permite tirar una trampa con espacio
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > next && tiros > 0)
            {
                next = Time.time + fire;
                Instantiate(rocaPrefab, transform.position, transform.rotation);
                Instantiate(rocaParticle, transform.position, rocaParticle.transform.rotation);
                playerAudio.PlayOneShot(pelotaSound, 1.0F);
                tiros--;
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                StartCoroutine(IncrementSpeed());
                hasIncrementSpeed = true;
            }
            if (!hasIncrementSpeed)
            {
                rayoImage.gameObject.SetActive(false);
                transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
                transform.Rotate(Vector3.up * Time.deltaTime * turnSepeed * horizontalInput);
            }
            else
            {
                rayoImage.gameObject.SetActive(true);
                transform.Translate(Vector3.forward * Time.deltaTime * incrementSpeed * verticalInput);
                transform.Rotate(Vector3.up * Time.deltaTime * turnSepeed * horizontalInput);
            }
            //Limites de movimiento para jugador horizontal
            if (transform.position.x < -rangoX)
                transform.position = new Vector3(-rangoX, transform.position.y, transform.position.z);
            nivelText.gameObject.SetActive(true);
            nivelText.text = "Nivel: " + nivel;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Star"))
        {
            Debug.Log("Mas Tiros");
            playerAudio.PlayOneShot(starSound, 1.0F);
            Instantiate(starParticle, transform.position, starParticle.transform.rotation);
            Destroy(other.gameObject);
            tiros++;
        }
        if (other.gameObject.CompareTag("Enemy1") || other.gameObject.CompareTag("Enemy2")|| other.gameObject.CompareTag("Enemy3"))
        {
            playerAudio.PlayOneShot(enemySound, 1.0F);
        }
    }
    IEnumerator IncrementSpeed()
    {
        yield return new WaitForSeconds(2);
        hasIncrementSpeed = false;
    }
    public void ResertGame()
    {
        restartJuego = true;
        iniciarButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(false);
        nivel++;
        transform.position = new Vector3(-2.2f, 0.6f, 1.5f);
        enemy1.transform.position = new Vector3(-2.2f, 0, -5);
        enemy2.transform.position = new Vector3(-2.2f, 0, -1.5f);
        enemy3.transform.position = new Vector3(-2.2f, 0, 5);
    }
    public void Inicio()
    {
        tiros = 0;
        tiros = 0;
        inicioCarrera = false;
        iniciarJuego = false;
        nivelText.gameObject.SetActive(true);
        rayoImage.gameObject.SetActive(false);
        titleText.gameObject.SetActive(false);
        podioButton.gameObject.SetActive(false);
        //iniciarButton.gameObject.SetActive(false);
        iniciarJuego = true;
    }
}
