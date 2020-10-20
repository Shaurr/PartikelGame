using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : ItemController {

    // on collision with player, add 1 health
    protected override void Action() {

        playerController.AddHealth(1);
        base.Action();
    }

}
