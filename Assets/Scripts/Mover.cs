using UnityEngine;

public class Mover : MonoBehaviour
{
    private static string Horizontal = "Horizontal";
    private static string Vertical = "Vertical";

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        float speed = 2f;
        float direction = Input.GetAxis(Vertical);
        transform.Translate(Vector3.forward*direction*speed*Time.deltaTime);
    }

    private void Rotate()
    {
        float rotateSpeed = 30f;
        float rotateDirection = Input.GetAxis(Horizontal);
        transform.Rotate (Vector3.up*rotateDirection*rotateSpeed*Time.deltaTime);
    }
}
