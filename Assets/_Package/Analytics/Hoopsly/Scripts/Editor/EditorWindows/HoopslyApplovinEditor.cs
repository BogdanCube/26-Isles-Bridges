using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Hoopsly.Settings;
using Hoopsly.Editor.Resources;



namespace Hoopsly.Editor
{
    public class HoopslyApplovinEditor
    {
        bool showInter = false;
        bool showReward = false;
        bool showBanner = false;
        bool showMREC = false;

        public void DrawEditor()
        {
            GUILayout.Label("Applovin MAX settings", EditorResources.Styles.TitleLableStyle);
            using (var v = new EditorGUILayout.VerticalScope("box"))
            {

                GUILayout.Space(15);

                HoopslySettings.Instance.ApplovinSettings.MaxSdkKey = EditorGUILayout.TextField("Applovin MAX SDK key", HoopslySettings.Instance.ApplovinSettings.MaxSdkKey);
                GUILayout.Space(5);

                HoopslySettings.Instance.ApplovinSettings.EnableVerboseLogging = EditorGUILayout.ToggleLeft("Enable Verbose Logging", HoopslySettings.Instance.ApplovinSettings.EnableVerboseLogging);
                GUILayout.Space(5);
                HoopslySettings.Instance.ApplovinSettings.ShowMediationDebuggerOnLoad = EditorGUILayout.ToggleLeft(new GUIContent("Show Mediation debugger on app start"), HoopslySettings.Instance.ApplovinSettings.ShowMediationDebuggerOnLoad);

                using (var adSettings = new EditorGUILayout.VerticalScope("box"))
                {
                    DrawInterstitialSettings();

                    DrawRewardedSettings();

                    DrawMRECsettings();

                    DrawBannerSettings();
                }

                GUILayout.Space(15);
            }
        }

