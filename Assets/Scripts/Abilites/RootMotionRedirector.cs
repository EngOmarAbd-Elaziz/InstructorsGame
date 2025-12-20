using UnityEngine;
namespace VirtualLabs.Utility
{
    [RequireComponent(typeof(Animator))]
    public class RootMotionRedirector : MonoBehaviour
    {
        [Tooltip("If true, applies the Animator's delta position to the parent.")]
        [SerializeField] private bool _applyPosition = true;
        [Tooltip("If true, applies the Animator's delta rotation to the parent.")]
        [SerializeField] private bool _applyRotation = true;
        private Animator _animator;
        private Transform _parentTransform;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _parentTransform = transform.parent;
            if (_parentTransform == null)
            {
                Debug.LogWarning($"[RootMotionRedirector] No parent found for {name}. Root motion will not be applied.", this);
            }
        }
        private void OnAnimatorMove()
        {
            if (_parentTransform == null) return;
            if (_applyPosition)
            {
                _parentTransform.position += _animator.deltaPosition;
            }
            if (_applyRotation)
            {
                _parentTransform.rotation *= _animator.deltaRotation;
            }
        }
    }
}
