using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float JumpPadSpeed = 20.0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("jump");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpPadSpeed,ForceMode2D.Impulse);
        }
    }
}
