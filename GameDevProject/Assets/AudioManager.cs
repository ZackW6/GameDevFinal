using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  [Header(" ------- Audio Source ----------")]
  [SerializeField] AudioSource musicSource;

  public double musicDuration;
  public double goalTime;
  public AudioClip currentclip;
  public AudioSource audioSource;
  public AudioClip background;

  private void Start()
  {
    musicSource.clip = background;
    musicSource.Play();
  }

  // private void OnPlayMusic()
  // {
  //   goalTime = AudioSettings.dspTime + 0.5;

  //   audioSource.clip = currentclip;
  //   audioSource.PlayScheduled(goalTime);

  //   musicDuration = (double)currentclip.samples / currentclip.frequency;
  //   goalTime = goalTime + musicDuration;
  // }

  // private void Update()
  // {
  //   {
  //     if (AudioSettings.dspTime > goalTime - 1)
  //     {
  //       audioSource.clip = currentclip;
  //       audioSource.PlayScheduled(goalTime);

  //       musicDuration = (double)currentclip.samples / currentclip.frequency;
  //       goalTime = AudioSettings.dspTime  + musicDuration;
  //     }
  //   }
  // }

  // private void playScheduledClip(){
  //   /* https://www.youtube.com/watch?v=3yKcrig3bU0 */
  // }
}
