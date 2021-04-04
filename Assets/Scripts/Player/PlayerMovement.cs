using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;

    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private Animator animator;

    private Vector2 _movement;

    // Update is called once per frame
    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", _movement.x); 
        animator.SetFloat("Vertical", _movement.y); 
        animator.SetFloat("Speed", _movement.sqrMagnitude); 
    }

    void FixedUpdate() {
        _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);
    }
}
