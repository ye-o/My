using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterControl : MonoBehaviour
{
    public GameObject riverE;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "river")
        {
            //实例化水花
            Instantiate(riverE, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));

            Destroy(gameObject);
            if (transform.name == "Hero")
                Destroy(GameObject.Find("UI_HealthBar"));
        }
    }
}
