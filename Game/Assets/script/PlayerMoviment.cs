using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public float forwardJumpForce;
    public int allowedJumpsInAir;
    private int jumpsInAir = 0;
    private Rigidbody2D rig;
    private BoxCollider2D boxCollider;
    private LayerMask groundLayer;
    private Animator animator;
    

    // Start is called before the first frame update
    void Start() //Executada apenas 1 vez
    {
        groundLayer = LayerMask.GetMask("Solid");
        rig = GetComponent<Rigidbody2D>(); //controla a graviadde do personagem
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() //executa mais de uma vez
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        bool shouldMove = Mathf.Abs(inputHorizontal) > 0.01f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if (shouldMove) {
            turnCharacterToDirection(inputHorizontal);

            if (isRunning) {
                Run(inputHorizontal);
                animator.SetBool("walk", false);
                animator.SetBool("run", true);
            } else {
                Walk(inputHorizontal);
                animator.SetBool("walk", true);
                animator.SetBool("run", false);
            }
        } else {
            animator.SetBool("walk", false);
            animator.SetBool("run", false);
        }

        if (Input.GetButtonDown("Jump")) {
            Jump();
        }
    }

    float getDirection() {
        return - Mathf.Sign(transform.localScale.x);
    }

    void Walk(float inputHorizontal) //loica do moviemntoo do pergonagem
    {   
        Vector3 movement = new Vector3(inputHorizontal, 0f, 0f);
        transform.position += movement * Speed * Time.deltaTime;
    }

    void Run (float inputHorizontal) {
        Vector3 movement = new Vector3(inputHorizontal, 0f, 0f);
        transform.position += movement * 2 * Speed * Time.deltaTime;
    }

    void turnCharacterToDirection (float inputHorizontal) {
        transform.localScale = new Vector3(
            - Mathf.Sign(inputHorizontal) * Mathf.Abs(transform.localScale.x), 
            transform.localScale.y,
            transform.localScale.z
        );
    }

    bool isGrounded() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            Vector2.down,
            0.1f,
            groundLayer
        );
        return raycastHit.collider != null;
    }

    void Jump() //logica do pulo do personagem
    {
        bool shouldJump = false;

        if (this.isGrounded()) {
            this.jumpsInAir = 0;

            shouldJump = true;
        } else {
            if (jumpsInAir < allowedJumpsInAir) {
                this.jumpsInAir++;

                shouldJump = true;
            }
        }

        if (shouldJump) {
            rig.AddForce(
                new Vector2(this.getDirection() * forwardJumpForce, JumpForce), 
                ForceMode2D.Impulse
            );
        }
    }
}
