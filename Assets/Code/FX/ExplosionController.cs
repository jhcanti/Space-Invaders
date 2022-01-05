using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;


    public void Create(Vector3 position)
    {
        var explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        Destroy(explosion, 2f);
    }
}
