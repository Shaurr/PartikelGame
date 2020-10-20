using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThree : EnemieController {

    [SerializeField]
    private GameObject particel;

    // initializes
    public override void Initialize() {
        base.Initialize();
        type = 2;

        GetComponent<SpriteRenderer>().color = new Color(1f, 0.584f, 0f, 1);
    }

    // called on death, spawns particelsystem
    private void Death() {

        GameObject ps = Instantiate(particel, transform.localPosition, Quaternion.identity);
        ps.GetComponent<ParticleSystem>().Play();
    }

    // search random point as target
    public override void SearchTarget() {
        float x = Random.Range(-4.8f, 4.8f);
        float y = Random.Range(-3.5f, 3.15f);
        target = new Vector3(x, y, 0);

        base.SearchTarget();
    }

    // on death call Death()
    public override void Die() {

        Death();
        base.Die();
    }
}
