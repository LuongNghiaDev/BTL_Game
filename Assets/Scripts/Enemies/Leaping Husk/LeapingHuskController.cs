using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LeapingHuskController : BaseEnemy
{
    [SerializeField]
    private Rigidbody2D rb;
    public HealthController healthCtrl => healthController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        wake();
        DeathControl();
    }
}
