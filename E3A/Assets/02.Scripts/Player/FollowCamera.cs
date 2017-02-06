using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

    public Transform Target;
    float _Height = 4f;
    float _Dist = 7f;

    private void LateUpdate()
    {
        transform.position = Target.position + Vector3.up * _Height - Vector3.forward * _Dist;
    }
}
