using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class add : MonoBehaviour
{
    public float MoveForce = 100.0f;
    public float JumpForce = 100.0f;
    public float MaxSpeed = 5.0f;
    public Rigidbody2D Hero;
    [HideInInspector]
    public bool FaceRight = true;
    [HideInInspector]
    public bool OnGroundAndJump = false;
    public Transform mGroundCheck;
    // Start is called before the first frame update
    void Start()
    {
        Hero = GetComponent<Rigidbody2D>();
        mGroundCheck = transform.Find("GroundCheck");
    }

    void Update()
    {
        float Movex = Input.GetAxis("Horizontal");
        if (Mathf.Abs(Hero.velocity.x) < MaxSpeed)
        {
            Hero.AddForce(Vector2.right * MoveForce * Movex);
        }
        if (Mathf.Abs(Hero.velocity.x) >= MaxSpeed)
        {
            Hero.velocity = new Vector2(MaxSpeed * Mathf.Sign(Hero.velocity.x), Hero.velocity.y);
        }
        if ((FaceRight && Movex < 0) || (!FaceRight && Movex > 0))
        {
            flip();
        }
        if (Physics2D.Linecast(transform.position, mGroundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            if (Input.GetButtonDown("Jump"))
            {
                OnGroundAndJump = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if (OnGroundAndJump)
        {
            Hero.AddForce(Vector2.up * JumpForce);
            OnGroundAndJump = false;
        }
    }
    private void flip()
    {
        Vector3 HeroFace = transform.localScale;
        HeroFace.x *= -1;
        transform.localScale = HeroFace;
        FaceRight = !FaceRight;
    }
}
