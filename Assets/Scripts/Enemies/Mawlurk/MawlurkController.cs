using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MawlurkController : BaseEnemy
{

    private void Update()
    {
        wake();
        DeathControl();
    }
}
