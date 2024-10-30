using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    public class MovePlayer : MonoBehaviour
    {
    
        public MovementJoystick movementJoystick;
        public float playerSpeed;
        private Rigidbody2D rb;

        //public Animator animator;
    
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            //animator = GetComponent<Animator>();
        }
    
        // Update is called once per frame
        void FixedUpdate()
        {
            Vector2 moveVector = new Vector2(movementJoystick.joystickVec.x, movementJoystick.joystickVec.y);
            if(moveVector != Vector2.zero)
            {
                rb.velocity = moveVector * playerSpeed;

                /*
                //update animator
                animator.SetFloat("Horizontal", moveVector.x);
                animator.SetFloat("Vertical", moveVector.y);
                animator.SetBool("isMoving", true);
                */
            }
            else
            {
                rb.velocity = Vector2.zero;
                //animator.SetBool("isMoving", false);
            }
        }
    }