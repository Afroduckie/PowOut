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
        [Description("Is 'LowPower' plugin enabled?")]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Boolean config to enable/disable CASSIE broadcasts at PowOut start
        /// </summary>
        [Description("CASSIE plays warning on PowOut events? [Default: True]")]
        public bool PowOutCassie { get; set; } = true;

        /// <summary>
        /// Gets String to pass to CASSIE for playing over loudspeakers
        /// </summary>
        [Description("CASSIE voice line to play at PowOut.")]
        public string PowOutMsg { get; private set; } = "Error .g1 .g2 .g4 malfunction .g7 .g4 .g3 facility power grid .g6";

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
        [Description("Should a power outage open all doors? [Default: True]")]
        public bool PowOutUnlockAllDoors { get; set; } = true;
    }
}
