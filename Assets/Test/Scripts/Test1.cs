

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YFramework;


public class Test1 : YMonoBehaviour
{
    public Transform target; // 目标位置
    public ParticleSystem particleSystem;

    private void Start()
    {
    }

    void Update()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        int numParticlesAlive = particleSystem.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            // Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - particles[i].position).normalized;
            Vector3 direction = (target.position - particles[i].position).normalized;
            //direction = transform.TransformDirection(direction);
            particles[i].velocity = direction * 3.0f; // 调整速度
        }

        particleSystem.SetParticles(particles, numParticlesAlive);
    }
}


