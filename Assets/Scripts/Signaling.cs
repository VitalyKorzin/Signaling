using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signaling : MonoBehaviour
{
    private readonly float _volumeChangeSpeed = 0.003f;
    private readonly float _maximumVolume = 1f;
    private readonly float _minimumVolume = 0f;
    private AudioSource _audioSource;
    private float _currentVolume;
    private bool _isThiefInHouse;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    private void Start()
    {
        _currentVolume = _minimumVolume;
        SetVolume(_currentVolume);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Thief thief))
        {
            _isThiefInHouse = true;
            StartCoroutine(IncreaseVolume());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Thief thief))
        {
            _isThiefInHouse = false;
            StartCoroutine(DecreaseVolume());
        }
    }

    private IEnumerator IncreaseVolume()
    {
        while (_currentVolume < _maximumVolume && _isThiefInHouse)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, _maximumVolume, _volumeChangeSpeed);
            SetVolume(_currentVolume);
            yield return null;
        }
    }

    private IEnumerator DecreaseVolume()
    {
        while (_currentVolume > _minimumVolume && _isThiefInHouse == false)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, _minimumVolume, _volumeChangeSpeed);
            SetVolume(_currentVolume);
            yield return null;
        }
    }

    private void SetVolume(float volume) => _audioSource.volume = volume;
}