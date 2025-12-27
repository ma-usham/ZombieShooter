using VContainer;
using VContainer.Unity;
using UnityEngine;
using Darkmatter.Core;
using Darkmatter.Presentation;
using Darkmatter.Domain;

namespace Darkmatter.App
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private InputReaderSO inputReader;
        [SerializeField] private PlayerMotor playerMotor;
        [SerializeField] private PlayerAnimController playerAnim;
        [SerializeField] private PlayerConfigSO playerConfig;
        [SerializeField] private CameraConfigSO cameraConfig;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<PlayerController>(Lifetime.Scoped);


            builder.RegisterComponent<IPlayerAnim>(playerAnim);
            builder.RegisterComponent<IInputReader>(inputReader);
            builder.RegisterComponent<IPlayerPawn>(playerMotor);

            builder.RegisterComponent(playerConfig);
            builder.RegisterComponent(cameraConfig);
            builder.Register<PlayerStateMachine>(Lifetime.Scoped);
        }
    }
}
