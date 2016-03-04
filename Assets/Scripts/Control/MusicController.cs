using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

class MusicTrack
{
  public AudioSource track;
  private float startFadeVolume = 0.0f;
  private float endFadeVolume = 0.0f;
  private float startFadeTime = 0.0f;
  private float endFadeTime = 0.0f;

  public MusicTrack(AudioSource t)
  {
    track = t;
    track.volume = 0.0f;
  }

  public void ProgramFadeVolume(float delay, float targetVolume)
  {
    startFadeVolume = track.volume;
    endFadeVolume = targetVolume;
    startFadeTime = track.time;
    endFadeTime = startFadeTime + delay;
  }

  public void doFade()
  {
    if (track.time > endFadeTime)
    {
      track.volume = endFadeVolume;
    }
    else
    {
      track.volume = (endFadeVolume - startFadeVolume) * Mathf.SmoothStep(0.0f, 1.0f, (track.time - startFadeTime) / (endFadeTime - startFadeTime)) + startFadeVolume;
    }
  }
}

public class MusicController : MonoBehaviour {


  [Header("Music tracks")]
  [Tooltip("Standalone music track")]
  public AudioSource baseTrack;
  [Tooltip("Music track played when the nozzle is actiavted")]
  public AudioSource bassTrack;
  [Tooltip("Music track played when the shooter have fire on")]
  public AudioSource rhythmicTrack;

  [Header("Parameters")]
  public float fadeDelay = 0.5f;


  private Dictionary<string, MusicTrack> tracks = new Dictionary<string,MusicTrack>();



	// Use this for initialization
	void Start () {
    Assert.IsNotNull(baseTrack);
    Assert.IsNotNull(bassTrack);
    Assert.IsNotNull(rhythmicTrack);

    tracks["base"] = new MusicTrack(baseTrack);
    tracks["bass"] = new MusicTrack(bassTrack);
    tracks["rhythmic"] = new MusicTrack(rhythmicTrack);

    foreach (KeyValuePair<string, MusicTrack> t in tracks)
    {
      t.Value.track.Play();
    }

    tracks["base"].ProgramFadeVolume(fadeDelay, 1.0f);

    bassTrack.timeSamples = bassTrack.timeSamples;
    rhythmicTrack.timeSamples = bassTrack.timeSamples;

	}


  public void turbo(bool on)
  {
    if (on)
    {
      tracks["bass"].ProgramFadeVolume(fadeDelay, 1.0f);
    }
    else
    {
      tracks["bass"].ProgramFadeVolume(fadeDelay, 0.0f); 
    }
  }

  public void fire(bool on)
  {
    if (on)
    {
      tracks["rhythmic"].ProgramFadeVolume(fadeDelay, 1.0f);
    }
    else
    {
      tracks["rhythmic"].ProgramFadeVolume(fadeDelay, 0.0f); 
    }
  }


	// Update is called once per frame
	void Update () { 
    foreach (KeyValuePair<string, MusicTrack> t in tracks)
    {
      t.Value.doFade();
    }
	}
}
