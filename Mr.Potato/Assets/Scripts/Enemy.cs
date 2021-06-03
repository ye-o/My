using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5.0f;
    public Sprite damageEnemy;
    public Sprite deadEnemy;
    public int HP = 2;
    public float minSpinForce = -200f;
    public float maxSpinForce = 200f;
   // public AudioClip[] deathClips;
    public GameObject UI_100Point;

    private Rigidbody2D enemyBody;
    private Transform frontCheck;
    private bool isDead = false;
    private SpriteRenderer curBody;

    void Start()
    {

    }
    private void Awake()
    {
        enemyBody = GetComponent <Rigidbody2D>();
        frontCheck = transform.Find("frontCheck");
        curBody = transform.Find("body").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        enemyBody.velocity = new Vector2(transform.localScale.x * moveSpeed, enemyBody.velocity.y);
        Collider2D[] colliders = Physics2D.OverlapPointAll(frontCheck.position);
        foreach (Collider2D c in colliders)
        {
            if (c.tag == "wall")
            {
                flip();
                break;
            }
        }
        //GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        if (HP == 1 && damageEnemy != null)
            curBody.sprite = damageEnemy;
        if(HP<=0 && !isDead)
        {
            death();
        }
    }
    public void Hurt()
    {
        HP--;
    }
    void death()
    {
        isDead = true;
        SpriteRenderer[] Sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer s in Sprites)
        {
            s.enabled = false;
        }

        curBody.sprite = deadEnemy;

        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (Collider2D c in cols)
            c.isTrigger = true;
       /* int i = Random.Range(0, deathClips.Length);
        AudioSource.PlayClipAtPoint(deathClips[i], transform.position);*/
        //给一个随机的旋转扭矩
        enemyBody.freezeRotation = false;
        enemyBody.AddTorque(Random.Range(minSpinForce, maxSpinForce));
        transform.Find("frontCheck").gameObject.SetActive(false);

        if (transform.name == "enemy1")
        {
            transform.Find("eye").GetComponent<SpriteRenderer>().enabled = false;
            transform.Find("eyeid").GetComponent<SpriteRenderer>().enabled = false;
        }
        Vector3 UI_100Points = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        Instantiate(UI_100Point, UI_100Points, Quaternion.identity);// Instantiate(UI_100Point, transform.position, Quaternion.identity);
        Debug.Log(Quaternion.identity);
    }
    void flip()
    {
        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
    }
}
