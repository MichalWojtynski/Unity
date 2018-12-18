using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour {

    public int damagePerShot = 20;                  // The damage inflicted by each bullet.
    public float timeBetweenBullets = 0.15f;        // The time between each shot.

    float timer;                                    // A timer to determine when to fire.
    int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
    ParticleSystem gunParticles;                    // Reference to the particle system.
    AudioSource gunAudio;                           // Reference to the audio source.
    float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.

    public GameObject kula;
    public GameObject dymObiekt;
    ParticleSystem dym;
    Animator anim;
    void Awake()
    {
        gunParticles = GetComponent<ParticleSystem>();
        gunAudio = GetComponent<AudioSource>();
        dym = dymObiekt.GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();


    }

    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;
        if (timer >= timeBetweenBullets)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Reset the timer.
        timer = 0f;
        anim.SetTrigger("Shoot");

        // Play the gun shot audioclip.
        gunAudio.Play();
        dym.Emit(5);

        // Stop the particles from playing if they were, then start the particles.
        
        Instantiate(kula, transform.position, transform.rotation);

       
    }
    public void animback()
    {
        anim.SetTrigger("ShootBack");
    }
}
