using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Clips")]
    [SerializeField] private AudioClip bgmClip;
    [SerializeField] private AudioClip craftClip;
    [SerializeField] private AudioClip footstepClip;
    [SerializeField] private AudioClip treeChopClip;

    [Header("Volumes")]
    [Range(0f, 1f)] [SerializeField] private float bgmVolume = 0.6f;
    [Range(0f, 1f)] [SerializeField] private float sfxVolume = 1f;
    [Range(0f, 1f)] [SerializeField] private float footstepVolume = 0.7f;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private AudioSource actionLoopSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        SetupAudioSources();
        ApplyInitialSettings();
    }

    private void Start()
    {
        PlayBgm();
    }

    private void SetupAudioSources()
    {
        if (bgmSource == null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
        }

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }

        if (footstepSource == null)
        {
            footstepSource = gameObject.AddComponent<AudioSource>();
        }

        if (actionLoopSource == null)
        {
            actionLoopSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void ApplyInitialSettings()
    {
       

        bgmSource.loop = true;
        bgmSource.playOnAwake = false;
        bgmSource.volume = bgmVolume;

        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
        sfxSource.volume = sfxVolume;

        footstepSource.loop = true;
        footstepSource.playOnAwake = false;
        footstepSource.volume = footstepVolume;

        actionLoopSource.loop = true;
        actionLoopSource.playOnAwake = false;
        actionLoopSource.volume = sfxVolume;
    }

    public void PlayBgm()
    {
        if (bgmClip == null)
        {
            return;
        }

        if (bgmSource.clip != bgmClip)
        {
            bgmSource.clip = bgmClip;
        }

        if (!bgmSource.isPlaying)
        {
            bgmSource.Play();
        }
    }

    public void StopBgm()
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }

    public void PlayCraftSfx()
    {
        PlayOneShot(craftClip);
    }

    // Repair uses the same sound as crafting.
    public void PlayRepairSfx()
    {
        PlayCraftSfx();
    }

    // Convenience method for places where craft/repair share one event call.
    public void PlayCraftOrRepairSfx()
    {
        PlayCraftSfx();
    }

    public void StartFootstep()
    {
        if (footstepClip == null)
        {
            return;
        }

        if (footstepSource.clip != footstepClip)
        {
            footstepSource.clip = footstepClip;
        }

        if (!footstepSource.isPlaying)
        {
            footstepSource.Play();
        }
    }

    public void StopFootstep()
    {
        if (footstepSource.isPlaying)
        {
            footstepSource.Stop();
        }
    }

    public void PlayTreeChopSfx()
    {
        PlayOneShot(treeChopClip);
    }

    public void StartTreeChopLoop()
    {
        StartLoop(treeChopClip);
    }

    public void StartRepairLoop()
    {
        StartLoop(craftClip);
    }

    public void StopActionLoop()
    {
        if (actionLoopSource.isPlaying)
        {
            actionLoopSource.Stop();
        }

        actionLoopSource.clip = null;
    }

    private void StartLoop(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }

        if (actionLoopSource.clip != clip)
        {
            actionLoopSource.clip = clip;
        }

        if (!actionLoopSource.isPlaying)
        {
            actionLoopSource.Play();
        }
    }

    private void PlayOneShot(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }

        sfxSource.PlayOneShot(clip, sfxVolume);
    }
}
