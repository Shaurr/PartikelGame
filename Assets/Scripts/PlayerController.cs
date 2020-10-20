using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    private GameController gameController;
    public float movementSpeed;
    private Rigidbody2D rb;
    private float fireDelay = 0;
    private float fireRate = 0.3f;
    [SerializeField]
    private GameObject projectile;

    private int life = 3;
    private float protection=0;


	// initialize
	void Start () {
        
        rb = this.GetComponent<Rigidbody2D>();
        gameController = FindObjectOfType<GameController>();
	}
	
	// controlls inputs
    private void FixedUpdate() {
        // get movement input and move 
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);

        // get mouseposition and rotate player into this direction
        Vector3 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(transform.position - mouseposition, Vector3.forward);
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

        // when left mouse button pressed, fire
        if (Input.GetButton("Fire1") && (Time.fixedTime > fireDelay)){
            
            Fire(mouseposition);
        }
    }

    // activates super
    public void SuperAttack() {
        //shoot 12 projectiles around player in 30° intervals
        Vector3 direction = new Vector3(0, 1, 0);
        for (int i = 0; i < 12; i++) {
            direction = Quaternion.Euler(0, 0, 30) * direction;
            GameObject newProjectile = Instantiate(projectile, transform.position + (0.3f * direction), Quaternion.identity);
            newProjectile.GetComponent<ProjectileController>().Initialze(direction);
        }
    }

    // shoot in targets direction
    private void Fire(Vector3 target) {
        // start firedelay
        fireDelay = Time.fixedTime + fireRate;
        Vector3 direction = (target - transform.position);
        direction = new Vector3(direction.x, direction.y, 0).normalized;
        // spawn projectile
        GameObject newProjectile = Instantiate(projectile, transform.position + (0.3f *direction), Quaternion.identity);
        newProjectile.GetComponent<ProjectileController>().Initialze(direction);
    }

    // on collision with enemie, take damage
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemie")) {
            Damage(1);          
            }
    }

    // substract damage from lifes
    private void Damage(int damage) {
        if (Time.time > protection) {
            // start protection time
            protection = Time.time + 0.5f;
            // substract damage from life
            life = life - damage;
            //update ui
            gameController.UpdateLife(life);
            StartCoroutine(ShowDamage());
            // if life 0, start game over 
            if (life <= 0) {
                gameController.GameOver();
            }
        }
    }

    // chages color of player for a short time to diplay damage
    private IEnumerator ShowDamage() {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color oldColor = sr.color;
        sr.color = new Color(255, 0, 0);
        yield return new WaitForSeconds(0.1f);
        sr.color = oldColor;
    }

    // take damage from enemie projectiles
    private void OnParticleCollision(GameObject other) {
        Damage(1);
    }

    // add health
    public void AddHealth(int amount) {
        life = life + amount;
        // 3 is max health
        if (life > 3) {
            life = 3;
        }
        // update ui
        gameController.UpdateLife(life);
    }
}
