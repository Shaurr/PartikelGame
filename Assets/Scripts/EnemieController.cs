using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieController : MonoBehaviour {


    protected GameController gc;
    protected int type;
    protected Vector3 target;
    protected Vector3 distance;
    protected Vector3 step;
    protected bool initialized = false;

    // if initialized call move every update
    public virtual void Update() {
        if (initialized) {
            Move();
        }
    }

    // initializes, called on start
    public virtual void Initialize() {

        gc = FindObjectOfType<GameController>();
        SearchTarget();
        initialized = true;
    }

    // controlls movement 
    public virtual void Move() {

        // calculate directionvector with length 1 and distance
        if (distance.magnitude == 0) {
            distance = target - transform.position;
            step = distance.normalized;
        }

        // move enemie in direction step, acording to passed time
        transform.Translate(step * Time.deltaTime);
        distance -= (step * Time.deltaTime);
        if (distance.magnitude < 0.02) {
            SearchTarget();
        }


    }

    // search new target
    public virtual void SearchTarget() {
        distance = new Vector3(0, 0, 0);
    }

    // calle don hit, deletes enemie
    public virtual void Die() {
        gc.DeleteEnemie(type);
        Destroy(this.gameObject);
    }

    // on collision, change target
    public virtual void OnCollisionEnter2D(Collision2D collision) {
        SearchTarget();
    }

}
