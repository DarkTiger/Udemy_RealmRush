﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movementPeriod = 0.5f;
    [SerializeField] ParticleSystem goalParticle;


    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();

        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(movementPeriod);
        }

        SelfDestruct();
    }

    void SelfDestruct()
    {
        var vfx = Instantiate(goalParticle, transform.position, Quaternion.identity);
        Destroy(vfx.gameObject, vfx.main.duration);

        Destroy(gameObject);
    }
}
