using UnityEngine;

public class turn : MonoBehaviour
{
    public float rotationSpeed = 0.2f;

    private Vector2 startTouchPosition;
    private bool isSwiping = false;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    isSwiping = true;
                    break;

                case TouchPhase.Moved:
                    if (isSwiping)
                    {
                        float deltaX = touch.deltaPosition.x;
                        float rotation = deltaX * rotationSpeed;
                        transform.Rotate(0f, -rotation, 0f);
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isSwiping = false;
                    break;
            }
        }

        else if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
            isSwiping = true;
        }
        else if (Input.GetMouseButton(0) && isSwiping)
        {
            Vector2 delta = (Vector2)Input.mousePosition - startTouchPosition;
            float rotation = delta.x * rotationSpeed * Time.deltaTime * 100f;
            transform.Rotate(0f, -rotation, 0f);
            startTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isSwiping = false;
        }
    }
}