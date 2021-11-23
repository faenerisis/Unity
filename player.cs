    Rigidbody2D rb;
    
    public float moveSpeed = 5f;
    public float jumpPower = 5f;
    public int numberOfJumps = 0;

    public bool isGrounded = false;
    private bool facingRight = true;

    float horizontalMovement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //MOVEMENT
        horizontalMovement = Input.GetAxisRaw("Horizontal") * moveSpeed;
       
        //FLIP
        {
            if (horizontalMovement > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalMovement < 0 && facingRight)
        {
            Flip();
        }
        }
        //JUMP
        Jump();
        if (numberOfJumps > 1)
        {
            numberOfJumps = 1;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMovement, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
            numberOfJumps -= 1;
        }
        if (numberOfJumps <= 0)
        {
            isGrounded = false;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    //COLLIDER SECTION
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Enemy")
        {
            isGrounded = true;
            numberOfJumps += 1;
        }
    }
