using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles all audio-related functionality on the client. Uses an object pool for sound effects.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public sealed class AudioManager : Singleton<AudioManager>
{
    #region Fields

    /// <summary>
    /// The AudioSource to play the music.
    /// </summary>
    private AudioSource musicSource = null;

    public AudioSource MusicSource
    {
        get
        {
            if (musicSource == null)
                musicSource = GetComponent<AudioSource>();

            return musicSource;
        }
    }

    /// <summary>
    /// The sounds being played. Objects in this list will be reused if they're not currently playing a sound.
    /// Otherwise, new objects will be added to accomodate more sounds.
    /// <para>All sounds in this list are parented to this object.</para>
    /// </summary>
    private List<AudioSource> SoundPool = new List<AudioSource>();

    private float musicVolume = .5f;
    private float soundVolume = .5f;

    /// <summary>
    /// The time, in seconds, before clearing all sounds if no sounds have been played.
    /// If the longest sound is longer than this value, adjust it.
    /// </summary>
    private const float SOUND_CLEAR_TIMER = 10f;
    private float LastPlayedSound = 0f;

    #endregion

    #region Properties

    /// <summary>
    /// The global volume of all music.
    /// </summary>
    public float MusicVolume
    {
        get { return musicVolume; }
        set { ChangeMusicVolume(value); }
    }

    /// <summary>
    /// The global volume of all sounds.
    /// </summary>
    public float SoundVolume
    {
        get { return soundVolume; }
        set { soundVolume = value; }
    }

    #endregion

    #region Unity Methods

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        //Check if we should clear the sound pool
        if (Time.time >= LastPlayedSound)
        {
            //Remove and destroy all sounds
            for (int i = 0; i < SoundPool.Count; i++)
            {
                AudioSource source = SoundPool[i];
                source.Stop();

                SoundPool.RemoveAt(i);
                Destroy(source.gameObject);
                i--;
            }

            //Refresh the timer again
            LastPlayedSound = Time.time + SOUND_CLEAR_TIMER;
        }
    }

    #endregion

    private void ChangeMusicVolume(float m_musicVolume)
    {
        //AudioSource's volume goes from 0 to 1
        musicVolume = Mathf.Clamp(m_musicVolume, 0f, 1f);
        MusicSource.volume = MusicVolume;
    }

    private void ChangeSoundVolume(float m_soundVolume)
    {
        //AudioSource's volume goes from 0 to 1
        m_soundVolume = Mathf.Clamp(m_soundVolume, 0f, 1f);
    }

    /// <summary>
    /// Plays a music clip.
    /// </summary>
    /// <param name="music">The music to play.</param>
    /// <param name="volume">The relative volume to play the clip at.</param>
    /// <param name="replayIfSame">If true, will play the clip even if it's the same music is currently being played.</param>
    public void PlayMusic(AudioClip music, float volume = 1f, bool replayIfSame = false)
    {
        if (replayIfSame == false && MusicSource.clip == music && MusicSource.isPlaying == true) return;

        MusicSource.clip = music;
        //Multiply this volume by the global music volume to get a relative volume
        MusicSource.volume = volume * musicVolume;
        MusicSource.Play();
    }

    public void PauseMusic()
    {
        MusicSource.Pause();
    }

    public void ResumeMusic()
    {
        MusicSource.UnPause();
    }

    public void StopMusic()
    {
        MusicSource.Stop();
    }

    /// <summary>
    /// Plays a sound clip.
    /// </summary>
    /// <param name="sound">The sound to play.</param>
    /// <param name="volume">The relative volume to play the clip at.</param>
    public void PlaySound(AudioClip sound, float volume = 1f)
    {
        AudioSource source = FindNextAvailableSoundSource();

        source.clip = sound;
        //Multiply this volume by the global sound volume to get a relative volume
        source.volume = volume * SoundVolume;
        source.Play();

        //Update when the last sound was played
        LastPlayedSound = Time.time + SOUND_CLEAR_TIMER;
    }

    /// <summary>
    /// Finds the next available sound to play and creates one if necessary.
    /// </summary>
    /// <returns>An available AudioSource not currently playing any sound.</returns>
    private AudioSource FindNextAvailableSoundSource()
    {
        //Check if any existing sources are not playing anything
        for (int i = 0; i < SoundPool.Count; i++)
        {
            //If so, return it
            if (SoundPool[i].isPlaying == false) return SoundPool[i];
        }

        //All sources are playing sounds, so create a new sound source
        GameObject newSound = new GameObject(name + " (Sound Instance)");
        AudioSource newSource = newSound.AddComponent<AudioSource>();
        newSound.transform.SetParent(transform);
        SoundPool.Add(newSource);

        return newSource;
    }
}
