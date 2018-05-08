﻿using System;
using EcsRx.Entities;
using EcsRx.Groups;
using EcsRx.Groups.Observable;
using EcsRx.Systems;
using EcsRx.Unity.Examples.CustomGameObjectHandling.Components;
using UniRx;
using UnityEngine;

namespace EcsRx.Unity.Examples.CustomGameObjectHandling.Systems
{
    public class PlayerControlSystem : IReactToGroupSystem
    {
        public readonly float MovementSpeed = 2.0f;

        public IGroup TargetGroup => new GroupBuilder()
            .WithComponent<CustomViewComponent>()
            .WithComponent<PlayerControlledComponent>()
            .Build();

        public IObservable<IObservableGroup> ReactToGroup(IObservableGroup group)
        { return Observable.EveryUpdate().Select(x => group); }

        public void Execute(IEntity entity)
        {
            var strafeMovement = 0f;
            var forardMovement = 0f;

            if (Input.GetKey(KeyCode.A)) { strafeMovement = -1.0f; }
            if (Input.GetKey(KeyCode.D)) { strafeMovement = 1.0f; }
            if (Input.GetKey(KeyCode.W)) { forardMovement = 1.0f; }
            if (Input.GetKey(KeyCode.S)) { forardMovement = -1.0f; }

            var viewComponent = entity.GetComponent<CustomViewComponent>();
            var transform = viewComponent.CustomView.transform;
            var newPosition = transform.position;

            newPosition.x += strafeMovement * MovementSpeed * Time.deltaTime;
            newPosition.z += forardMovement * MovementSpeed * Time.deltaTime;

            transform.position = newPosition;
        }
    }
}