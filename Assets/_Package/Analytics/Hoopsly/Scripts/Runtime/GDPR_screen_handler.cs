using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace Hoopsly.GRDP
{
    public class GDPR_screen_handler : MonoBehaviour
    {
        [SerializeField] private Button m_aceptButton;
        public Button AceptButton
        {
            get { return m_aceptButton; }
        }
        [SerializeField] private Shadow m_acepeteButtonShadow;
        public Shadow AcepetButtonShadow
        {
            get 
            {
                if (m_acepeteButtonShadow == null)
                    m_acepeteButtonShadow = m_aceptButton.GetComponent<Shadow>();
                return m_acepeteButtonShadow; 
            }
        }
        [SerializeField] private Text m_aceptButtonText;
        public Text AceptButtonText
        {
            get
            {
                if (m_aceptButtonText == null)
                    m_aceptButtonText = m_aceptButton.GetComponentInChildren<Text>();
                return m_aceptButtonText;
            }
        }
        public Button m_declineButton;

        [SerializeField] private Image m_messagePanelImage;
        public Image PanelImage
        {
            get
            {
                return m_messagePanelImage;
            }
        }
        [SerializeField] private Shadow m_messagePanelShadow;
        public Shadow PanelShadow
        {
            get
            {
                if (m_messagePanelShadow == null)
                    m_messagePanelShadow = PanelImage.GetComponent<Shadow>();
                return m_messagePanelShadow;
            }
        }


        [SerializeField] private Text m_titleText;
        public Text TitleText
        {
            get { return m_titleText; }
        }

        [SerializeField] private Shadow m_titleTextShadow;
        public Shadow TitleTextShadow
        {
            get 
            {
                if (m_titleTextShadow == null)
                    m_titleTextShadow = TitleText.GetComponent<Shadow>();
                return m_titleTextShadow; 
            }
        }


        [SerializeField] private Text m_messageText;
        public Text MessageText
        {
            get { return m_messageText; }
        }
        [SerializeField] private Shadow m_messageTextShadow;
        public Shadow MessageTextShadow
        {
            get
            {
                if (m_messageTextShadow == null)
                    m_messageTextShadow = MessageText.GetComponent<Shadow>();
                return m_messageTextShadow;
            }
        }

        [SerializeField] private string m_termsOfService_Link;
        public string TermsOfService_URL
        {
            get { return m_termsOfService_Link; }
            set { m_termsOfService_Link = value; }
        }
        [SerializeField] private string m_privacyPolicy_Link;
        public string PrivacyPolicy_URL
        {
            get { return m_privacyPolicy_Link; }
            set { m_privacyPolicy_Link = value; }
        }
        public Button m_termsOfService_Button;
        public Button m_privacyPolicy_Button;

        private ConsentValue m_currentConsent = ConsentValue.none;
        public ConsentValue GetConsentValue
        {
            get { return m_currentConsent; }
        }

        private void Awake()
        {
            SetupScreen();
        }

        public void SetupScreen()
        {
            m_aceptButton.onClick.AddListener(AcepteConsent);
            m_declineButton.onClick.AddListener(DeclineConsent);

            if (m_termsOfService_Link != "")
            {
                m_termsOfService_Button.onClick.AddListener(delegate { OpenLink(m_termsOfService_Link); });
            }

            if(m_privacyPolicy_Link != "")
            {
                m_privacyPolicy_Button.onClick.AddListener(delegate { OpenLink(m_privacyPolicy_Link); });
            }
        }

        private void AcepteConsent()
        {
            m_currentConsent = ConsentValue.accepted;
        }

        private void DeclineConsent()
        {
            m_currentConsent = ConsentValue.declined;
        }

        private void OpenLink(string link)
        {
            Application.OpenURL(link);
        }
    }

    public enum ConsentValue { none, accepted, declined };
}
