using Unity.Cinemachine;
using UnityEngine;

namespace Game 
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _сinemachineCamera;
        [SerializeField] private CinemachinePositionComposer _cinemachinePositionComposer;
        
        public void SetTrackingTarget(Transform tracking)
        {
            var currentOffset = _cinemachinePositionComposer.TargetOffset;
            var newCameraPos = tracking.position - currentOffset;

            _сinemachineCamera.PreviousStateIsValid = false;
            _сinemachineCamera.ForceCameraPosition(newCameraPos, _сinemachineCamera.transform.rotation);
            _сinemachineCamera.Follow = tracking;
        }
    }
}
