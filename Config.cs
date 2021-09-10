// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace PowOut
{
    using Exiled.API.Interfaces;
    using Exiled.API.Enums;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Interactables.Interobjects.DoorUtils;

    /// <inheritdoc cref="IConfig"/>
    public class Config : IConfig
    {
        /// <inheritdoc/>
        [Description("Is 'PowOut' plugin enabled?")]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Boolean config to enable/disable CASSIE broadcasts at PowOut start
        /// </summary>
        [Description("CASSIE plays warning on PowOut events? [Default: True]")]
        public bool PowOutCassie { get; set; } = true;

        /// <summary>
        /// Gets String to pass to CASSIE for playing over loudspeakers
        /// </summary>
        [Description("CASSIE voice line to play when a blackout occurs.")]
        public string PowOutMsg { get; private set; } = "Error .g1 .g4 .g3 facility power .g6";

        /// <summary>
        /// Boolean to enable/disable Blackouts (if plugin enabled either Blackouts or Brownouts must be enabled)
        /// </summary>
        [Description("Should Blackout events occur? [Default: True] NOTE: If both blackouts AND brownouts disabled, defaults to Blackout events.")]
        public bool PowOutDoBlackouts { get; set; } = true;

        /// <summary>
        /// Boolean config to enable/disable CASSIE broadcasts at PowOut start
        /// </summary>
        [Description("Should Brownout events occur? Picks one at random if both enabled. [Default: True]")]
        public bool PowOutDoBrownouts { get; set; } = true;

        /// <summary>
        /// Int ratio of blackouts to brownouts, in percent change
        /// </summary>
        [Description("Percent chance for a brownout instead of a blackout. [Default: 40, Max 100]")]
        public int PowOutBrownoutRatio { get; private set; } = 40;

        /// <summary>
        /// Int minimum duration of PowOut event
        /// </summary>
        [Description("Minimum PowOut duration in seconds. [Default: 150]")]
        public int PowOutMinRng { get; private set; } = 150;

        /// <summary>
        /// Int maximum duration of PowOut event
        /// </summary>
        [Description("Max PowOut duration in seconds. [Default: 180]")]
        public int PowOutMaxRng { get; private set; } = 180;

        /// <summary>
        /// Int minimum duration of PowOut event
        /// </summary>
        [Description("Min cooldown of PowOut events during match, in seconds. [Default: 360]")]
        public int PowOutFreqMinRng { get; private set; } = 360;

        /// <summary>
        /// Int maximum duration of PowOut event
        /// </summary>
        [Description("Max cooldown of PowOut events during match, in seconds. [Default: 600]")]
        public int PowOutFreqMaxRng { get; private set; } = 600;

        /// <summary>
        /// Boolean config to enable/disable CASSIE broadcasts at PowOut start
        /// </summary>
        [Description("Should a power outage open doors? [Default: True]")]
        public bool PowOutDoorControl { get; set; } = true;

        /// <summary>
        /// List of doors to be excluded from PowOut's door control
        /// </summary>
        [Description("Doors to EXCLUDE from the plugin's door controls. Use either Exiled Discord (#resources) or the in-game Remote Admin (Door Management) to find room names.")]
        public List<string> PowOutDoorBlacklist { get; private set; } = new List<string>() { "CHECKPOINT_EZ_HCZ", "CHECKPOINT_LCZ_A", "CHECKPOINT_LCZ_B", "ESCAPE_PRIMARY", "ESCAPE_SECONDARY", "GATE_A", "GATE_B", "SURFACE_NUKE" }; 

        /// <summary>
        /// Boolean config to enable/disable UNSTUCK routine
        /// </summary>
        [Description("Should the sever cause power outages to free stuck SCP players? [Default: True]")]
        public bool PowOutUnstuck { get; set; } = true;

        /// <summary>
        /// Gets String to pass to CASSIE for playing over loudspeakers
        /// </summary>
        [Description("Message to broadcast to SCP player being let out. Leave blank if no broadcast is desired.")]
        public string PowOutUnstuckMsg{ get; private set; } = "SCP-079 has let you out.";

        /// <summary>
        /// Int ratio of blackouts to brownouts, in percent change
        /// </summary>
        [Description("Time SCP player will see the unstuck message. [Default: 8]")]
        public int PowOutUnstuckMsgTime { get; private set; } = 8;
    }
}
