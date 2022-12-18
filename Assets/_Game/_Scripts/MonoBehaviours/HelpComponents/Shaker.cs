using UnityEngine;
using DG.Tweening;

public class Shaker : MonoBehaviour
{
    [System.Serializable]
    private class ShakeProperties
    {
        [SerializeField] private bool _shake;
        [SerializeField] private float _duration;
        [SerializeField] private int _vibrato = 10;
        [SerializeField] private Vector3 _strength;

        public bool Shake => _shake;
        public float Duration => _duration;
        public int Vibrato => _vibrato;
        public Vector3 Strength => _strength;
    }

    [System.Serializable]
    private class ShakePreset
    {
        [SerializeField] private ShakeProperties _positionShake;
        [SerializeField] private ShakeProperties _rotationShake;

        public ShakeProperties Position => _positionShake;
        public ShakeProperties Rotation => _rotationShake;
    }

    [SerializeField] private Transform _shakingTransform;

    [SerializeField] private ShakePreset _lightShake;
    [SerializeField] private ShakePreset _mediumShake;
    [SerializeField] private ShakePreset _heavyShake;

    private void Awake()
    {
        if (_shakingTransform == null) _shakingTransform = transform;
    }

    public void ShakeLight() => Shake(_lightShake);
    public void ShakeMedium() => Shake(_mediumShake);
    public void ShakeHeavy() => Shake(_heavyShake);

    private void Shake(ShakePreset preset)
    {
        if (preset.Position.Shake) _shakingTransform.DOShakePosition(
            preset.Position.Duration, preset.Position.Strength, preset.Position.Vibrato);

        if (preset.Rotation.Shake) _shakingTransform.DOShakeRotation(
            preset.Rotation.Duration, preset.Rotation.Strength, preset.Rotation.Vibrato);
    }
}