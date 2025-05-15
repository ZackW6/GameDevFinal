using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  [Header(" ------- Audio Source ----------")]
  [SerializeField] AudioSource musicSource;

  public double musicDuration;
  public double goalTime;
  public AudioSource[] _audioSource;
  public int audioToggle;
  public AudioClip currentclip;

  // public AudioClip background;

  private void Start()
  {
    OnPlayMusic();
  }

  private void OnPlayMusic()
  {
    goalTime = AudioSettings.dspTime + 0.5;

     _audioSource[audioToggle].clip = currentclip;
     _audioSource[audioToggle].PlayScheduled(goalTime);

    musicDuration = (double)currentclip.samples / currentclip.frequency;
    goalTime = goalTime + musicDuration;
  }

  private void Update()
  {
    if (AudioSettings.dspTime > goalTime - 1){
      playScheduledClip();
    }
    // {
    //   if (AudioSettings.dspTime > goalTime - 1)
    //   {
    //     audioSource.clip = currentclip;
    //     audioSource.PlayScheduled(goalTime);

    //     musicDuration = (double)currentclip.samples / currentclip.frequency;
    //     goalTime = AudioSettings.dspTime  + musicDuration;
    //   }
    // }
  }

  private void playScheduledClip(){
    _audioSource[audioToggle].clip = currentclip;
    _audioSource[audioToggle].PlayScheduled(goalTime);

  musicDuration = (double)currentclip.samples / currentclip.frequency;
        goalTime =goalTime  + musicDuration;
    audioToggle = 1-audioToggle;

  }

  public void setCurrentClip (AudioClip Clip){
    Clip = currentclip;
  }
  
}
