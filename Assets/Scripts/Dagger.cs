using UnityEngine;

public class Dagger : MonoBehaviour
{
    private Enemy targetEnemy;  // To keep track of the targeted enemy
    private bool isMoving = false;

    void Start()
    {
        // Start moving the dagger
        isMoving = true;
    }

    void Update()
    {
        if (isMoving && targetEnemy != null)
        {
            // Move towards the enemy
            float step = 10f * Time.deltaTime;  // Adjust speed as needed
            transform.position = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, step);

            // Check if the dagger reaches the enemy
            if (transform.position == targetEnemy.transform.position)
            {
                // Destroy the enemy
                targetEnemy.Die();

                // Destroy the dagger
                Destroy(gameObject);
            }
        }
    }

    // Method to set the enemy target when dagger is thrown
    public void SetTarget(Enemy enemy)
    {
        targetEnemy = enemy;
    }
}
