using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimAddEvent : MonoBehaviour
{
   public List<AudioClip> slowSteps = new List<AudioClip>(); //tu peux add autant de sons de pas que tu veux dans l inspector
    public List<AudioClip> fastSteps = new List<AudioClip>(); // pareil
    public void SlowSteps() 
    {
        int Slowsteps = Random.Range(0, slowSteps.Count); // fais un random des pas
        AudioManager.instance.PlaySoundFXClip(slowSteps[Slowsteps], transform, 0.5f); // play le son avec l AudioManager
    }
    public void FastSteps() 
    {
        int Faststeps = Random.Range(0, slowSteps.Count);
        AudioManager.instance.PlaySoundFXClip(slowSteps[Faststeps], transform, 0.5f);
    }
    //si jamais tu veux reprendre le script ta besoin de mettre l AudioManager sur un empty + mettre ce script sur l obj qui a les anims ici le player + dans l anim cliquer sur add event et dedans tu rajoute la fonction du son
}
