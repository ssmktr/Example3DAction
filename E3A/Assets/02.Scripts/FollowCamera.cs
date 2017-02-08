using System.Collections;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    float _Dist = 10f;
    float _Height = 4f;

    public Transform Target;

    private void LateUpdate()
    {
        transform.position = Target.position + Vector3.up * _Height - Vector3.forward * _Dist;
    }
}
