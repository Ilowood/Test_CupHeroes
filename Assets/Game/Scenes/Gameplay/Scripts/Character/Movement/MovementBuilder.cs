using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class MovementBuilder
    {
        private readonly List<Func<UniTask>> _steps = new();
        private readonly MovementSystem _system;

        public MovementBuilder(MovementSystem system)
        {
            _system = system;
        }

        public MovementBuilder AccelerateTo(Vector3 targetPoint, Func<MovementSystem, MoveProfile> profileSelector)
        {
            var p = profileSelector(_system);
            _steps.Add(() => Profiles.Accelerate(_system, targetPoint, p.Speed, p.AccelerationTime));
            return this;
        }

        public MovementBuilder MoveStraightTo(Vector3 targetPoint)
        {
            _steps.Add(() => Profiles.MoveStraight(_system, targetPoint, _system.CurrentSpeed));
            return this;
        }

        public MovementBuilder DecelerateToStop(Vector3 targetPoint, Func<MovementSystem, MoveProfile> profileSelector)
        {
            var profile = profileSelector(_system);
            _steps.Add(() => Profiles.Decelerate(_system, targetPoint, profile.DecelerationTime));
            return this;
        }

        public MovementBuilder Do(Action action)
        {
            _steps.Add(() =>
            {
                action?.Invoke();
                return UniTask.CompletedTask;
            });

            return this;
        }

        public async UniTask RunAsync(CancellationToken token = default)
        {
            foreach (var step in _steps)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }

                await step.Invoke();
            }
        }
    }
}
