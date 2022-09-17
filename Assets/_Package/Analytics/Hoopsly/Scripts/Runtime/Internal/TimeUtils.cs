using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Hoopsly.Internal.Time
{
    public static class TimeUtils
    {
        public static int GetUnixTime()
        {
            return (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public static int GetUnixTimeInSeconds()
        {
            return (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}
