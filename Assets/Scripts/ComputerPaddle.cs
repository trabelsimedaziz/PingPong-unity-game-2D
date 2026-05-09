using UnityEngine;

public class ComputerPaddle : Paddle
{
    public float topLimit = 3.8f;
    public float bottomLimit = -3.8f;
    public GameObject ballObject;

    private void FixedUpdate()
    {
        if (ballObject == null) return;

        Rigidbody2D ballRb = ballObject.GetComponent<Rigidbody2D>();
        if (ballRb == null) return;

        float targetY;

        if (ballRb.linearVelocity.x > 0.0f)
        {
            targetY = ballObject.transform.position.y;
        }
        else
        {
            targetY = 0f;
        }

        targetY = Mathf.Clamp(targetY, bottomLimit, topLimit);

        float newY = Mathf.MoveTowards(
            transform.position.y,
            targetY,
            speed * Time.fixedDeltaTime
        );

        newY = Mathf.Clamp(newY, bottomLimit, topLimit);

        _rigidbody.MovePosition(new Vector2(transform.position.x, newY));
    }
}