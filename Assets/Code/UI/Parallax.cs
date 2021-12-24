using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;

    private Transform[] _backgrounds;
    private float[] _parallaxSpeeds;
    private Vector3[] _startPosition;


    private void Awake()
    {
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
        for (int i = 0; i < transform.childCount; i++)
        {
            var newPosition = Mathf.Repeat(Time.time * scrollSpeed / _parallaxSpeeds[i], 30f);
            _backgrounds[i].position = _startPosition[i] + Vector3.right * newPosition;
        }
    }

    public void SetParallaxBackground(Sprite background)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _backgrounds[i].GetComponent<SpriteRenderer>().sprite = background;
            _backgrounds[i].GetChild(0).GetComponent<SpriteRenderer>().sprite = background;
        }
    }
}