        private void DrawInterstitialSettings()
        {
            showInter = EditorGUILayout.BeginFoldoutHeaderGroup(showInter, "Interstitial AD");
            if (showInter)
            {
                using (var inter = new EditorGUILayout.VerticalScope(EditorResources.Styles.BoxStyle))
                {

                    HoopslySettings.Instance.ApplovinSettings.UseInterstitialAd = EditorGUILayout.BeginToggleGroup("Enable interstitial AD", HoopslySettings.Instance.ApplovinSettings.UseInterstitialAd);

                    GUILayout.Space(5);

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Interstitial AD Unit ID ( ANDROID )", GUILayout.Width(200));
                    HoopslySettings.Instance.ApplovinSettings.InterstitialAdUnitID_ANDROID = EditorGUILayout.TextField(HoopslySettings.Instance.ApplovinSettings.InterstitialAdUnitID_ANDROID);
                    EditorGUILayout.EndHorizontal();

                    GUILayout.Space(5);

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Interstitial AD Unit ID ( iOS )", GUILayout.Width(200));
                    HoopslySettings.Instance.ApplovinSettings.InterstitialAdUnitID_IOS = EditorGUILayout.TextField(HoopslySettings.Instance.ApplovinSettings.InterstitialAdUnitID_IOS);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.EndToggleGroup();
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        private void DrawRewardedSettings()
        {
            GUILayout.Space(10);
            showReward = EditorGUILayout.BeginFoldoutHeaderGroup(showReward, "Reward AD");
            if (showReward)
            {
                using (var inter = new EditorGUILayout.VerticalScope(EditorResources.Styles.BoxStyle))
                {

                    HoopslySettings.Instance.ApplovinSettings.UseRewardedAd = EditorGUILayout.BeginToggleGroup("Enable Rewarded AD", HoopslySettings.Instance.ApplovinSettings.UseRewardedAd);

                    GUILayout.Space(5);

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Rewarded AD Unit ID ( ANDROID )", GUILayout.Width(200));
                    HoopslySettings.Instance.ApplovinSettings.RewardedAdUnitID_ANDROID = EditorGUILayout.DelayedTextField(HoopslySettings.Instance.ApplovinSettings.RewardedAdUnitID_ANDROID);
                    EditorGUILayout.EndHorizontal();

                    GUILayout.Space(5);

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Rewarded AD Unit ID ( iOS )", GUILayout.Width(200));
                    HoopslySettings.Instance.ApplovinSettings.RewardedAdUnitID_IOS = EditorGUILayout.DelayedTextField(HoopslySettings.Instance.ApplovinSettings.RewardedAdUnitID_IOS);
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.EndToggleGroup();
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
        private void DrawMRECsettings()
        {
            GUILayout.Space(10);
            showMREC = EditorGUILayout.BeginFoldoutHeaderGroup(showMREC, "MREC AD");
            if (showMREC)
            {
                using (var inter = new EditorGUILayout.VerticalScope(EditorResources.Styles.BoxStyle))
                {

                    HoopslySettings.Instance.ApplovinSettings.UseMRECAd = EditorGUILayout.BeginToggleGroup("Enable MREC AD", HoopslySettings.Instance.ApplovinSettings.UseMRECAd);
                    GUILayout.Space(5);

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("MREC AD Unit ID ( ANDROID )", GUILayout.Width(200));
                    HoopslySettings.Instance.ApplovinSettings.MRECAdUnitID_ANDROID = EditorGUILayout.DelayedTextField(HoopslySettings.Instance.ApplovinSettings.MRECAdUnitID_ANDROID);
                    EditorGUILayout.EndHorizontal();

                    GUILayout.Space(5);

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("MREC AD Unit ID ( iOS )", GUILayout.Width(200));
                    HoopslySettings.Instance.ApplovinSettings.MRECAdUnitID_IOS = EditorGUILayout.DelayedTextField(HoopslySettings.Instance.ApplovinSettings.MRECAdUnitID_IOS);
                    EditorGUILayout.EndHorizontal();

                    GUILayout.Space(10);

                    HoopslySettings.Instance.ApplovinSettings.MrecPosition = (MaxSdkBase.AdViewPosition)EditorGUILayout.EnumPopup("Select MREC position", HoopslySettings.Instance.ApplovinSettings.MrecPosition);

                    EditorGUILayout.EndToggleGroup();
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private void DrawBannerSettings()
        {
            GUILayout.Space(10);
            showBanner = EditorGUILayout.BeginFoldoutHeaderGroup(showBanner, "Banner AD");
            if (showBanner)
            {
                using (var inter = new EditorGUILayout.VerticalScope(EditorResources.Styles.BoxStyle))
                {

                    HoopslySettings.Instance.ApplovinSettings.UseBannerAd = EditorGUILayout.BeginToggleGroup("Enable Banner AD", HoopslySettings.Instance.ApplovinSettings.UseBannerAd);
                    GUILayout.Space(5);

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Banner AD Unit ID ( ANDROID )", GUILayout.Width(200));
                    HoopslySettings.Instance.ApplovinSettings.BannerAdUnitID_ANDROID = EditorGUILayout.DelayedTextField(HoopslySettings.Instance.ApplovinSettings.BannerAdUnitID_ANDROID);
                    EditorGUILayout.EndHorizontal();

                    GUILayout.Space(5);

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Banner AD Unit ID ( iOS )", GUILayout.Width(200));
                    HoopslySettings.Instance.ApplovinSettings.BannerAdUnitID_IOS = EditorGUILayout.DelayedTextField(HoopslySettings.Instance.ApplovinSettings.BannerAdUnitID_IOS);
                    EditorGUILayout.EndHorizontal();
                    GUILayout.Space(10);
                    HoopslySettings.Instance.ApplovinSettings.UseAdaptiveBanner = EditorGUILayout.ToggleLeft("Use adaptive banner", HoopslySettings.Instance.ApplovinSettings.UseAdaptiveBanner);
                    GUILayout.Space(5);
                    HoopslySettings.Instance.ApplovinSettings.BannerPosition = (MaxSdk.BannerPosition)EditorGUILayout.EnumPopup("Select banner position", HoopslySettings.Instance.ApplovinSettings.BannerPosition);
                    GUILayout.Space(5);
                    HoopslySettings.Instance.ApplovinSettings.BannerBackgroundColor = EditorGUILayout.ColorField("Background color", HoopslySettings.Instance.ApplovinSettings.BannerBackgroundColor);

                    EditorGUILayout.EndToggleGroup();
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}
