using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private bool doubleJump = true;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    
    public AudioClip jumpSound;
    public AudioClip crashSound;

    public float jumpForce;
    public float gravityModifier;

    public bool isOnGround = true;
    public bool gameOver = false;
    public bool dashActive = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component of the object
        playerRb = GetComponent<Rigidbody>();
        // Get the Animator component of the object
        playerAnim = GetComponent<Animator>();
        // Get the AudioSource component of the object
        playerAudio = GetComponent<AudioSource>();
        // Set the gravity for this object
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && doubleJump)
        {
            // Push the object up with the physics system instantly 
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // Set isOnGround false so the player can not double jump
            isOnGround = false;
            doubleJump = false;

            playerAnim.Play("Running_Jump", 3, 0f);
            playerAudio.PlayOneShot(jumpSound, 0.05f);
            dirtParticle.Stop();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !doubleJump && !gameOver)
        {
            // Push the object up with the physics system instantly 
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // Set isOnGround false so the player can not double jump
            isOnGround = false;
            doubleJump = true;

            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 0.05f);
            dirtParticle.Stop();
        }

        if (Input.GetKeyDown(KeyCode.F) && !gameOver)
        {
            if (dashActive == true)
            {
                dashActive = false;
                playerAnim.SetFloat("Speed_f", 1.0f);
            }
            else
            {
                dashActive = true;
                playerAnim.SetFloat("Speed_f", 1.5f);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Set isOnGround true so the player can jump again after touching the ground
            isOnGround = true;
            if (!gameOver)
            {
                dirtParticle.Play();
            }
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Set gameOver true if the objects collides with an Obstacle
            gameOver = true;
            Debug.Log("Game Over!");

            playerAudio.PlayOneShot(crashSound, 0.3f);
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
        }
    }
}