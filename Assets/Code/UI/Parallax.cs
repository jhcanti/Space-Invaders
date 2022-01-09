using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;

    private Transform[] _backgrounds;
    private float[] _parallaxSpeeds;
    private Vector3[] _startPosition;
    private bool _isActive;


    private void Awake()
    {
        _isActive = false;
        _backgrounds = new Transform[transform.childCount];
        _parallaxSpeeds = new float[transform.childCount];
        _startPosition = new Vector3[transform.childCount];
        
        for (int i = 0; i < transform.childCount; i++)
        {
            _backgrounds[i] = transform.GetChild(i);
            _startPosition[i] = _backgrounds[i].position;
            _parallaxSpeeds[i] = _backgrounds[i].position.z * -1;
        }
    }

    
    private void Update()
    {
        if (!_isActive) return;
        
        for (int i = 0; i < transform.childCount; i++)
        {
            var newPosition = Mathf.Repeat(Time.time * scrollSpeed / _parallaxSpeeds[i], 10f);
            _backgrounds[i].position = _startPosition[i] + Vector3.right * newPosition;
        }
    }

    
    public void StartParallax()
    {
        _isActive = true;
    }

    public void StopParallax()
    {
        _isActive = false;
    }
}
