﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deathParticlePrefab;
    [SerializeField] AudioClip enemyHitSFX;
    [SerializeField] AudioClip enemyDeathSFX;
    AudioSource myAudioSource;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }


    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if (hitPoints <= 0)
        {
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        myAudioSource.PlayOneShot(enemyHitSFX);
        hitPoints--;
        hitParticlePrefab.Play();
    }

    void KillEnemy()
    {
        var vfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        Destroy(vfx.gameObject, vfx.main.duration);
        AudioSource.PlayClipAtPoint(enemyDeathSFX, Camera.main.transform.position);

        Destroy(gameObject);
    }
}
