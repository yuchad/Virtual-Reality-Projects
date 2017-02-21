using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDesk : MonoBehaviour
{

    public Transform SelectedObject;
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

}
