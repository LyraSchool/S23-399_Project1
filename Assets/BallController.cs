using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BallController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    
    [SerializeField]
    private float jumpForce = 5;
    
    
    public int score = 0;
    
    private bool countdown;
    
    
    
    [SerializeField]
    private TMP_Text scoreText;
    
    [SerializeField]
    private GameObject pauseText;
    
    
    

    private Rigidbody _rigidbody;


    // Start is called before the first frame update
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        countdown = false;
    }

    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        
        if (countdown)
        {
            HandleQuitCountdown();
        }
    }

    private void HandleQuitCountdown()
    {
        // Timer Stuff

        if (timer < 3)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
        }
        else
        {
            //Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }


    private void HandleInput()
    {
        float xSpeed = Input.GetAxis("Horizontal");
        float ySpeed = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(xSpeed, 0, ySpeed);
        movement.Normalize();
        
        movement *= speed;
        
        // Debug.Log(movement);

        Vector3 position = transform.position;
        transform.position = Vector3.Lerp(position, position + movement, Time.deltaTime);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            bool isPaused = pauseText.activeSelf;
            Time.timeScale = isPaused ? 1 : 0;
            pauseText.SetActive(!isPaused);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            countdown = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered!");
        score++;
        
        scoreText.text = $"Score: {score}";
        
        
        
        float newX = UnityEngine.Random.Range(-9, 9);
        float newZ = UnityEngine.Random.Range(-9, 9);

        Transform otherTransform = other.transform;
        Vector3 otherPos = otherTransform.position;
        otherTransform.position = new Vector3(newX, otherPos.y, newZ);
    }
    
}
