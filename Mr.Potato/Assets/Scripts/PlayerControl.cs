using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D heroBody;
    public float moveForce = 100;
    private float fInput = 0.0f;
    public float maxSpead = 5;
    private bool bFaceRight = true;
    //private bool bGrounded = false;
    //Transform mGroundCheck;
    void Start()
    {
        heroBody = GetComponent<Rigidbody2D>();
        //mGroundCheck = transform.Find("GroundCheck");
    }
    // Update is called once per frame
    void Update()
    {
        fInput = Input.GetAxis("Horizontal");
        if (fInput < 0 && bFaceRight)
            flip();
        else if (fInput > 0 && !bFaceRight)
            flip();
        //Physics2D.Linecast(transform.position, mGroundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }
    private void FixedUpdate()
    {
        if (Mathf.Abs(heroBody.velocity.x) < maxSpead)//fInput*heroBody.velocity.x
            heroBody.AddForce(Vector2.right * fInput * moveForce);
        if (Mathf.Abs(heroBody.velocity.x) > maxSpead)
            heroBody.velocity = new Vector2 (Mathf.Sign(heroBody.velocity.x) * maxSpead,heroBody.velocity.y);
    }
    void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        bFaceRight = !bFaceRight;
    }
}