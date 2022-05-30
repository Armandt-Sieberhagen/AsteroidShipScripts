using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    [SerializeField] Bullets Bullet;
    [SerializeField] float TimeBetweenShots;
    [SerializeField] float BulletFireStrength = 5000;
    float CurrentSHotTime;

    private void FixedUpdate()
    {
        if (CurrentSHotTime<=Time.deltaTime)
        {
            if (Input.GetMouseButton(0))
            {
                CurrentSHotTime = TimeBetweenShots;
                ShootBullet();
            }
            
        }
        else
        {
            CurrentSHotTime -= Time.deltaTime;
        }
        
    }

    void ShootBullet()
    {
        GameObject ShipBullet = Instantiate(Bullet.gameObject, transform.position, transform.rotation);
        ShipBullet.GetComponent<Bullets>().Body.AddForce(transform.forward* BulletFireStrength);
        
    }
}
