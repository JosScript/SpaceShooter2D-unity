using System.Collections;
using UnityEngine;

public class LoseAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 targetScale = new Vector3(2, 2, 2);
    [SerializeField] private float scaleUpDuration = 1f;
    [SerializeField] private float scaleDownDuration = 1f;

    private Vector3 _originalScale;
    private Coroutine _scaleCoroutine;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _originalScale = transform.localScale;
        StartScaling();
    }
    
    private void StartScaling()
    {
        if (_scaleCoroutine != null) StopCoroutine(_scaleCoroutine);
        _scaleCoroutine = StartCoroutine(ScaleRoutine());
    }
    
    private IEnumerator ScaleTo(Vector3 target, float duration)
    {
        var startScale = transform.localScale;
        var elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var t = Mathf.Clamp01(elapsedTime / duration);
            transform.localScale = Vector3.Lerp(startScale, target, t);
            yield return null; // Wait for next frame
        }

        transform.localScale = target;
    }
    
    private IEnumerator ScaleRoutine()
    {
        while (true)
        {
            yield return ScaleTo(targetScale, scaleUpDuration);
            yield return ScaleTo(_originalScale, scaleDownDuration);
        }
    }
    
    // Reset to original scale when disabled
    private void OnDisable()
    {
        transform.localScale = _originalScale;
    }
    
    // Update is called once per frame
    private void Update()
    {
        
    }
}
