using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    private ParticleSystem myParticleSystem;
    private void Awake()
    {
        myParticleSystem = GetComponent<ParticleSystem>();
    }
    private void Update(){
        if(myParticleSystem && !myParticleSystem.isPlaying){
            DestroySelf();
        }
    }

    public void DestroySelf(){
        Destroy(gameObject);
    }
}
