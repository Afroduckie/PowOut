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
        }
    }
}
