using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScaleSync : NetworkBehaviour {

    
    // When you call a hook from a syncVar, instead of updating the value in all the other clients, the game calls a method
    [SyncVar(hook = "ApplyScale")]
    public Vector3 scale = new Vector3(1,1,1);
   
    private void OnCollisionEnter(Collision collision) {
        //Disregard collisions on the clients.
        if (isServer) {
            scale *= Random.Range(0.5f, 1.5f);

        }
    }  
 
    //The parameter is the syncvar's new value.
    void ApplyScale(Vector3 newScale) {
        GetComponent<Transform>().localScale = newScale;
        scale = newScale;
    }
}
