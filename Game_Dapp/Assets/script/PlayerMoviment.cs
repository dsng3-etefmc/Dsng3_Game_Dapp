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
    

    // Start is called before the first frame update
    void Start() //Executada apenas 1 vez
    {
        groundLayer = LayerMask.GetMask("Solid");
        rig = GetComponent<Rigidbody2D>(); //controla a graviadde do personagem
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() //executa mais de uma vez
    {
        Move();

        if (Input.GetButtonDown("Jump")) {
            Jump();
        }
    }

    float getDirection() {
        return Mathf.Sign(transform.localScale.x);
    }

    void Move() //loica do moviemntoo do pergonagem
    {
        float inputHorizontal = Input.GetAxis("Horizontal");

        if (Mathf.Abs(inputHorizontal) > 0.01f)
        {
            transform.localScale = new Vector3(
                Mathf.Sign(inputHorizontal) * Mathf.Abs(transform.localScale.x), 
                transform.localScale.y,
                transform.localScale.z
            );
        }

        Vector3 movement = new Vector3(inputHorizontal, 0f, 0f);
        transform.position += movement * Speed * Time.deltaTime;
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
