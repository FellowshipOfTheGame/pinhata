using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sounds
{
    MenuSelection,
    PlayerAttack_1,
    PlayerAttack_2,
    PlayerAttack_3,
    PlayerTakeDamage,
    EnemyAttack,
    EnemyTakeDamage
}

public enum Music
{
    MexicanThriller
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioClip[] musics;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource musicSource;

    public static SoundManager Instance { get; set; } = null;

    [Range(0f, 1f)]
    public float Volume;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayClip(Sounds sound)
    {
        audioSource.PlayOneShot(clips[(int)sound], Volume);
    }

    public void PlayClip(Sounds sound, AudioSource source)
    {
        source.PlayOneShot(clips[(int)sound], Volume);
    }

    public void PlayMusic(Music music)
    {
        musicSource.PlayOneShot(musics[(int)music], Volume);
    }

    public void SetVolume(float value)
    {
        Volume = value;
    }

}
