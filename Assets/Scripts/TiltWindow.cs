using UnityEngine;

public class TiltWindow : MonoBehaviour
{
    public Vector2 range = new Vector2(5f, 3f);

    private Transform _mTrans;
    private Quaternion _mRot;
    private Vector2 _rot = Vector2.zero;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mTrans = transform;
        _mRot = _mTrans.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        
        float width = Screen.width / 2.0f;
        float height = Screen.height / 2.0f;
        float x = Mathf.Clamp((mousePos.x - width) / width, -1f, 1f);
        float y = Mathf.Clamp((mousePos.y - height) / height, -1f, 1f);
        _rot = Vector2.Lerp(_rot, new Vector2(x, y), Time.deltaTime * 5f);
        
        _mTrans.localRotation = _mRot * Quaternion.Euler(-_rot.y * range.y, _rot.x * range.x, 0f);
    }
}
