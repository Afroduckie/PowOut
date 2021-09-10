using System;
using System.Collections.Generic;
using System.Linq;
using MEC;
using Exiled.API.Features;
using Exiled.API.Enums;
using Exiled.Events.EventArgs;
using Random = UnityEngine.Random;
using Interactables.Interobjects.DoorUtils;
using UnityEngine;

/*
    UnstuckHandler
            Runs detection for stuck SCP players and performs one of a few actions to get them unstuck.
            Configurable options include- full blackout to unstuck, open only that door, stuck timer
 */
namespace PowOut
{
    public class UnstuckHandler
    {
        private CoroutineHandle timer = new CoroutineHandle();
        private static readonly Config Config = PowOut.Singleton.Config;
        private Player pl;
        private RoleType role;

        private List<RoomType> closets = new List<RoomType>()
        {
            RoomType.Lcz012,
            RoomType.Hcz079,
            RoomType.Hcz106,
            RoomType.Lcz914
        };
        private List<RoomType> exclusion = new List<RoomType>()
        {
            RoomType.EzGateA,
            RoomType.EzGateB
        };

        public void OnRoundStart()
        {
            Timing.KillCoroutines(timer);
        }

        public void OnRoundRestart()
        {
            Timing.KillCoroutines(timer);
        }

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            Timing.KillCoroutines(timer);
        }

        public void InteractingDoor(InteractingDoorEventArgs ev)
        {
            pl = ev.Player;
            role = pl.Role;
            if(ev.Player.IsScp)
            {
                timer = Timing.RunCoroutine(StuckTimer());
            }
                
        }

        private IEnumerator<float> StuckTimer()
        {
            if (!timer.IsRunning)
            {
                // Check every 25 seconds to see if SCP player is in the same room they were earlier. If so, check to see if they're stuck and let them out if so.
                Log.Debug("{Unstucker} Starting the Unstuck watcher thread for SCP role " + role);
                Room start = pl.CurrentRoom;
                yield return Timing.WaitForSeconds(6f);
                bool release = false;
                if (pl.CurrentRoom == start)
                {
                    // Check if they're stuck with a simple detection first
                    int doorcnt = pl.CurrentRoom.Doors.Count();
                    int lockcnt = 0;
                    foreach (Door door in pl.CurrentRoom.Doors)
                    {
                        if (!door.IsOpen && (door.IsLocked || door.RequiredPermissions.RequiredPermissions != Interactables.Interobjects.DoorUtils.KeycardPermissions.None))
                            lockcnt++;
                    }
                    if (doorcnt == lockcnt && !exclusion.Contains(pl.CurrentRoom.Type))
                        release = true;
                    else if (closets.Contains(pl.CurrentRoom.Type) && !exclusion.Contains(pl.CurrentRoom.Type))
                    {
                        release = true;
                    }
                }

                if (release)
                {
                    Log.Debug("{Unstucker} Possibly trapped " + role.ToString() + " player in " + start.Name + ", starting stuck timer...");
                    yield return Timing.WaitForSeconds(15f);
                    foreach (Door door in pl.CurrentRoom.Doors)
                    {
                        door.Base.NetworkTargetState = true;
                    }

                    Log.Debug("{Unstucker} Released stuck SCP player in " + start.Name);
                    pl.ShowHint(Config.PowOutUnstuckMsg, Config.PowOutUnstuckMsgTime + 5f);
                    yield return Timing.WaitForSeconds(10f);

                    foreach (Door door in start.Doors)
                    {
                        door.Base.NetworkTargetState = false;
                    }
                }
                Timing.KillCoroutines(timer);
            }
            else
            {
                Timing.KillCoroutines(timer);
            }
        }

    }
}
