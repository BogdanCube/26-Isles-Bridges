using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Firebase;
using Firebase.Analytics;

namespace Hoopsly.Internal.Events
{
    public delegate void StringDelegate(string value);
    public class HoopslyAnalicsEvent
    {
        private string m_eventName;
        public string EventName
        {
            get { return m_eventName; }
        }


        private HoopslyEventParameter[] m_eventParameters;
        public HoopslyEventParameter[] EventParameters
        {
            get { return m_eventParameters; }
        }

        public HoopslyAnalicsEvent(string eventName)
        {
            m_eventName = eventName;
        }

        public HoopslyAnalicsEvent(string eventName, HoopslyEventParameter eventParameter)
        {
            m_eventName = eventName;
            m_eventParameters = new HoopslyEventParameter[] { eventParameter };
        }

        public HoopslyAnalicsEvent(string eventName, HoopslyEventParameter[] eventParameters)
        {
            m_eventName = eventName;
            m_eventParameters = eventParameters;
        }

        public HoopslyAnalicsEvent(string eventName, HoopslyEventParameter[] eventParameters, HoopslyEvent_String newEvent)
        {
            m_eventName = eventName;
            m_eventParameters = eventParameters;

            OnDispatchEventString = new HoopslyEvent_String[] { newEvent };
        }

        public HoopslyAnalicsEvent(string eventName, HoopslyEventParameter[] eventParameters, HoopslyEvent_String[] newEvents)
        {
            m_eventName = eventName;
            m_eventParameters = eventParameters;
            OnDispatchEventString = newEvents;
        }

        public HoopslyAnalicsEvent(string eventName, HoopslyEventParameter[] eventParameters, HoopslyEvent_Double newEvent)
        {
            m_eventName = eventName;
            m_eventParameters = eventParameters;

            OnDispatchEventDouble = new HoopslyEvent_Double[] { newEvent };
        }

        public HoopslyAnalicsEvent(string eventName, HoopslyEventParameter[] eventParameters, HoopslyEvent_Double[] newEvent)
        {
            m_eventName = eventName;
            m_eventParameters = eventParameters;
            OnDispatchEventDouble = newEvent;
        }

        public HoopslyAnalicsEvent(string eventName, HoopslyEventParameter[] eventParameters, HoopslyEvent_Long newEvent)
        {
            m_eventName = eventName;
            m_eventParameters = eventParameters;

            OnDispatchEventLong = new HoopslyEvent_Long[] { newEvent };
        }

        public HoopslyAnalicsEvent(string eventName, HoopslyEventParameter[] eventParameters, HoopslyEvent_Long[] newEvent)
        {
            m_eventName = eventName;
            m_eventParameters = eventParameters;

            OnDispatchEventLong = newEvent;
        }

        private HoopslyEvent_String[] OnDispatchEventString;
        private HoopslyEvent_Double[] OnDispatchEventDouble;
        private HoopslyEvent_Long[] OnDispatchEventLong;

        public void Dispatch()
        {
            EventLogActions(DispatchOrder.BeforeLog);
            if (m_eventParameters != null)
            {
                List<Parameter> eventParameters = new List<Parameter>();
                for (int i = 0; i < m_eventParameters.Length; i++)
                {
                    if (Hoopsly.Settings.HoopslySettings.Instance.GeneralSettings.HoopslyEventsLogLevel == Settings.HoopslyLogLevel.Verbose)
                    {
                        if (m_eventParameters[i] != null)
                        {
                            HoopslyLogger.LogMessage($"===[Parameter with id: {i} IS exist. Parameter name is: {m_eventParameters[i].ParamName}. Parameter value is: {m_eventParameters[i].ParamValue}]===", Settings.HoopslyLogLevel.Verbose);
                        }
                        else
                        {
                            HoopslyLogger.LogMessage($"===[Parameter with id: {i} NOT exist]===", Settings.HoopslyLogLevel.Verbose, H_LogType.Error);
                        }
                        if (eventParameters == null)
                        {
                            HoopslyLogger.LogMessage("===[Parameters is NULL]===", Settings.HoopslyLogLevel.Verbose, H_LogType.Error);
                        }
                    }
                    Parameter fbParam = m_eventParameters[i].ConvertToFirebaseParameter();
                    if (fbParam != null)
                    {
                        eventParameters.Add(fbParam);
                    }
                }
                HoopslyLogger.LogMessage($"===[Firebase: {m_eventName} was logged]===", Settings.HoopslyLogLevel.Verbose);
                FirebaseAnalytics.LogEvent(m_eventName, eventParameters.ToArray());
            }
            else
            {
                FirebaseAnalytics.LogEvent(m_eventName);
            }
            EventLogActions(DispatchOrder.AfterLog);
        }

