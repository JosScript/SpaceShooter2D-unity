using UnityEngine;

public class WinAnimation : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float size = 1f;
    private float _time;
    private Vector3 _previousPosition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _previousPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        _time += Time.deltaTime * speed;
        _time %= 2 * Mathf.PI;

        // Calculate new position using parametric equations
        var x = size * Mathf.Cos(_time) / (1 + Mathf.Pow(Mathf.Sin(_time), 2)) - 4f;
        var y = size * (Mathf.Sin(_time) * Mathf.Cos(_time)) / (1 + Mathf.Pow(Mathf.Sin(_time), 2));
        var newPosition = new Vector3(x, y, 0);

        // Calculate direction of movement
        var direction = newPosition - _previousPosition;

        // Update position and rotation
        transform.position = newPosition;
        _previousPosition = newPosition;

        // Rotate the object to face the direction of movement
        if (!(direction.sqrMagnitude > 0.001f)) return;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
