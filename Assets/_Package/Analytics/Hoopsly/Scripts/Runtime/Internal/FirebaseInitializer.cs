using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Analytics;
using Firebase.Crashlytics;
using System.Threading.Tasks;

namespace Hoopsly.Internal.FirebaseSdk
{
    public class FirebaseInitializer : IInitilizable
    {
        public async Task Initilize(string uuid)
        {
            try
            {
                await FirebaseInit(uuid);
            }
            catch (System.Exception ex)
            {
                HoopslyLogger.LogMessage(ex.StackTrace, Settings.HoopslyLogLevel.Suppress, H_LogType.Error);
            }
        }

        private async Task FirebaseInit(string uuid)
        {
            HoopslyLogger.LogMessage("==========[FIREBASE_INIT]==========", Settings.HoopslyLogLevel.Suppress);
            await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    FirebaseAnalytics.SetUserId(uuid);
                    Crashlytics.SetUserId(uuid);
                    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                    HoopslyLogger.LogMessage("==========[FIREBASE_DEPENDENCIES_CHECK_COMPLETE]==========", Settings.HoopslyLogLevel.Debug);
                    FirebaseApp.LogLevel = LogLevel.Assert;
                }
                else
                {
                    HoopslyLogger.LogMessage(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus), Settings.HoopslyLogLevel.Suppress, H_LogType.Error);
                }
            });
            HoopslyLogger.LogMessage("==========[FIREBASE_INIT_COMPLETE]==========", Settings.HoopslyLogLevel.Suppress);
        }
    }
}
