using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {
    private Vector3 direction;

    private Rigidbody2D rb;
    private float timer;
    private float lifetime;
    

    void Update() {
        timer -= Time.deltaTime;
        // every 0.1 seconds, slow projectile and decrease lifetime
        if (timer < 0) {
            rb.AddForce((-1 * direction) * 5);
            timer = 0.1f;
            lifetime -= 0.1f;
            // destroy when lifetime 0
            if (lifetime <= 0) {
                Destroy(gameObject);
            }
        }

    }

    // initialize start values
    public void Initialze(Vector3 _direction) {
        direction = _direction;
        rb = this.GetComponent<Rigidbody2D>();
        // start movement
        rb.AddForce(direction * 160);
        timer = 0.1f;
        lifetime = 1.5f;
    }

    // on collision with enemie or wall, destroy
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Enemie")) {
            collision.gameObject.GetComponent<EnemieController>().Die();
            Destroy(gameObject);

        }
        if (collision.gameObject.CompareTag("Wall")) {
            Destroy(gameObject);
        }
    }
}
