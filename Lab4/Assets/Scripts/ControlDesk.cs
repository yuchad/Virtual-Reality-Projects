using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDesk : MonoBehaviour
{

    public Transform SelectedObject;
	public GameObject cylSelected;
	public Material[] myMaterial;
	private MeshRenderer cylMesh;

	void start(){
		
	}

    public void SetX(float x)
    {
        Vector3 pos = SelectedObject.localPosition;
        pos.x = x;
        SelectedObject.localPosition = pos;
    }
    public void SetY(float y)
    {
        Vector3 pos = SelectedObject.localPosition;
        pos.y = y;
        SelectedObject.localPosition = pos;
    }
    public void SetZ(float z)
    {
        Vector3 pos = SelectedObject.localPosition;
        pos.z = z;
        SelectedObject.localPosition = pos;
    }

	public void setColour(){
		bool isSet;
	
			cylMesh = cylSelected.GetComponent<MeshRenderer> ();
			cylMesh.sharedMaterial = myMaterial [0];

	}

}
