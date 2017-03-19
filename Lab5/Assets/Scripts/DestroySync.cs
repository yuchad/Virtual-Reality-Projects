using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DestroySync : NetworkBehaviour {

   [SyncVar]
    public int collisions = 0;
    public int maxHit = 8;
    

    private void OnCollisionEnter(Collision collision) {
        //Disregard collisions on the clients.
        if (isServer) {
             collisions++;
        }
    }

     void Update() {
        DestroyAfterN();
    }


    //The parameter is the syncvar's new value.

    void DestroyAfterN() {
        if (collisions >= maxHit && isServer) {
           NetworkServer.Destroy(gameObject);
           collisions = 0;
        }
    }
}
