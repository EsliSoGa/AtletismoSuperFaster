using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //Libreria para utilizar propiedades de texto
using UnityEngine.SceneManagement; //Controlar la escena
using UnityEngine.UI; //Libreria para controlar los botones
using UnityEngine.AI;
public class Contador : MonoBehaviour
{
    public int scorePlayer;
    public int scoreEnemy1;
    public int scoreEnemy2;
    public int scoreEnemy3;
    public int posicion;

    private PlayerController playerControllerScript;

    public TextMeshProUGUI scoreText;

    //Enemigos y jugador
    private GameObject player;
    private GameObject enemy1;
    private GameObject enemy2;
    private GameObject enemy3;
    public GameObject podioObjeto;
    public Button restartButton;
    public Button podioButton;
    private NavMeshAgent navMesh;
    // Start is called before the first frame update
    void Start()
    {
        posicion = 0;
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        scoreText.gameObject.SetActive(false);
        enemy1 = GameObject.Find("Enemy");
        enemy2 = GameObject.Find("Enemy2");
        enemy3 = GameObject.Find("Enemy3");
        player = GameObject.Find("Player");
        //podioObjeto = GameObject.Find("Podios");
    }

    // Update is called once per frame
    void Update()
    {
        if(posicion == 4)
        {
            playerControllerScript.inicioCarrera = false;
            playerControllerScript.iniciarJuego = false;
            playerControllerScript.restartButton.gameObject.SetActive(true);
            playerControllerScript.podioButton.gameObject.SetActive(true);
            posicion = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        posicion++;
        if (other.gameObject.CompareTag("Player"))
        {
            if (posicion == 1)
                scorePlayer += 100;
            else if (posicion == 2)
                scorePlayer += 75;
            else if (posicion == 3)
                scorePlayer += 50;
            else if (posicion == 4)
                scorePlayer += 25;
            Debug.Log("Pasa Jugador " + posicion);
            scoreText.gameObject.SetActive(true);
            scoreText.text = "Score: " + scorePlayer;
        }
        else if (other.gameObject.CompareTag("Enemy1"))
        {
            if (posicion == 1)
                scoreEnemy1 += 100;
            else if (posicion == 2)
                scoreEnemy1 += 75;
            else if (posicion == 3)
                scoreEnemy1 += 50;
            else if (posicion == 4)
                scoreEnemy1 += 25;
            Debug.Log("Pasa Enemy1 " + posicion);
        }
        else if (other.gameObject.CompareTag("Enemy2"))
        {
            if (posicion == 1)
                scoreEnemy2 += 100;
            else if (posicion == 2)
                scoreEnemy2 += 75;
            else if (posicion == 3)
                scoreEnemy2 += 50;
            else if (posicion == 4)
                scoreEnemy2 += 25;
            Debug.Log("Pasa Enemy2 " + posicion);
        }
        else if (other.gameObject.CompareTag("Enemy3"))
        {
            if (posicion == 1)
                scoreEnemy3 += 100;
            else if (posicion == 2)
                scoreEnemy3 += 75;
            else if (posicion == 3)
                scoreEnemy3 += 50;
            else if (posicion == 4)
                scoreEnemy3 += 25;
            Debug.Log("Pasa Enemy3 " + posicion);
        }
    }
    public bool navInabilitar = true;
    public void podio()
    {
        navInabilitar = false;
        Instantiate(podioObjeto, podioObjeto.transform.position, podioObjeto.transform.rotation);
        int[] vector = new int[4];
        int t;
        vector[0] = scorePlayer;
        vector[1] = scoreEnemy1;
        vector[2] = scoreEnemy2;
        vector[3] = scoreEnemy3;
        for (int a = 1; a < vector.Length; a++)
        { 
            for (int b = vector.Length - 1; b >= a; b--)
            {
                if (vector[b - 1] > vector[b])
                {
                    t = vector[b - 1];
                    vector[b - 1] = vector[b];
                    vector[b] = t;
                }
            }
        }
        enemy1.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        enemy2.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        enemy3.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        podioButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        for (int i=0; i<vector.Length;i++)
        {
            if(vector[i] == scorePlayer)
            {
                player.transform.position = new Vector3(-2.2f, 2, 3 - i);
            }
             if (vector[i] == scoreEnemy1)
             {
                 enemy1.transform.position = new Vector3(-2.2f, 2, 3 - i);
             } 
             if (vector[i] == scoreEnemy2)
             {
                 enemy2.transform.position = new Vector3(-2.2f, 2, 3 - i);
             }
             if (vector[i] == scoreEnemy3)
             {
                 enemy3.transform.position = new Vector3(-2.2f, 2, 3 - i);
             }
        }
    }
}
