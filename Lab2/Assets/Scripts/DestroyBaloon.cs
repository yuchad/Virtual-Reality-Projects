using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBaloon : MonoBehaviour {

    public int health = 1;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 10.5f);
	}
	
	public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
