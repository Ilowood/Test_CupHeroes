using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public static class Profiles
    {
        private static float CalculateDecelerationRate(float speed, float decelerationTime)
            => speed / decelerationTime;
        private static float CalculateBrakingDistance(float speed, float decelerationRate)
            => (speed * speed) / (2f * decelerationRate);
        private static float CalculateAccelerationDistance(float startSpeed, float targetSpeed, float accelerationRate)
            => Mathf.Max(0f, (targetSpeed * targetSpeed - startSpeed * startSpeed) / (2f * accelerationRate));
        private static float CalculatePeakSpeed(float startSpeed, float distance, float accelerationRate, float decelerationRate)
            => Mathf.Sqrt((startSpeed * startSpeed * decelerationRate + 2f * distance * accelerationRate * decelerationRate) / (accelerationRate + decelerationRate));

        public static UniTask Accelerate(MovementSystem system, Vector3 target, float maxSpeed, float accelerationTime)
        {
            var direction = Mathf.Sign(target.x - system.Transform.position.x);
            var accelerationRate = maxSpeed / accelerationTime;

            return system.RunPhase(target, maxSpeed, accelerationRate, direction);
        }

        public static UniTask MoveStraight(MovementSystem system, Vector3 target, float speed)
        {
            var direction = Mathf.Sign(target.x - system.Transform.position.x);
            return system.RunPhase(target, speed, 0f, direction);
        }

        public static async UniTask Decelerate(MovementSystem system, Vector3 target, float decelerationTime)
        {
            var direction = Mathf.Sign(target.x - system.Transform.position.x);
            var distance = Vector3.Distance(system.Transform.position, target);

            var currentSpeed = Mathf.Max(system.CurrentSpeed, 0f);
            var decelerationRate = CalculateDecelerationRate(currentSpeed, decelerationTime);
            var brakingDistance = CalculateBrakingDistance(currentSpeed, decelerationRate);

            if (distance > brakingDistance + 1e-3f)
            {
                var cruiseDistance = distance - brakingDistance;
                var brakingStart = system.Transform.position + new Vector3(direction * cruiseDistance, 0f, 0f);

                await system.RunPhase(brakingStart, currentSpeed, 0f, direction);
            }

            await system.RunPhase(target, 0f, decelerationRate, direction);
        }

        public static async UniTask Trapezoid(MovementSystem system, Vector3 target, float maxSpeed, float accelerationTime, float decelerationTime)
        {
            var direction = Mathf.Sign(target.x - system.Transform.position.x);
            var distance = Vector3.Distance(system.Transform.position, target);

            var accelerationRate = maxSpeed / accelerationTime;
            var decelerationRate = maxSpeed / decelerationTime;

            var accelerationDistance = CalculateAccelerationDistance(system.CurrentSpeed, maxSpeed, accelerationRate);
            var decelerationDistance = CalculateBrakingDistance(maxSpeed, decelerationRate);
            var cruiseDistance = distance - (accelerationDistance + decelerationDistance);

            if (cruiseDistance < 0f)
            {
                await Triangle(system, target, accelerationRate, decelerationRate, direction);
                return;
            }

            var accelerationEnd = system.Transform.position + new Vector3(direction * accelerationDistance, 0f, 0f);
            var cruiseEnd = accelerationEnd + new Vector3(direction * cruiseDistance, 0f, 0f);

            await system.RunPhase(accelerationEnd, maxSpeed, accelerationRate, direction);
            await system.RunPhase(cruiseEnd, maxSpeed, 0f, direction);
            await system.RunPhase(target, 0f, decelerationRate, direction);
        }

        public static async UniTask Triangle(MovementSystem system, Vector3 target, float accelerationRate, float decelerationRate, float direction)
        {
            var distance = Vector3.Distance(system.Transform.position, target);
            var peakSpeed = CalculatePeakSpeed(system.CurrentSpeed, distance, accelerationRate, decelerationRate);
            var accelerationDistance = CalculateAccelerationDistance(system.CurrentSpeed, peakSpeed, accelerationRate);

            var accelerationEnd = system.Transform.position + new Vector3(direction * accelerationDistance, 0f, 0f);

            await system.RunPhase(accelerationEnd, peakSpeed, accelerationRate, direction);
            await system.RunPhase(target, 0f, decelerationRate, direction);
        }
    }
}
