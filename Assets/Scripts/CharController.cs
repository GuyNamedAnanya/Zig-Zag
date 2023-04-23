using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [SerializeField] Transform rayStart;
    [SerializeField] AudioClip pickupSFX;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float elapsedTime = 0;
    [SerializeField] float timeToIncreaseSpeed = 5f;
    [SerializeField] float moveSpeedAddFactor = 0.5f;
    [SerializeField] GameObject Effect;

    
    GameManager gameManager;
    Rigidbody rb;
    Animator anim;
    AudioSource audioSource;
    float fallingDistance = -2f;
    float characterRotation = 45f;
    bool isWalkingRight = true;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        audioSource = GetComponent<AudioSource>();
        
    }

    private void FixedUpdate()
    {
        //starts the running animaton when the game starts
        if(gameManager.isGameStarted == false)
        {
            return;
        }
        else
        {
            anim.SetTrigger("isRunning");
        }

        IncreaseSpeed();
    }

    /// <summary>
    /// Increases the speed of the player after every set duration of time
    /// </summary>
    void IncreaseSpeed()
    {
        if(elapsedTime < timeToIncreaseSpeed)
        {
            elapsedTime += Time.deltaTime;
        }
        else
        {
            moveSpeed += moveSpeedAddFactor;
            elapsedTime = 0;
        }
        rb.transform.position = transform.position + moveSpeed * Time.deltaTime * transform.forward;
    }

    
    void Update()
    {
        RaycastHit hit;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Switch();
        }

        // checks if raycast hits the gorund or not to set the animator to falling or not falling
        if (!Physics.Raycast(rayStart.position, -transform.up, out hit, Mathf.Infinity))
        {
            anim.SetTrigger("IsFalling");
        }
        else
        {
            anim.SetTrigger("notFalling");
        }

        //if position of character is less than falling distance then we restart
        if(transform.position.y < fallingDistance)
        {
            gameManager.RestartGame();
        }
    }

    /// <summary>
    /// switches the direction of player from left to right
    /// </summary>
    void Switch()
    {
        if(gameManager.isGameStarted == false)
        {
            return;
        }

        //toggles between true and false for iswalkingright
        isWalkingRight = !isWalkingRight;

        if(isWalkingRight)
        {
            //rotates the player right if it is facing right
            transform.rotation = Quaternion.Euler(0, characterRotation, 0);
        }
        else
        {
            //rotates the player left if it is facing left
            transform.rotation = Quaternion.Euler(0, -characterRotation, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Crystal"))
        {
            //if player collides with diamond
            //score is increased
            //pickup effect is played
            //pickup audio is played
            gameManager.IncreaseScore();
            GameObject g = Instantiate(Effect, rayStart.transform.position, Quaternion.identity);
            audioSource.PlayOneShot(pickupSFX);
            Destroy(g, 1f);
            Destroy(other.gameObject);            
        }
    }
}
