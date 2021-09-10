using System;
using System.Collections.Generic;
using System.Linq;
using MEC;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Random = UnityEngine.Random;
using Interactables.Interobjects.DoorUtils;
using UnityEngine;

namespace PowOut
{
    public class PowOutHandler
    {
        public CoroutineHandle powerInterval = new CoroutineHandle();       // Blackout event timer, includes DoorControl
        private static readonly Config Config = PowOut.Singleton.Config;    // For enabling use of Config variables
        private float EventInterval;                                        // Variable for referencing the randoms
        private float EventDuration;                                        //      externally for multiple coroutines
        
        public void OnRoundStart()
        {
            // Kill any errant still-running coroutines
            if (powerInterval.IsRunning)
            {
                Timing.KillCoroutines("powerInterval");
            }

            SetRandoms();

            // Check config for door functionality
            if (Config.PowOutDoBlackouts)
            {
                try
                {
                    powerInterval = Timing.RunCoroutine(Blackout());
                }
                catch (Exception e)
                {
                    Log.Error($"{e}");
                }
            }            
        }

        public void SetRandoms()
        {
            EventInterval = Random.Range(Config.PowOutFreqMinRng, Config.PowOutFreqMaxRng);
            EventDuration = Random.Range(Config.PowOutMinRng, Config.PowOutMaxRng);
        }

        public void OnRoundRestart()
        {
            Timing.KillCoroutines("powerInterval");
            Timing.KillCoroutines("doorControl");
        }

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            Timing.KillCoroutines("powerInterval");
            Timing.KillCoroutines("doorControl");
        }

        private IEnumerator<float> Blackout()
        {
            Log.Debug("Blackout coroutine initialized.");
            yield return Timing.WaitForSeconds(EventInterval);

            Log.Debug("Success in creating power outage event.");

            foreach (Room r in Map.Rooms)
            {
                r.TurnOffLights(EventDuration);               
            }

            if (Config.PowOutDoorControl)
            {
                // Iterate through all doors to unlock and open them. Currently uses Warhead routine as template.
                foreach (Door door in Exiled.API.Features.Map.Doors)
                {
                    if (!Config.PowOutDoorBlacklist.Contains(door.Nametag) && door.Base.RequiredPermissions.RequiredPermissions != KeycardPermissions.Checkpoints)
                    {
                        door.Base.NetworkTargetState = true;
                        door.Base.ServerChangeLock(DoorLockReason.AdminCommand, true);
                    }
                }

                Log.Debug("{DoorControl} Opening Whitelisted Doors...");              
            }

            if (Config.PowOutCassie)
            {
                string bcast = Config.PowOutMsg;
                Cassie.Message(bcast);
            }
            yield return Timing.WaitForSeconds(EventDuration);

            if (Config.PowOutDoorControl)
            {
                Log.Debug("{DoorControl} Closing Affected Doors...");
                foreach (Door door in Map.Doors)
                {
                    if (!Config.PowOutDoorBlacklist.Contains(door.Nametag))
                    {
                        door.Base.ServerChangeLock(DoorLockReason.AdminCommand, false);
                        door.Base.NetworkTargetState = false;
                    }
                }
            }

            if (powerInterval.IsRunning)
            {
                Timing.KillCoroutines("powerInterval");
            }
            SetRandoms();
            Timing.RunCoroutine(Blackout(), "powerInterval");
        }
    }
}