using Exiled;
using Exiled.API.Interfaces;
using Exiled.API.Enums;
using Exiled.API.Features;
using UnityEngine;
using MEC;
using ServerEvent = Exiled.Events.Handlers.Server;

namespace PowOut
{
    public class PowOut : Plugin<Config>
    {
        public static PowOut Singleton = new PowOut();

        private PowOutHandler powOutHandler;

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
            powOutHandler = new PowOutHandler();
            ServerEvent.RoundStarted += powOutHandler.OnRoundStart;

            Log.Info($"Config entry 'PowOutCassie' success, fetched: {Config.PowOutCassie}");
            Log.Info($"Config entry 'PowOutMsg' success, fetched: {Config.PowOutMsg}");
            Log.Info($"Config entry 'PowOutMinRng' success, fetched: {Config.PowOutMinRng}");
            Log.Info($"Config entry 'PowOutMaxRng' success, fetched: {Config.PowOutMaxRng}");
            Log.Info($"Config entry 'PowOutFreqMinRng' success, fetched: {Config.PowOutFreqMinRng}");
            Log.Info($"Config entry 'PowOutFreqMaxRng' success, fetched: {Config.PowOutFreqMaxRng}");

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
            ServerEvent.RoundStarted += powOutHandler.OnRoundStart;

        }

        /// <summary>
        /// Unregisters the plugin events.
        /// </summary>
        private void UnregisterEvents()
        {
            ServerEvent.RoundStarted -= powOutHandler.OnRoundStart;
            Singleton = null;
            powOutHandler = null;
            /*Exiled.Events.Handlers.Server.WaitingForPlayers -= serverHandler.OnWaitingForPlayers;
            Exiled.Events.Handlers.Server.EndingRound -= serverHandler.OnEndingRound;

            Exiled.Events.Handlers.Player.Destroying -= playerHandler.OnDestroying;
            Exiled.Events.Handlers.Player.Dying -= playerHandler.OnDying;
            Exiled.Events.Handlers.Player.Died -= playerHandler.OnDied;
            Exiled.Events.Handlers.Player.ChangingRole -= playerHandler.OnChangingRole;
            Exiled.Events.Handlers.Player.ChangingItem -= playerHandler.OnChangingItem;
            Exiled.Events.Handlers.Player.Verified -= playerHandler.OnVerified;
            Exiled.Events.Handlers.Player.FailingEscapePocketDimension -= playerHandler.OnFailingEscapePocketDimension;
            Exiled.Events.Handlers.Player.EscapingPocketDimension -= playerHandler.OnEscapingPocketDimension;
            Exiled.Events.Handlers.Player.UnlockingGenerator -= playerHandler.OnUnlockingGenerator;
            Exiled.Events.Handlers.Player.PreAuthenticating -= playerHandler.OnPreAuthenticating;

            Exiled.Events.Handlers.Warhead.Stopping -= warheadHandler.OnStopping;
            Exiled.Events.Handlers.Warhead.Starting -= warheadHandler.OnStarting;

            Exiled.Events.Handlers.Scp106.Teleporting -= playerHandler.OnTeleporting;
            Exiled.Events.Handlers.Scp106.Containing -= playerHandler.OnContaining;
            Exiled.Events.Handlers.Scp106.CreatingPortal -= playerHandler.OnCreatingPortal;

            Exiled.Events.Handlers.Scp914.Activating -= playerHandler.OnActivating;
            Exiled.Events.Handlers.Scp914.ChangingKnobSetting -= playerHandler.OnChangingKnobSetting;

            Exiled.Events.Handlers.Map.ExplodingGrenade -= mapHandler.OnExplodingGrenade;
            Exiled.Events.Handlers.Map.GeneratorActivated -= mapHandler.OnGeneratorActivated;

            Exiled.Events.Handlers.Item.ChangingDurability -= itemHandler.OnChangingDurability;
            Exiled.Events.Handlers.Item.ChangingAttachments -= itemHandler.OnChangingAttachments;

            Exiled.Events.Handlers.Scp914.UpgradingItems -= scp914Handler.OnUpgradingItems;

            Exiled.Events.Handlers.Scp096.AddingTarget -= scp096Handler.OnAddingTarget;

            serverHandler = null;
            playerHandler = null;
            warheadHandler = null;
            mapHandler = null;
            itemHandler = null;
            scp914Handler = null;
            scp096Handler = null;*/
        }
    }
}
