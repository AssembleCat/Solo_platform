using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Walk : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    Animator anim;
    //SpriteRenderer spriteRenderer;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        //Think();
        anim = GetComponent<Animator>();
        //spriteRenderer = GetComponent<SpriteRenderer>(); 

        Invoke("Think", 5);
    }


    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        Vector2 front = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(front, Vector3.down, 1.0f, LayerMask.GetMask("platform"));
        if (rayHit.collider == null)
        {
            nextMove *= -1;
            CancelInvoke();
            Invoke("Think", 3);
        }
    }

    void Think() {
        nextMove = Random.Range(-1, 2); //최솟값은 범위에 포함되지만 최댓값은 포함안됨


        float nextThink = Random.Range(2f, 5f);
        Invoke("Think", nextThink);

        anim.SetInteger("walkspeed", nextMove);
        /*if(nextMove != 0)
            spriteRenderer.filpX = nextMove == 1;*/
    }
}
