using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float huetBloodPoint = 20f;
    public  AudioClip[] ouchClips;
    public float damageRepeat = 0.5f;
    public float hurtForce = 1000f;
    
    private float lastHurt;
    private Animator anim;
    float health = 100f;
    SpriteRenderer healthbar;
    Vector3 healthBarScale;

    void Start()
    {
        healthbar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();

        healthBarScale = healthbar.transform.localScale;
        lastHurt = Time.time;

        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision .gameObject .tag == "Enemy")
        {
            if(Time .time >lastHurt +damageRepeat)
            {
                if(health > 0)
                {   
                    //减血
                    TakeDamage(collision.gameObject.transform );
                    lastHurt = Time.time;
                    if(health <= 0)
                    {
                        //播放死亡画面，掉入河中
                        anim.SetTrigger("Die");
                        //掉入河中
                        Collider2D[] colliders = GetComponents<Collider2D>();
                        foreach (Collider2D c in colliders)
                            c.isTrigger = true;

                        SpriteRenderer[] sp = GetComponentsInChildren<SpriteRenderer>();
                        foreach (SpriteRenderer s in sp)
                            s.sortingLayerName  = "UI";

                        GetComponent<PlayerControl>().enabled = false;
                        GetComponentInChildren<Gun>().enabled = false;
                    }
                }
            }
            
        }
    }
    void TakeDamage(Transform enemy)
    {
        health -= huetBloodPoint;
        //更新血条状态
        UpdateHealthBar();
        Vector3 hurtVector = transform.position - enemy.position + Vector3.up;
        GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);

        int i = Random.Range(0, ouchClips.Length);
        AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
    }
    void UpdateHealthBar()
    {
        healthbar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
        healthbar.transform.localScale = new Vector3(health * 0.01f, 1, 1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
