

//// Dashboard: handles a dashboard UI with signals and gauges

//using UnityEngine;
//using UnityEngine.UI;
//using System;
//using EdyCommonTools;

//namespace VehiclePhysics.UI
//{
//    public class Dashboard : MonoBehaviour
//    {
//        public VehicleBase vehicle;

//        [Header("Needles")]
//        public Needle speedNeedle = new Needle();
//        public Needle rpmNeedle = new Needle();

//        [Header("Dashboard elements")]
//        public GameObject stalledSignal;
//        public GameObject handbrakeSignal;
//        public GameObject warningSignal;
//        public GameObject retarderSignal;
//        public GameObject axleDiffLockSignal;
//        public GameObject centerDiffLockSignal;
//        public GameObject fullDiffLockSignal;
//        public GameObject singleAxleDriveSignal;
//        public GameObject singleAxleDiffLockSignal;

//        [Header("UI labels")]
//        public Text gearLabel;
//        public Text speedMphLabel;

//        [Serializable]
//        public class Needle
//        {
//            public Transform needle;
//            public float minValue = 0.0f;
//            public float maxValue = 200.0f;
//            public float angleAtMinValue = 135.0f;
//            public float angleAtMaxValue = -135.0f;

//            public void SetValue(float value)
//            {
//                if (needle == null) return;

//                float x = (value - minValue) / (maxValue - minValue);
//                float angle = MathUtility.UnclampedLerp(angleAtMinValue, angleAtMaxValue, x);

//                needle.localRotation = Quaternion.Euler(0, 0, angle);
//            }
//        }

//        float m_lastVehicleTime;
//        float m_warningTime;

//        InterpolatedFloat m_speedMs = new InterpolatedFloat();
//        InterpolatedFloat m_engineRpm = new InterpolatedFloat();

//        void OnEnable()
//        {
//            m_lastVehicleTime = -1.0f;
//            m_warningTime = -10.0f;

//            if (vehicle == null)
//                vehicle = GetComponentInParent<VehicleBase>();
//        }

//        void FixedUpdate()
//        {
//            if (vehicle == null || !vehicle.isActiveAndEnabled) return;

//            m_speedMs.Set(vehicle.data.Get(Channel.Vehicle, VehicleData.Speed) / 1000.0f);
//            m_engineRpm.Set(vehicle.data.Get(Channel.Vehicle, VehicleData.EngineRpm) / 1000.0f);
//        }

//        void Update()
//        {
//            if (vehicle == null || !vehicle.isActiveAndEnabled)
//            {
//                ResetDashboard();
//                return;
//            }

//            UpdateNeedles();
//            UpdateWarningSignals();
//            UpdateGearAndSpeedLabels();

//            m_lastVehicleTime = vehicle.time;
//        }

//        void ResetDashboard()
//        {
//            speedNeedle.SetValue(0.0f);
//            rpmNeedle.SetValue(0.0f);
//            m_speedMs.Reset(0.0f);
//            m_engineRpm.Reset(0.0f);

//            SetAllSignals(false);

//            if (gearLabel != null) gearLabel.text = "-";
//            if (speedMphLabel != null) speedMphLabel.text = "-";

//            m_lastVehicleTime = -1.0f;
//        }

//        void UpdateNeedles()
//        {
//            float frameRatio = InterpolatedFloat.GetFrameRatio();

//            float speedMs = m_speedMs.GetInterpolated(frameRatio);
//            float engineRpm = m_engineRpm.GetInterpolated(frameRatio);
//            if (speedMs < 0) speedMs = 0.0f;
//            if (engineRpm < 0) engineRpm = 0.0f;

//            speedNeedle.SetValue(speedMs * 3.6f);
//            rpmNeedle.SetValue(engineRpm);
//        }

//        void UpdateWarningSignals()
//        {
//            int[] vehicleData = vehicle.data.Get(Channel.Vehicle);

//            bool abs = vehicleData[VehicleData.AbsEngaged] != 0;
//            bool tcs = vehicleData[VehicleData.TcsEngaged] != 0;
//            bool esc = vehicleData[VehicleData.EscEngaged] != 0;
//            bool asr = vehicleData[VehicleData.AsrEngaged] != 0;

//            if (abs || tcs || esc || asr)
//                m_warningTime = Time.time;

//            SetEnabled(warningSignal, Time.time - m_warningTime < 0.5f && Mathf.Repeat(Time.time, 0.3f) < 0.2f);
//        }

//        void UpdateGearAndSpeedLabels()
//        {
//            int[] vehicleData = vehicle.data.Get(Channel.Vehicle);

//            if (speedMphLabel != null)
//                speedMphLabel.text = (m_speedMs.GetInterpolated(InterpolatedFloat.GetFrameRatio()) * 2.237f).ToString("0") + "\nmph";

//            if (gearLabel != null)
//            {
//                int gearId = vehicleData[VehicleData.GearboxGear];
//                int gearMode = vehicleData[VehicleData.GearboxMode];
//                bool switchingGear = vehicleData[VehicleData.GearboxShifting] != 0;

//                gearLabel.text = GetGearText(gearId, gearMode, switchingGear);
//            }
//        }

//        string GetGearText(int gearId, int gearMode, bool switchingGear)
//        {
//            switch (gearMode)
//            {
//                case 0: return gearId == 0 ? (switchingGear ? " " : "N") : (gearId > 0 ? gearId.ToString() : "R" + (-gearId));
//                case 1: return "P";
//                case 2: return gearId < -1 ? "R" + (-gearId) : "R";
//                case 3: return "N";
//                case 4: return gearId > 0 ? "D" + gearId : "D";
//                case 5: return gearId > 0 ? "L" + gearId : "L";
//                default: return "-";
//            }
//        }

//        void SetEnabled(GameObject go, bool active)
//        {
//            if (go != null) go.SetActive(active);
//        }

//        void SetAllSignals(bool state)
//        {
//            SetEnabled(stalledSignal, state);
//            SetEnabled(handbrakeSignal, state);
//            SetEnabled(warningSignal, state);
//            SetEnabled(retarderSignal, state);
//            SetEnabled(fullDiffLockSignal, state);
//            SetEnabled(axleDiffLockSignal, state);
//            SetEnabled(centerDiffLockSignal, state);
//            SetEnabled(singleAxleDriveSignal, state);
//            SetEnabled(singleAxleDiffLockSignal, state);
//        }
//    }
//}
