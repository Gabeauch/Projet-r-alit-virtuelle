using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // VARIABLE VERS UNE INSTANCE DE LA CLASSE RigidBody
    private Rigidbody playerRb;
    public float jumpForce = 10;
    public float gravityModifier = 1;
    public bool isOnGround = true;
    public bool gameOver = false;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // DETECTER SI LA TOUCHE ESPACE EST APPUYÉE
        // ET EST-CE QUE LE PERSONNAGE EST DÉCLARÉ AU SOL?


        if ( Input.GetKeyDown(KeyCode.Space) && isOnGround )
        {
            // SI OUI, IMPRIMER LE MESSAGE "JUMP"
            Debug.Log("JUMP");
            // AJOUTER UNE FORVE VERS LE HAUT 
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse );
            // DÉCLARER QUE LE PERSONNAGE N'EST PLUS AU SOL
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }   
    }

    // LORSQUE LE PERSONNAGE RENTRE EN COLLISION AVEC LE SOL
    // DÉCLARER QU'IL EST AU SOL
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle")) {
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            isOnGround = false;
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
       
    }


}

