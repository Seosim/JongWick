using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParticle : MonoBehaviour
{
    private Enemy mEnemy;
    private ParticleSystem mParticleSystem;

    void Start()
    {
        mEnemy = GetComponentInParent<Enemy>();
        mEnemy.aHit += HitParticle;

        mParticleSystem = GetComponent<ParticleSystem>();
    }

    private void HitParticle()
    {
        mParticleSystem.Play();
    }
}
