using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hoopsly.Internal;
using Hoopsly.Settings;
using Firebase;
using Firebase.Analytics;
using Firebase.RemoteConfig;
using System.Threading.Tasks;

namespace Hoopsly.Internal.FirebaseSdk
{
    public class FirebaseRConfigInitializer : IInitilizable<RemoteConfigInitResult>
    {
        private FirebaseRemoteConfig m_remoteConfig;

        public async Task<RemoteConfigInitResult> Initilize(string uuid)
        {
            try
            {
                return await InitRemoteConfig(uuid);
            }
            catch (System.Exception ex)
            {
                HoopslyLogger.LogMessage(ex.StackTrace, HoopslyLogLevel.Suppress, H_LogType.Error);
                return null;
            }
        }

        private async Task<RemoteConfigInitResult> InitRemoteConfig(string uuid)
        {
            HoopslyLogger.LogMessage("==========[FIREBASE_REMOTE_CONFIG_INIT]==========", Settings.HoopslyLogLevel.Suppress);
            Dictionary<string, object> defaults = new Dictionary<string, object>();
            defaults.Add("ab_test", 0);
            defaults.Add("ab_group", "");
            m_remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            await m_remoteConfig.SetDefaultsAsync(defaults);
            await m_remoteConfig.FetchAsync(System.TimeSpan.Zero);
            await m_remoteConfig.ActivateAsync();
            return new RemoteConfigInitResult(m_remoteConfig.GetValue("ab_group").StringValue, (int)m_remoteConfig.GetValue("ab_test").LongValue);

        }

    }

}
