using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammer_particle : MonoBehaviour
{
    public ParticleSystem slam;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().speed = Random.Range(0.5f, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSlam()
    {
        slam.Play();
    }
}
