using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOne : EnemieController {

    //initializes
    public override void Initialize() {
        base.Initialize();
        type = 0;
        GetComponent<SpriteRenderer>().color = new Color(0.12f, 0.88f, 0.95f, 1);

        //start particelsystem (shooting)
        GetComponent<ParticleSystem>().Play();

    }

    // searches random point as target
    public override void SearchTarget() {
        float x = Random.Range(-4.8f, 4.8f);
        float y = Random.Range(-3.5f, 3.15f);
        target = new Vector3(x, y, 0);

        base.SearchTarget();
    }




}
