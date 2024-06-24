using UnityEngine;

public class CurveMovement : MonoBehaviour
{
    public Transform target; // Target position to move to
    public AnimationCurve horizontalCurve; // Curve for the horizontal movement
    public AnimationCurve verticalCurve; // Curve for the vertical jump
    public float duration = 1f; // Duration of the jump

    private Vector3 startPosition;
    private Vector3 MiddlePoint;
    private Vector3 endPosition;
    private float elapsedTime = 0f;
    private bool isJumping = false;

    void Start()
    {
        startPosition = transform.position;
        endPosition = target.position;
        StartJump();
    }

    void Update()
    {
        if (isJumping)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Evaluate the curves
            float horizontalT = horizontalCurve.Evaluate(t);
            float verticalT = verticalCurve.Evaluate(t);

            // Interpolate the position based on the curves
            Vector3 horizontalPosition = Vector3.Lerp(startPosition, endPosition, horizontalT);
            float verticalPosition = Mathf.Lerp(0f, 1f, verticalT);

            transform.position = new Vector3(horizontalPosition.x, startPosition.y + verticalPosition, horizontalPosition.z);

            if (t >= 1f)
            {
                isJumping = false;
            }
        }
    }

    public void StartJump()
    {
        startPosition = transform.position;
        endPosition = target.position;
        elapsedTime = 0f;
        isJumping = true;
    }
}