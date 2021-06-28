using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float MAX;
    public float Jump;
    Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;
    Animator anim;


    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
;    }

    void Update() {

        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJump"))
        {
            rigidbody.AddForce(Vector2.up * Jump, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
        }

        if (Input.GetButtonUp("Horizontal")) {
            //rigidbody.velocity.normalized
            rigidbody.velocity = new Vector2(rigidbody.velocity.normalized.x * 0.5f, rigidbody.velocity.y);
        }
        if (Input.GetButton("Horizontal"))
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;

        if (Mathf.Abs(rigidbody.velocity.x) < 0.4)
            anim.SetBool("isWalk", false);
        else
            anim.SetBool("isWalk", true);
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rigidbody.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigidbody.velocity.x > MAX)
            rigidbody.velocity = new Vector2(MAX, rigidbody.velocity.y);
        else if (rigidbody.velocity.x < MAX * (-1))
            rigidbody.velocity = new Vector2(MAX * (-1), rigidbody.velocity.y);

        if (rigidbody.velocity.y < 0) {
            Debug.DrawRay(rigidbody.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigidbody.position, Vector3.down, 1.0f, LayerMask.GetMask("platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                    anim.SetBool("isJump", false);
            }
        
        }
    }
}
