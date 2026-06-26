using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerName : MonoBehaviour
{
    public Transform target;
    public RectTransform textUI;
    public Vector3 offset = new Vector3(0, 50f, 0);

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position);
        textUI.position = screenPos + offset;
    }
}
