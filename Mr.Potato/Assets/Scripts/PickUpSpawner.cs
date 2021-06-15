using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] pickups;
    public float pickupTime = 5f;
    public float leftX = 12f;
    public float rightX = -12f;
    public float highHealthThreshold = 75f;     // The health of the player, above which only bomb crates will be delivered.
    public float lowHealthThreshold = 25f;		// The health of the player, below which only health crates will be delivered.

    private PlayerHealth playerHealth;			// Reference to the PlayerHealth script.

    void Awake()
    {
        // Setting up the reference.
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    void Start()
    {
        StartCoroutine(spawnPickup());
    }

    public IEnumerator spawnPickup()
    {
        yield return new WaitForSeconds(pickupTime);

        float randomx = Random.Range(leftX, rightX);
        Vector3 randomPos = new Vector3(randomx, 15, 0);


        if (playerHealth.health >= highHealthThreshold)
            // ... instantiate a bomb pickup at the drop position.
            Instantiate(pickups[0], randomPos, Quaternion.identity);
        // Otherwise if the player's health is below the low threshold...
        else if (playerHealth.health <= lowHealthThreshold)
            // ... instantiate a health pickup at the drop position.
            Instantiate(pickups[1], randomPos, Quaternion.identity);
        // Otherwise...
        else
        {
            // ... instantiate a random pickup at the drop position.
            int index = Random.Range(0, pickups.Length);//else  
            Instantiate(pickups[index], randomPos, Quaternion.identity);
        }
    }
}
