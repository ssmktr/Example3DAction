using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour {

    protected Animator avatar;
    float _LastAttackTime, _LastSkillTime, _LastDashTime;
    public bool _IsAttacking = false;
    public bool _IsDashing = false;

    float h, v;

	void Start () {
        avatar = GetComponent<Animator>();	
	}

    public void OnStickChanged(Vector2 stickPos)
    {
        h = stickPos.x;
        v = stickPos.y;
    }

    private void Update()
    {
        if (avatar)
        {
            float back = 1f;
            if (v < 0f)
                back -= 1f;

            avatar.SetFloat("Speed", (h * h + v * v));

            Rigidbody rigid = GetComponent<Rigidbody>();
            if (rigid != null)
            {
                Vector3 speed = rigid.velocity;
                speed.x = 4 * h;
                speed.y = 4 * v;
                rigid.velocity = speed;
                if (h != 0f && v != 0f)
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));
                }
            }
        }
    }

    public void OnAttackDown()
    {
        _IsAttacking = true;
        avatar.SetBool("Combo", true);
        StartCoroutine(StartAttact());
    }

    public void OnAttackUp()
    {
        avatar.SetBool("Combo", false);
        _IsAttacking = false;
    }

    IEnumerator StartAttact()
    {
        _LastAttackTime = Time.time;
        while (_IsAttacking)
        {
            avatar.SetTrigger("AttackStart");
            yield return null;
        }
    }

    public void OnSkillDown()
    {
        if (Time.time - _LastSkillTime > 1f)
        {
            avatar.SetBool("Skill", true);
            _LastSkillTime = Time.time;
        }
    }

    public void OnSkillUp()
    {
        avatar.SetBool("Skill", false);
    }

    public void OnDashDown()
    {
        if (Time.time - _LastDashTime > 1f)
        {
            _LastDashTime = Time.time;
            _IsDashing = true;
            avatar.SetTrigger("Dash");
        }
    }

    public void OnDashUp()
    {
        _IsDashing = false;
    }
}
