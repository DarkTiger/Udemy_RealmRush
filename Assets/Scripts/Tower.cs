﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] ParticleSystem projectileParticle;
    [SerializeField] float attackRange = 10f;
    public Waypoint baseWaypoint;

    Transform targetEnemy;


    void Update()
    {
        SetTargetEnemy();

        if (targetEnemy)
        {
            FireAtEnemy();
        }
        else
        {
            Shoot(false);
        }
    }

    void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if (sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach (EnemyDamage testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }

        targetEnemy = closestEnemy;
    }

    private Transform GetClosest(Transform transformA, Transform transformB)
    {
        var distToA = Vector3.Distance(transform.position, transformA.position);
        var distToB = Vector3.Distance(transform.position, transformB.position);

        if (distToA < distToB)
        {
            return transformA;
        }

        return transformB;
    }

    void FireAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.transform.position, transform.position);

        if (distanceToEnemy <= attackRange)
        {
            objectToPan.LookAt(targetEnemy);
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }
}
