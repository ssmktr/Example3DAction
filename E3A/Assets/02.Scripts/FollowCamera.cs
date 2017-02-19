using System.Collections;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    float _Dist = 10f;
    float _Height = 5f;

    public Transform Target;

    private void LateUpdate()
    {
        if (Target != null)
        {
            transform.LookAt(Target);
            transform.position = Target.position + Vector3.up * _Height - Vector3.forward * _Dist;
        }
    }
}
