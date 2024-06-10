using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance {  get; private set; }
    [SerializeField] private AudioClip addPlayer;
    [SerializeField] private AudioClip diePlayer;
    [SerializeField] private AudioClip canonShot;
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip loose;
    [SerializeField] private AudioClip reachCastle;
     AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
    public void AddPlayer()
    {
        PlaySound(addPlayer);
    }
    public void DiePlayer()
    {
        PlaySound(diePlayer);
    }
    public void CanonShot()
    {
        PlaySound(canonShot);
    }
    public void Win()
    {
        PlaySound(win);
    }
    public void Loose()
    {
        PlaySound(loose);
    }
    public void ReachCastle()
    {
        PlaySound(reachCastle);
    }




}
