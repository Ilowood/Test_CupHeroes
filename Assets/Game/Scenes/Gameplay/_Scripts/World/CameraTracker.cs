using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Game 
{
    public class CameraTracker : MonoBehaviour
    {
        private Vector3 _lastCameraPosition;

        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineCamera _сinemachineCamera;
        [SerializeField] private CinemachinePositionComposer _cinemachinePositionComposer;

        public event Action<Vector3> OnCameraOffsetChanged;

        public void FollowTarget(Transform tracking)
        {
            var currentOffset = _cinemachinePositionComposer.TargetOffset;
            var newCameraPos = tracking.position - currentOffset;

            _сinemachineCamera.PreviousStateIsValid = false;
            _сinemachineCamera.ForceCameraPosition(newCameraPos, _сinemachineCamera.transform.rotation);
            _сinemachineCamera.Follow = tracking;

            _lastCameraPosition = _camera.transform.position;

            CinemachineCore.CameraUpdatedEvent.AddListener(OnCameraUpdated);
        }

        public void StopFollowing()
        {
            _сinemachineCamera.Follow = null;
            DisableTracking();
        }

        private void OnCameraUpdated(CinemachineBrain brain)
        {
            var currentPosition = _camera.transform.position;
            var cameraOffset = currentPosition - _lastCameraPosition;

            OnCameraOffsetChanged?.Invoke(cameraOffset);

            _lastCameraPosition = currentPosition;
        }
        
        private void DisableTracking()
        {
            CinemachineCore.CameraUpdatedEvent.RemoveListener(OnCameraUpdated);
        }

        private void OnDestroy()
        {
            DisableTracking();
        }
    }
}
