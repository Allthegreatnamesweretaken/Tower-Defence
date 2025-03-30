using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign the bullet prefab in the inspector
    public Transform firePoint; // Assign the fire point in the inspector
    public float fireRate = 1f; // Time between shots
    public float bulletSpeed = 70f; // Speed of the bullet
    public float bulletExplosionRadius = 0f; // Explosion radius of the bullet

    private float fireCountdown = 0f;

    // Update is called once per frame
    void Update()
    {
        fireCountdown -= Time.deltaTime;

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }
    }

    void Shoot()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return;

        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = bulletGO.GetComponent<Bullet>();

            if (bullet != null)
            {
                bullet.Seek(nearestEnemy.transform);
                bullet.speed = bulletSpeed;
                bullet.explosionRadius = bulletExplosionRadius;
            }
        }
    }
}
