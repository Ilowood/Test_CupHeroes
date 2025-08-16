using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class Battle
    {
        // private readonly EntityController _playerController;
        // private readonly List<EntityController> _enemieControllers;

        // private bool IsConditionBattle => IsLiveEnemies() && _playerController != null && _playerController.IsLive;

        // public bool IsUserWin => _playerController != null;

        // public Battle(EntityController playerController, List<EntityController> enemieControllers)
        // {
        //     _playerController = playerController;
        //     _enemieControllers = enemieControllers;
        // }

        // private bool IsLiveEnemies()
        // {
        //     for (var i = 0; i < _enemieControllers.Count; i++)
        //     {
        //         if (_enemieControllers[i] != null && _enemieControllers[i].IsLive)
        //             return true;
        //     }
        //     return false;
        // }

        // public async UniTask Start()
        // {
        //     var isUserStep = true;

        //     while (IsConditionBattle)
        //     {
        //         if (isUserStep)
        //         {
        //             await Round(new() { _playerController }, _enemieControllers);
        //             isUserStep = false;
        //         }
        //         else
        //         {
        //             await Round(_enemieControllers, new() { _playerController });
        //             isUserStep = true;
        //         }
        //     }
        // }

        // private async UniTask Round(List<EntityController> attackingDeck, List<EntityController> defendingDeck)
        // {
        //     for (var i = 0; i < attackingDeck.Count; i++)
        //     {
        //         await attackingDeck[i].Attack(FindEnemy(defendingDeck).transform);

        //         if (!IsConditionBattle)
        //         {
        //             break;
        //         }
        //     }
        // }

        // private EntityController FindEnemy(List<EntityController> enemy)
        // {
        //     for (var i = 0; i < enemy.Count; i++)
        //     {
        //         if (enemy[i] != null && enemy[i].IsLive)
        //         {
        //             return enemy[i];
        //         }
        //     }

        //     throw new System.Exception();
        // }
    }
}
