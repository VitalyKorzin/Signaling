using UnityEngine;

public class Thief : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Translate(_speed * Input.GetAxis("Horizontal") * Time.deltaTime * transform.right);
    }
}