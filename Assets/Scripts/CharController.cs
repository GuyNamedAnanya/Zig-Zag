using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [SerializeField] Transform rayStart;
    [SerializeField] AudioClip pickupSFX;

    [SerializeField] GameObject Effect;

    
    GameManager gameManager;
    Rigidbody rb;
    Animator anim;
    AudioSource audioSource;

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
        if(gameManager.isGameStarted == false)
        {
            return;
        }
        else
        {
            anim.SetTrigger("isRunning");
        }
        rb.transform.position = transform.position + 2 * Time.deltaTime * transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Switch();
        }

        if (!Physics.Raycast(rayStart.position, -transform.up, out hit, Mathf.Infinity))
        {
            anim.SetTrigger("IsFalling");
        }
        else
        {
            anim.SetTrigger("notFalling");
        }

        if(transform.position.y < -2)
        {
            gameManager.RestartGame();
        }
    }

    void Switch()
    {
        if(gameManager.isGameStarted == false)
        {
            return;
        }
        isWalkingRight = !isWalkingRight;

        if(isWalkingRight)
        {
            transform.rotation = Quaternion.Euler(0, 45, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, -45, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Crystal"))
        {
            gameManager.IncreaseScore();
            GameObject g = Instantiate(Effect, rayStart.transform.position, Quaternion.identity);
            audioSource.PlayOneShot(pickupSFX);
            Destroy(g, 1f);
            Destroy(other.gameObject);            
        }
    }
}
