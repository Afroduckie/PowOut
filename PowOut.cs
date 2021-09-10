using Exiled;
using Exiled.API.Interfaces;
using Exiled.API.Enums;
using Exiled.API.Features;
using UnityEngine;
using MEC;
using ServerEvent = Exiled.Events.Handlers.Server;
using Player = Exiled.Events.Handlers.Player;

namespace PowOut
{
    public class PowOut : Plugin<Config>
    {
        public static PowOut Singleton = new PowOut();

        private PowOutHandler powOutHandler;
        private UnstuckHandler unstucker;

        private PowOut()
        {
            // empty as my love life
        }

        /// <summary>
        /// Gets the only existing instance of this plugin.
        /// </summary>
        public static PowOut Instance => Singleton;

        /// <inheritdoc/>
        public override PluginPriority Priority { get; } = PluginPriority.Last;

        /// <inheritdoc/>
        public override void OnEnabled()
        {
            Singleton = this;
            RegisterEvents();

            base.OnEnabled();
        }

        /// <inheritdoc/>
        public override void OnDisabled()
        {
            UnregisterEvents();
            base.OnDisabled();
        }

        /// <summary>
        /// Registers the plugin events.
        /// </summary>
        private void RegisterEvents()
        {
            powOutHandler = new PowOutHandler();
            unstucker = new UnstuckHandler();
            ServerEvent.RoundStarted += powOutHandler.OnRoundStart;
            ServerEvent.RoundStarted += unstucker.OnRoundStart;
            ServerEvent.RoundEnded += powOutHandler.OnRoundEnded;
            ServerEvent.RoundEnded += unstucker.OnRoundEnded;
            ServerEvent.RestartingRound += powOutHandler.OnRoundRestart;
            ServerEvent.RestartingRound += unstucker.OnRoundRestart;
            Player.InteractingDoor += unstucker.InteractingDoor;
        }

        /// <summary>
        /// Unregisters the plugin events.
        /// </summary>
        private void UnregisterEvents()
        {
            powOutHandler = null;
            unstucker = null;
            ServerEvent.RoundStarted -= powOutHandler.OnRoundStart;
            ServerEvent.RoundStarted -= unstucker.OnRoundStart;
            ServerEvent.RoundEnded -= powOutHandler.OnRoundEnded;
            ServerEvent.RoundEnded -= unstucker.OnRoundEnded;
            ServerEvent.RestartingRound -= powOutHandler.OnRoundRestart;
            ServerEvent.RestartingRound -= unstucker.OnRoundRestart;
            Player.InteractingDoor -= unstucker.InteractingDoor;
        }
    }
}
