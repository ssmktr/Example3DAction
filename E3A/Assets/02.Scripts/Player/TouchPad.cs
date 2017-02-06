using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TouchPad : MonoBehaviour {

    RectTransform _TouchPad;
    int _TouchId = -1;
    Vector3 _StartPos = Vector3.zero;
    bool _IsButtonPressed = false;

    public float _DragRadius = 60f;
    public PlayerMovement _Player;

	void Start () {
        _TouchPad = GetComponent<RectTransform>();
        _StartPos = _TouchPad.position;
	}

    public void ButtonDown()
    {
        _IsButtonPressed = true;
    }

    public void ButtonUp()
    {
        _IsButtonPressed = false;
        HandleInput(_StartPos);
    }

    private void FixedUpdate()
    {
        HandleTouchInput();

#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER
        HandleInput(Input.mousePosition);
#endif
    }

    void HandleTouchInput()
    {
        int i = 0;
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                i++;
                Vector3 touchPos = new Vector3(touch.position.x, touch.position.y);

                if (touch.phase == TouchPhase.Began)
                {
                    if (touch.position.x <= (_StartPos.x + _DragRadius))
                    {
                        _TouchId = i;
                    }
                }

                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    if (_TouchId == i)
                    {
                        HandleInput(touchPos);
                    }
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    if (_TouchId == i)
                    {
                        _TouchId = -1;
                    }
                }
            }
        }
    }

    void HandleInput(Vector3 input)
    {
        if (_IsButtonPressed)
        {
            Vector3 diffVector = (input - _StartPos);

            if (diffVector.sqrMagnitude > _DragRadius * _DragRadius)
            {
                diffVector.Normalize();

                _TouchPad.position = _StartPos + diffVector * _DragRadius;
            }
            else
            {
                _TouchPad.position = input;
            }
        }
        else
        {
            _TouchPad.position = _StartPos;
        }

        Vector3 diff = _TouchPad.position - _StartPos;
        Vector2 normDiff = new Vector3(diff.x / _DragRadius, diff.y / _DragRadius);

        if (_Player != null)
        {
            _Player.OnStickChanged(normDiff);
        }
    }
}
