using Unity.Mathematics;
using UnityEngine;

public class Dagger : MonoBehaviour {
    [SerializeField] private float daggerSpeed = 70f;
    private Enemy targetEnemy;  // To keep track of the targeted enemy
    private bool isMoving = false;

    [SerializeField] private ParticleSystem hitVfx;



    void Start() {
        // Start moving the dagger
        isMoving = true;
    }

    void Update() {

        transform.LookAt(targetEnemy.transform.position);
        if (isMoving && targetEnemy != null) {
            // Move towards the enemy
            float step = daggerSpeed * Time.deltaTime;  // Adjust speed as needed
            transform.position = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, step);

            // Check if the dagger reaches the enemy
            if (transform.position == targetEnemy.transform.position) {
                // Destroy the enemy

                Instantiate(hitVfx, transform.position, quaternion.identity);
                if (targetEnemy.enemyType == Enemy.EnemyType.alive) {
                    targetEnemy.Death();
                }
                else if (targetEnemy.enemyType == Enemy.EnemyType.notAlive) {
                    targetEnemy.Die();
                }
                // Destroy the dagger
                Destroy(gameObject);
            }
        }
    }

    // Method to set the enemy target when dagger is thrown
    public void SetTarget(Enemy enemy) {
        targetEnemy = enemy;
    }
}
