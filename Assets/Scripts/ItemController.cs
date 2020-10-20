using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {
    protected PlayerController playerController;

    //on collision with player, call action
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            playerController = collision.gameObject.GetComponent<PlayerController>();
            Action();

        }
    }


    // what to do when collision with player
    protected virtual void Action() {

        Destroy(gameObject);
    }
}
