using System.Collections;
using UnityEngine;

namespace _Game.Scripts.UI
{
    public class SpritePopup : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _moveHeight = 0.5f;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private float _size = 1f;

        private Coroutine _coroutine;
        private Vector3 _initialPosition;

        private void Awake()
        {
            _initialPosition = transform.localPosition;
            _spriteRenderer.enabled = false;
            transform.localScale = Vector3.one * _size;
        }

        public void Show(Sprite sprite)
        {
            if (sprite == null)
                return;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ShowRoutine(sprite));
        }

        private IEnumerator ShowRoutine(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
            _spriteRenderer.enabled = true;

            transform.localPosition = _initialPosition;
            transform.localScale = Vector3.one * _size;
            SetAlpha(1f);

            Vector3 targetPosition =
                _initialPosition + Vector3.up * _moveHeight;

            float elapsed = 0f;

            while (elapsed < _duration)
            {
                elapsed += Time.deltaTime;

                float progress = Mathf.Clamp01(
                    elapsed / _duration
                );

                transform.localPosition = Vector3.Lerp(
                    _initialPosition,
                    targetPosition,
                    progress
                );

                SetAlpha(1f - progress);

                yield return null;
            }

            transform.localPosition = _initialPosition;
            _spriteRenderer.enabled = false;

            _coroutine = null;
        }

        private void SetAlpha(float alpha)
        {
            Color color = _spriteRenderer.color;
            color.a = alpha;
            _spriteRenderer.color = color;
        }
    }
}
