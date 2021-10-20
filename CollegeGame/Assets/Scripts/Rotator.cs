using UnityEngine;

public class Rotator : MonoBehaviour
{
    public GameObject Hero;

    private Vector2 _lastTouchPoint;

    public void Update()
    {
        foreach(var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _lastTouchPoint = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                var rotateValue = Vector2.Distance(_lastTouchPoint, touch.position);
                
                if (_lastTouchPoint.x < touch.position.x)
                    rotateValue *= -1;

                Hero.transform.Rotate(new Vector3(0, rotateValue / 2, 0));
                _lastTouchPoint = touch.position;
            }
        }
    }
}
