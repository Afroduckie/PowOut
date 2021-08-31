using System;
using System.Collections.Generic;
using System.Linq;
using MEC;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using UnityEngine;
using Random = UnityEngine.Random;
using Interactables.Interobjects.DoorUtils;

namespace PowOut
{
    public class PowOutHandler
    {
        public CoroutineHandle powerInterval = new CoroutineHandle();
        private static readonly Config Config = PowOut.Singleton.Config;

        public void OnRoundStart()
        {
            if (powerInterval.IsRunning)
            {
                Timing.KillCoroutines(powerInterval);
            }

            Timing.RunCoroutine(Blackout(), "powerInterval");
            Log.Debug($"PowOut successfully booted up, starting Blackout countdown---");
        }

        public void OnRoundRestart()
        {
            Timing.KillCoroutines("powerInterval");
        }

        public void OnRoundEnd()
        {
            Timing.KillCoroutines("powerInterval");
        }

        private IEnumerator<float> Blackout()
        {
            yield return Timing.WaitForSeconds(5f);
            float duration = Random.Range(Config.PowOutMinRng, Config.PowOutMinRng);
            float runtime = Random.Range(Config.PowOutFreqMinRng, Config.PowOutFreqMaxRng);
            yield return Timing.WaitForSeconds(runtime);

            Log.Debug("Success in creating power outage event.");

            List<Room> rooms = Map.Rooms.ToList();
            foreach (Room r in rooms)
            {
                r.TurnOffLights(duration);               
            }

            if (Config.PowOutUnlockAllDoors)
            {
                List<DoorVariant> doors = Map.Doors.ToList();
                foreach (DoorVariant d in doors)
                {
                    d.ServerChangeLock(DoorLockReason.AdminCommand, true);
                }
            }

            if (Config.PowOutCassie == true)//Config.PowOutCassie)
            {
                string cast = Config.PowOutMsg;
                Cassie.Message(cast);
            }
            yield return Timing.WaitForSeconds(duration);

            if (Config.PowOutUnlockAllDoors)
            {
                List<DoorVariant> doors = Map.Doors.ToList();
                foreach (DoorVariant d in doors)
                {
                    d.ServerChangeLock(DoorLockReason.AdminCommand, true);
                }
            }

            Timing.RunCoroutine(Blackout(), "powerInterval");
        }

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            Timing.KillCoroutines(powerInterval);
        }
    }
}