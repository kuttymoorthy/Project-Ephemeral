using DG.Tweening;
using UnityEngine;

namespace Ephemeral.ScriptsOnly.Scripts
{
    public class AnimationHandler : MonoBehaviour
    {
        public float zoomDuration = 2.0f;
        public float zoomScale = 1.5f;

        private Transform _transform;
        private Vector3 _initialScale;

        private void Awake()
        {
            _transform = transform;
            _initialScale = _transform.localScale;
        }

        private void OnEnable()
        {
            _transform.localScale = Vector3.zero;
            _transform.DOScale(_initialScale * zoomScale, zoomDuration)
                .SetEase(Ease.OutBack);
        }
    }
}