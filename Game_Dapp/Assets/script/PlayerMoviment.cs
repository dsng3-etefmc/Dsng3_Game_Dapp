using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    private Rigidbody2D rig;

    // Start is called before the first frame update
    void Start() //Executada apenas 1 vez
    {
        rig = GetComponent<Rigidbody2D>(); //controla a graviadde do personagem
    }

    // Update is called once per frame
    void Update() //executa mais de uma vez
    {
        Move();
        Jump();
    }

    void Move() //loica do moviemntoo do pergonagem
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Speed * Time.deltaTime;
    }

    void Jump() //logica do pulo do personagem
    {
        if (Input.GetButtonDown("Jump"))
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }
    }
}
