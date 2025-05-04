using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField] private float speed;
    private  RawImage _image;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _image = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        _image.uvRect = new Rect(_image.uvRect.position + new Vector2(speed, 0) * Time.deltaTime, _image.uvRect.size);
    }
}
