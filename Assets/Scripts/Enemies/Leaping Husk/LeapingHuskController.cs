using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapingHuskController : BaseEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        wake();
        DeathControl();
    }
}
