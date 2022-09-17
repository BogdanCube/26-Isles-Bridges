using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Hoopsly.Settings;

namespace Hoopsly.Internal
{
    public class FPS_MeasureTool
    {
        private List<int> m_readings = new List<int>();
        public int CurrentMeasureCount
        {
            get { return m_readings.Count; }
        }
        private bool m_isMeasuring = false;
        public bool MeasurmentInProcess
        {
            get { return m_isMeasuring; }
        }

        private float m_measurementIntervals = 1f;
        public float MeasureInterval
        {
            set { m_measurementIntervals = value; }
        }

        public void StartMeasurement()
        {
            m_isMeasuring = true;
            FPS_MeasurementTask();
        }
        private async Task FPS_MeasurementTask()
        {
            m_readings.Clear();
            while (m_isMeasuring)
            {
                m_readings.Add((int)(1f / UnityEngine.Time.unscaledDeltaTime));
                HoopslyLogger.LogMessage($"===[Measure tick!_Current FPS:_{m_readings[m_readings.Count - 1]}_Current records:_{m_readings.Count}]===", HoopslyLogLevel.Verbose);
                await Task.Delay((int)(m_measurementIntervals * 1000f));
            }
        }
        public int[] StopMeasurement()
        {
            int[] result = new int[3];
            m_isMeasuring = false;
            int[] m_readingsArray = m_readings.ToArray();
            Array.Sort(m_readingsArray);
            result[0] = Average(m_readingsArray);
            result[1] = (int)Percentile(m_readingsArray, .01f);
            result[2] = (int)Percentile(m_readingsArray, .05f);
            m_readings.Clear();
            return result;
        }
        private int Average(int[] sequence)
        {
            int sum = 0;
            for (int i = 0; i < sequence.Length; i++)
            {
                sum += sequence[i];
            }
            return sum / sequence.Length;
        }
        //percentile 0-1 range
        private double Percentile(int[] sequence, double percentile)
        {
            int N = sequence.Length;
            double n = (N - 1) * percentile + 1;
            if (n == 1d) return sequence[0];
            else if (n == N) return sequence[N - 1];
            else
            {
                int k = (int)n;
                double d = n - k;
                return sequence[k - 1] + d * (sequence[k] - sequence[k - 1]);
            }
        }
    }
}