        private void EventLogActions(DispatchOrder dispatchOrder)
        {
            if(OnDispatchEventString!=null)
            {
                foreach (HoopslyEvent_String eventAction in OnDispatchEventString)
                {
                    if (eventAction.DispatchOrder == dispatchOrder)
                    {
                        eventAction.InvokeAction();
                    }
                }
            }

            if(OnDispatchEventDouble!=null)
            {
                foreach (HoopslyEvent_Double eventAction in OnDispatchEventDouble)
                {
                    if (eventAction.DispatchOrder == dispatchOrder)
                        eventAction.Invoke(eventAction.RelativeParameterValue);
                }
            }

            if(OnDispatchEventLong!=null)
            {
                foreach (HoopslyEvent_Long eventAction in OnDispatchEventLong)
                {
                    if (eventAction.DispatchOrder == dispatchOrder)
                        eventAction.Invoke(eventAction.RelativeParameterValue);
                }
            }
        }
    }

    public class HoopslyEventParameter
    {
        private enum ValueType { str, lng, doubl, none };
        private ValueType valueType = ValueType.none;

        private string m_paramName;
        public string ParamName
        {
            get { return m_paramName; }
        }

        private object m_paramValue;
        public object ParamValue
        {
            get
            {
                return m_paramValue;
            }
        }


        public HoopslyEventParameter(string paramName, string paramValue)
        {
            m_paramName = paramName;
            m_paramValue = paramValue;
            valueType = ValueType.str;
        }

        public HoopslyEventParameter(string paramName, long paramValue)
        {
            m_paramName = paramName;
            m_paramValue = paramValue;
            valueType = ValueType.lng;
        }

        public HoopslyEventParameter(string paramName, double paramValue)
        {
            m_paramName = paramName;
            m_paramValue = paramValue;
            valueType = ValueType.doubl;
        }


        public Parameter ConvertToFirebaseParameter()
        {
            if(valueType==ValueType.str)
            {
                return new Parameter(ParamName, (string)ParamValue);
            }
            else if(valueType==ValueType.doubl)
            {
                return new Parameter(ParamName, (double)ParamValue);
            }    
            else if(valueType==ValueType.lng)
            {
                return new Parameter(ParamName, (long)ParamValue);
            }
            else
            {
                return null;
            }

        }
    }

    [System.Serializable]
    public class HoopslyEvent_Long : UnityEvent<long>
    {
        private long m_relativeParameterValue;
        public long RelativeParameterValue
        {
            get { return m_relativeParameterValue; }
        }
        private DispatchOrder m_dispatchOrder;
        public DispatchOrder DispatchOrder { get { return m_dispatchOrder; } }
        public HoopslyEvent_Long(long relativeParameterValue, DispatchOrder dispatchOrder, UnityAction<long> relativeAction)
        {
            m_relativeParameterValue = relativeParameterValue;
            m_dispatchOrder = dispatchOrder;
            this.AddListener(relativeAction);
        }
    }

    [System.Serializable]
    public class HoopslyEvent_String
    {
        private string m_relativeParameterValue;
        public string RelativeParameterValue
        {
            get { return m_relativeParameterValue; }
        }
        private DispatchOrder m_dispatchOrder;
        public DispatchOrder DispatchOrder { get { return m_dispatchOrder; } }
        StringDelegate m_delegate;
        public HoopslyEvent_String(string relativeParameterValue, DispatchOrder dispatchOrder, StringDelegate stringDelegate)
        {
            m_relativeParameterValue = relativeParameterValue;
            m_dispatchOrder = dispatchOrder;
            m_delegate = stringDelegate;
        }
        public void InvokeAction()
        {
            m_delegate.Invoke(RelativeParameterValue);
        }
    }

    [System.Serializable]
    public class HoopslyEvent_Double : UnityEvent<double>
    {
        private double m_relativeParameterValue;
        public double RelativeParameterValue
        {
            get { return m_relativeParameterValue; }
        }
        private DispatchOrder m_dispatchOrder;
        public DispatchOrder DispatchOrder { get { return m_dispatchOrder; } }
        public HoopslyEvent_Double(double relativeParameterValue, DispatchOrder dispatchOrder, UnityAction<double> relativeAction)
        {
            m_relativeParameterValue = relativeParameterValue;
            m_dispatchOrder = dispatchOrder;
            this.AddListener(relativeAction);
        }
    }

    public enum DispatchOrder { BeforeLog, AfterLog };

}
