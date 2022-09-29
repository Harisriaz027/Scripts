using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput,verticalInput;
    public float speed;
    public Animator playerAnim;
    private Rigidbody2D playerRb;
    public bool climbingAllowed,attackMode,gameOver;
    bool inAir;
    GameManager gm;
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent< Rigidbody2D > ();
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
      
    }

   
    void Update()
    {
        if (gameOver == false)
        {

            if (horizontalInput == 0)
            {
                horizontalInput = Input.GetAxis("Horizontal");
                transform.Translate(Vector2.right * Time.deltaTime * speed * horizontalInput);
            }

            else if (horizontalInput > 0)
            {
                horizontalInput = Input.GetAxis("Horizontal");
                transform.Translate(Vector2.right * Time.deltaTime * speed * horizontalInput);
                transform.eulerAngles = new Vector2(0, 0);
                playerAnim.SetFloat("SPEED", horizontalInput);
            }
            else if (horizontalInput < 0)
            {
                horizontalInput = Input.GetAxis("Horizontal");
                transform.Translate(Vector2.left * Time.deltaTime * speed * horizontalInput);
                transform.eulerAngles = new Vector2(0, 180);
                playerAnim.SetFloat("SPEED", -horizontalInput);
            }
            if (Input.GetKeyDown(KeyCode.Space) && !inAir)
            {
                playerRb.AddForce(Vector2.up * 40, ForceMode2D.Impulse);
                playerAnim.SetBool("JUMP_B", true);
                inAir = true;
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                playerAnim.SetBool("JUMP_B", false);
            }
            verticalInput = Input.GetAxis("Vertical");
            if (Input.GetMouseButtonDown(0))
            {
                playerAnim.SetInteger("Attack", 1);
                attackMode = true;
                StartCoroutine(Attack());
            }

            if (climbingAllowed)
            {
                playerRb.isKinematic = true;
                playerRb.velocity = new Vector2(playerRb.velocity.x, verticalInput * speed);
            }
            else
            {
                playerRb.isKinematic = false;
            }
        }
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
        attackMode = false;
        playerAnim.SetInteger("Attack", 0);

        
    }
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3);
        Time.timeScale = 0;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            inAir = false;
        }
       if (collision.gameObject.CompareTag("Planet") && attackMode==true)
        {
            Destroy(collision.gameObject);
            gm.scoreCount += 10;
        }
        if (collision.gameObject.CompareTag("Planet") && attackMode == false)
        {
            Destroy(collision.gameObject);
            gameOver = true;
            playerAnim.SetBool("GameOver", true);
            StartCoroutine(GameOver());
            
            
        }
    }
    
  
}
