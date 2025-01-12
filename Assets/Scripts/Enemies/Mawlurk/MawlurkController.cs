using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MawlurkController : BaseEnemy
{

    [Header("Shoot Section")]
    [SerializeField] Transform shotPoint1;
    [SerializeField] GameObject projectile1;
    [SerializeField] Transform shotPoint2;
    [SerializeField] GameObject projectile2;
    [SerializeField] Transform shotPoint3;
    [SerializeField] GameObject projectile3;
    [SerializeField] Transform shotPoint4;
    [SerializeField] GameObject projectile4;
    [SerializeField] Transform shotPoint5;
    [SerializeField] GameObject projectile5;
    [SerializeField] Transform shotPoint6;
    [SerializeField] GameObject projectile6;

    private void Update()
    {
        wake();
        DeathControl();
    }

    public void shootEvent()
    {
        // Đạn 1
        GameObject instance1 = (GameObject)Instantiate(projectile1, shotPoint1.position, shotPoint1.rotation);
        Vector2 velocity1 = new Vector2(5 * (-1), 5);
        instance1.GetComponent<Rigidbody2D>().velocity = velocity1;

        // Đạn 2
        GameObject instance2 = (GameObject)Instantiate(projectile2, shotPoint2.position, shotPoint2.rotation);
        Vector2 velocity2 = new Vector2(10 * (-1), 10);
        instance2.GetComponent<Rigidbody2D>().velocity = velocity2;

        // Đạn 3
        GameObject instance3 = (GameObject)Instantiate(projectile3, shotPoint3.position, shotPoint3.rotation);
        Vector2 velocity3 = new Vector2(15 * (-1), 15);
        instance3.GetComponent<Rigidbody2D>().velocity = velocity3;

        // Đạn 4
        GameObject instance4 = (GameObject)Instantiate(projectile4, shotPoint4.position, shotPoint4.rotation);
        Vector2 velocity4 = new Vector2(15 * 1, 15);
        instance4.GetComponent<Rigidbody2D>().velocity = velocity4;

        // Đạn 5
        GameObject instance5 = (GameObject)Instantiate(projectile5, shotPoint5.position, shotPoint5.rotation);
        Vector2 velocity5 = new Vector2(10 * 1, 10);
        instance5.GetComponent<Rigidbody2D>().velocity = velocity5;

        // Đạn 6
        GameObject instance6 = (GameObject)Instantiate(projectile6, shotPoint6.position, shotPoint6.rotation);
        Vector2 velocity6 = new Vector2(5 * 1, 5);
        instance6.GetComponent<Rigidbody2D>().velocity = velocity6;
    }

}
