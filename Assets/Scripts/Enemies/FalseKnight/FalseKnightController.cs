using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseKnightController : BaseEnemy
{
    public HealthController healthCtrl => healthController;
    private AudioManager audioManager;

    private void Update()
    {
        wake();
        DeathControl();
    }

}
