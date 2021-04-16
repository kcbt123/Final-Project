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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key was pressed.");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Space key was released.");
        }
    }

    void FixedUpdate() {

        Vector2 distance = _movement * _moveSpeed * Time.fixedDeltaTime;
        Vector2 movePosition = _rb.position + distance;

        // if (Vector3.Distance(_rb.position, movePosition) <= 2f) {
        //     _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);
        // }

        if (Mathf.Abs(_movement.x) == 1f) {
            _rb.position = new Vector2(_movement.x, 0f);        
        } 

        if (Mathf.Abs(_movement.y) == 1f) {
            _rb.position = new Vector2(0f, _movement.y);       
        }
    }
}
