using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Super : ItemController {

    // on collision with player, activate super
    protected override void Action() {

        playerController.SuperAttack();

        base.Action();
    }
}
