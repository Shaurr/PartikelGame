using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwo : EnemieController {

    protected GameObject player;

    // initializes
    public override void Initialize() {

        type = 1;
        gc = FindObjectOfType<GameController>();
        player = gc.GetCurrentPlayer();
        SearchTarget();
        initialized = true;

    }

    // sets new target ecvery update
    public override void Update() {
        if (initialized) {
            SearchTarget();
            base.Update();
        }

    }

    // sets playerposition as target
    public override void SearchTarget() {

        float x = player.transform.position.x;
        float y = player.transform.position.y;
        target = new Vector3(x, y, 0);

        base.SearchTarget();
    }

    // do nothing on collision
    public override void OnCollisionEnter2D(Collision2D collision) {

    }


}