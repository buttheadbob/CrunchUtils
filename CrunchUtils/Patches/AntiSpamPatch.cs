using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Sandbox.Engine.Multiplayer;
using SpaceEngineers.Game.Entities.Blocks;
using Torch.Managers.PatchManager;

namespace CrunchUtilities.Patches
{
    [PatchShim]
    public static class AntiSpamPatch
    {
        internal static readonly MethodInfo flee =
            typeof(MyMultiplayerBase).GetMethod("IsMessageSpam", BindingFlags.Static | BindingFlags.NonPublic) ??
            throw new Exception("Failed to find patch method IsMessageSpam");

        internal static readonly MethodInfo patchFlee =
            typeof(AntiSpamPatch).GetMethod(nameof(Patched), BindingFlags.Static | BindingFlags.Public) ??
            throw new Exception("Failed to find patch method");

        public static void Patch(PatchContext ctx)
        {

            ctx.GetPattern(flee).Prefixes.Add(patchFlee);
        }

        public static bool Patched(long senderIdentity, string messageText)
        {
            if (senderIdentity == 0)
            {
                return false;
            }

            //    Core.Log.Info("enemy not null, allowing");
            return true;
        }
    }
}
