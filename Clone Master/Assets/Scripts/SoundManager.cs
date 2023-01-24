using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Sounds")]
    public AudioSource GatesGrow;
    public AudioSource Run;
    public AudioSource Dead;
    public AudioSource BossFight;
    public AudioSource BossHit;
    public AudioSource BossDeath;
    public AudioSource LoseSound;
    public AudioSource WinSound;
    public AudioSource CoinSound;


    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
