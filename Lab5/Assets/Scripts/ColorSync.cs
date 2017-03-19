using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ColorSync : NetworkBehaviour {


    // SyncVars have to be updated where the authority lies. In this case, the authority of ball lies in the server.
    [SyncVar]
    public int NumHit = 0;

    // When you call a hook from a syncVar, instead of updating the value in all the other clients, the game calls a method
    [SyncVar(hook = "ApplyColor")]
    public Color myColor = Color.white;


    private void Start()
    {
        // The hook for a syncvar doesn't execute at the start of the object's life (Because the var hasn't changed), so we do it ourself.
        ApplyColor(myColor);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Disregard collisions on the clients.
        if (isServer)
        {
            myColor = Random.ColorHSV(0,1,1,1,0.8f,0.8f);
            NumHit++;
            RpcYell();
        }
    }
    // Rpcs need to have their names begin with Rpc... Question: Does this execute on the client, the server, or both?
    [ClientRpc]
    void RpcYell()
    {
        print(" Oh! I've been hit "+ NumHit + " times!");
    }
    //The parameter is the syncvar's new value.
    void ApplyColor(Color newColor)
    {
        GetComponent<MeshRenderer>().material.color = newColor;
        //This doesn't happen automatically.
        myColor = newColor;
    }

   
}
