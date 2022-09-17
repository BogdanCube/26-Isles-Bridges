using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Hoopsly.Editor.Resources
{
    public class EditorResources
    {
        public class Resources
        {
            private static Texture2D m_trashIcon;
            public static Texture2D TrashIcon
            {
                get
                {
                    if (m_trashIcon==null)
                    {
                        m_trashIcon = EditorGUIUtility.Load("Assets/Hoopsly/Resources/Textures/trashIcon.png") as Texture2D;
                    }
                    return m_trashIcon;
                }
            }

            private static Texture2D m_plusIcon;
            public static Texture2D PlusIcon
            {
                get
                {
                    if (m_plusIcon == null)
                    {
                        m_plusIcon = EditorGUIUtility.Load("Assets/Hoopsly/Resources/Textures/plusIcon.png") as Texture2D;
                    }
                    return m_plusIcon;
                }
            }
        }

        public class Styles
        {
            #region gui_styles
            private static GUIStyle titleLabelStyle;
            public static GUIStyle TitleLableStyle
            {
                get
                {
                    if (titleLabelStyle == null)
                    {
                        titleLabelStyle = new GUIStyle(EditorStyles.label)
                        {
                            fontSize = 14,
                            fontStyle = FontStyle.Bold,
                            fixedHeight = 20
                        };
                    }
                    return titleLabelStyle;
                }
            }

            private static GUIStyle headerLabelStyle;
            public static GUIStyle HeaderLableStyle
            {
                get
                {
                    if (headerLabelStyle == null)
                    {
                        headerLabelStyle = new GUIStyle(EditorStyles.label)
                        {
                            fontSize = 12,
                            fontStyle = FontStyle.Bold,
                            fixedHeight = 18
                        };
                    }
                    return headerLabelStyle;
                }
            }

            private static GUIStyle versionHeaderStyle;
            public static GUIStyle VersionHeaderLableStyle
            {
                get
                {
                    if (versionHeaderStyle == null)
                    {
                        versionHeaderStyle = new GUIStyle(EditorStyles.label)
                        {
                            fontSize = 12,
                            fontStyle = FontStyle.Bold,
                            fixedHeight = 18,
                            alignment = TextAnchor.MiddleRight
                        };
                    }
                    return versionHeaderStyle;
                }
            }

            private static GUIStyle boxStyle;
            public static GUIStyle BoxStyle
            {
                get
                {
                    if (boxStyle == null)
                    {
                        boxStyle = new GUIStyle();
                        boxStyle.padding = new RectOffset(15, 15, 10, 10);
                    }
                    return boxStyle;
                }
            }

            private static GUIStyle linkStyle;
            public static GUIStyle LinkLableStyle
            {
                get
                {
                    if (linkStyle == null)
                    {
                        linkStyle = new GUIStyle(EditorStyles.label)
                        {
                            fontSize = 12,
                            fontStyle = FontStyle.Bold,
                            fixedHeight = 18,
                            alignment = TextAnchor.MiddleLeft
                        };
                    }
                    return linkStyle;
                }
            }
            #endregion
        }
    }

}
