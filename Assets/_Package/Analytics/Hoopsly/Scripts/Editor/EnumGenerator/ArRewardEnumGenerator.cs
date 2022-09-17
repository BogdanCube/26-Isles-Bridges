using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Hoopsly.Editor
{
    public class ArRewardEnumGenerator
    {
        private const string m_enumsProjectPath = "Assets/Hoopsly/Scripts/Runtime/Enums/";
        private const string m_adRewardTypeEnumFilename = "AdRewardType.cs";
        private const string m_enumBody = "public enum AdRewardType {";

        public static void BuildEnum(AdRewardTypeEditrorCell[] newEnumValues)
        {
            string newEnumLine = m_enumBody;
            for (int i = 0; i < newEnumValues.Length; i++)
            {
                newEnumLine += newEnumValues[i].m_value;
                if(i<newEnumValues.Length-1)
                    newEnumLine += ",";
            }
            newEnumLine += "};";
            File.WriteAllText(m_enumsProjectPath + m_adRewardTypeEnumFilename, newEnumLine);
        }

        public static string[] GetCurrentAdRewardTypesFromEnum()
        {
            return ParseEnum(File.ReadAllText(m_enumsProjectPath + m_adRewardTypeEnumFilename));
        }

        private static string[] ParseEnum(string enumText)
        {
            string[] firstSplit = enumText.Split('{');
            string[] secondSplit = firstSplit[1].Split('}');
            string[] thirdSplit = secondSplit[0].Split(',');
            for (int i = 0; i < thirdSplit.Length; i++)
            {
                thirdSplit[i] = thirdSplit[i].Replace(" ", "");
            }
            return thirdSplit;
        }
    }
}