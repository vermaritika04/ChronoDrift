////--------------------------------------------------------------
////      Vehicle Physics Pro: advanced vehicle physics kit
////          Copyright © 2011-2019 Angel Garcia "Edy"
////        http://vehiclephysics.com | @VehiclePhysics
////--------------------------------------------------------------

//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.EventSystems;
//using System;
//using EdyCommonTools;

//namespace VehiclePhysics.UI
//{
//    public class DashboardUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
//    {
//        public VehicleBase vehicle;

//        // Ignition Key UI
//        public Text start;
//        public Text accOn;
//        public Text off;
//        public Color normalColor = GColor.ParseColorHex("#999999");
//        public Color highlightColor = Color.white;
//        bool m_startPressed = false, m_accOnPressed = false, m_offPressed = false, m_keyReleased = false;

//        // Gear Mode Selector UI
//        public Color selectedColor = GColor.ParseColorHex("#E6E6E6");
//        public Color unselectedColor = GColor.ParseColorHex("#999999");
//        public Transform selector;
//        public Graphic gearM, gearP, gearR, gearN, gearD, gearL;
//        int m_prevGearMode = -1, m_prevGearInput = -1, m_newGearMode = -1;

//        // Dashboard UI
//        public Needle speedNeedle = new Needle();
//        public Needle rpmNeedle = new Needle();
//        public GameObject stalledSignal, handbrakeSignal, warningSignal, retarderSignal;
//        public GameObject axleDiffLockSignal, centerDiffLockSignal, fullDiffLockSignal;
//        public GameObject singleAxleDriveSignal, singleAxleDiffLockSignal;
//        public Text gearLabel, speedMphLabel;
//        float m_lastVehicleTime, m_warningTime;
//        InterpolatedFloat m_speedMs = new InterpolatedFloat();
//        InterpolatedFloat m_engineRpm = new InterpolatedFloat();

//        [Serializable]
//        public class Needle
//        {
//            public Transform needle;
//            public float minValue = 0.0f, maxValue = 200.0f, angleAtMinValue = 135.0f, angleAtMaxValue = -135.0f;
//            public void SetValue(float value)
//            {
//                if (needle == null) return;
//                float x = (value - minValue) / (maxValue - minValue);
//                float angle = MathUtility.UnclampedLerp(angleAtMinValue, angleAtMaxValue, x);
//                needle.localRotation = Quaternion.Euler(0, 0, angle);
//            }
//        }

//        void OnEnable()
//        {
//            m_startPressed = m_accOnPressed = m_offPressed = m_keyReleased = false;
//            m_prevGearMode = m_prevGearInput = m_newGearMode = -1;
//            m_lastVehicleTime = -1.0f;
//            m_warningTime = -10.0f;
//        }

//        void FixedUpdate()
//        {
//            if (vehicle == null) return;

//            // Ignition Key Handling
//            int key = vehicle.data.Get(Channel.Input, InputData.Key);
//            SetHighlight(start, key == 1);
//            SetHighlight(accOn, key == 0);
//            SetHighlight(off, key == -1);
//            if (m_startPressed) StartPressed();
//            if (m_accOnPressed) AccOnPressed();
//            if (m_offPressed) OffPressed();
//            if (m_keyReleased) ReleaseKey();
//            m_startPressed = m_accOnPressed = m_offPressed = m_keyReleased = false;

//            // Gear Mode Handling
//            int gearInput = vehicle.data.Get(Channel.Input, InputData.AutomaticGear);
//            int gearMode = vehicle.data.Get(Channel.Vehicle, VehicleData.GearboxMode);
//            if (gearMode != m_prevGearMode) { HighlightGear(gearMode); m_prevGearMode = gearMode; }
//            if (gearInput != m_prevGearInput) { MoveGearSelector(gearInput); m_prevGearInput = gearInput; }
//            if (m_newGearMode >= 0) { vehicle.data.Set(Channel.Input, InputData.AutomaticGear, m_newGearMode); m_newGearMode = -1; }
//        }

//        public void OnPointerDown(PointerEventData eventData)
//        {
//            if (vehicle == null || eventData.button != PointerEventData.InputButton.Left) return;
//            GameObject pressed = eventData.pointerCurrentRaycast.gameObject;
//            if (start != null && pressed == start.gameObject) m_startPressed = true;
//            else if (accOn != null && pressed == accOn.gameObject) m_accOnPressed = true;
//            else if (off != null && pressed == off.gameObject) m_offPressed = true;
//            else m_newGearMode = GraphicToGear(pressed.GetComponentInChildren<Graphic>());
//        }

//        public void OnPointerUp(PointerEventData eventData) { m_keyReleased = true; }

//        void StartPressed() { if (vehicle == null) return; int key = vehicle.data.Get(Channel.Input, InputData.Key); if (key == -1) vehicle.data.Set(Channel.Input, InputData.Key, 0); else if (key == 0) vehicle.data.Set(Channel.Input, InputData.Key, 1); }
//        void AccOnPressed() { if (vehicle != null) vehicle.data.Set(Channel.Input, InputData.Key, 0); }
//        void OffPressed() { if (vehicle != null) vehicle.data.Set(Channel.Input, InputData.Key, -1); }
//        void ReleaseKey() { if (vehicle == null) return; if (vehicle.data.Get(Channel.Input, InputData.Key) == 1) vehicle.data.Set(Channel.Input, InputData.Key, 0); }

//        void HighlightGear(int gearMode) { ClearEngagedGearMode(); SetGraphicColor(GearToGraphic(gearMode), selectedColor); }
//        void MoveGearSelector(int gearInput) { if (selector != null) selector.position = GearToGraphic(gearInput)?.transform.position ?? selector.position; }
//        void ClearEngagedGearMode() { SetGraphicColor(gearM, unselectedColor); SetGraphicColor(gearP, unselectedColor); SetGraphicColor(gearR, unselectedColor); SetGraphicColor(gearN, unselectedColor); SetGraphicColor(gearD, unselectedColor); SetGraphicColor(gearL, unselectedColor); }
//        Graphic GearToGraphic(int gearMode) { return gearMode switch { 0 => gearM, 1 => gearP, 2 => gearR, 3 => gearN, 4 => gearD, 5 => gearL, _ => null }; }
//        int GraphicToGear(Graphic graphic) { return graphic == gearM ? 0 : graphic == gearP ? 1 : graphic == gearR ? 2 : graphic == gearN ? 3 : graphic == gearD ? 4 : graphic == gearL ? 5 : -1; }
//        void SetHighlight(Text text, bool highlight) { if (text != null) text.color = highlight ? highlightColor : normalColor; }
//        void SetGraphicColor(Graphic graphic, Color color) { if (graphic != null) graphic.color = color; }
//    }
//}
//--------------------------------------------------------------
//      Vehicle Physics Pro: advanced vehicle physics kit
//          Copyright © 2011-2019 Angel Garcia "Edy"
//        http://vehiclephysics.com | @VehiclePhysics
//--------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using EdyCommonTools;

namespace VehiclePhysics.UI
{
    public class DashboardUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public VehicleBase vehicle;

        // Ignition Key UI
        public Text start;
        public Text accOn;
        public Text off;
        public Color normalColor = GColor.ParseColorHex("#999999");
        public Color highlightColor = Color.white;
        bool m_startPressed = false, m_accOnPressed = false, m_offPressed = false, m_keyReleased = false;

        // Gear Mode Selector UI
        public Color selectedColor = GColor.ParseColorHex("#E6E6E6");
        public Color unselectedColor = GColor.ParseColorHex("#999999");
        public Transform selector;
        public Graphic gearM, gearP, gearR, gearN, gearD, gearL;
        int m_prevGearMode = -1, m_prevGearInput = -1, m_newGearMode = -1;

        // Dashboard UI
        public Needle speedNeedle = new Needle();
        public Needle rpmNeedle = new Needle();
        public GameObject warningSignal, handbrakeSignal;
        public Text gearLabel, speedMphLabel;
        float m_lastVehicleTime, m_warningTime;
        InterpolatedFloat m_speedMs = new InterpolatedFloat();
        InterpolatedFloat m_engineRpm = new InterpolatedFloat();

        [Serializable]
        public class Needle
        {
            public Transform needle;
            public float minValue = 0.0f, maxValue = 200.0f, angleAtMinValue = 135.0f, angleAtMaxValue = -135.0f;
            public void SetValue(float value)
            {
                if (needle == null) return;
                float x = (value - minValue) / (maxValue - minValue);
                float angle = MathUtility.UnclampedLerp(angleAtMinValue, angleAtMaxValue, x);
                needle.localRotation = Quaternion.Euler(0, 0, angle);
            }
        }

        void OnEnable()
        {
            m_startPressed = m_accOnPressed = m_offPressed = m_keyReleased = false;
            m_prevGearMode = m_prevGearInput = m_newGearMode = -1;
            m_lastVehicleTime = -1.0f;
            m_warningTime = -10.0f;
        }

        void FixedUpdate()
        {
            if (vehicle == null) return;

            // Ignition Key Handling
            int key = vehicle.data.Get(Channel.Input, InputData.Key);
            SetHighlight(start, key == 1);
            SetHighlight(accOn, key == 0);
            SetHighlight(off, key == -1);
            if (m_startPressed) StartPressed();
            if (m_accOnPressed) AccOnPressed();
            if (m_offPressed) OffPressed();
            if (m_keyReleased) ReleaseKey();
            m_startPressed = m_accOnPressed = m_offPressed = m_keyReleased = false;

            // Gear Mode Handling
            int gearInput = vehicle.data.Get(Channel.Input, InputData.AutomaticGear);
            int gearMode = vehicle.data.Get(Channel.Vehicle, VehicleData.GearboxMode);
            if (gearMode != m_prevGearMode) { HighlightGear(gearMode); m_prevGearMode = gearMode; }
            if (gearInput != m_prevGearInput) { MoveGearSelector(gearInput); m_prevGearInput = gearInput; }
            if (m_newGearMode >= 0) { vehicle.data.Set(Channel.Input, InputData.AutomaticGear, m_newGearMode); m_newGearMode = -1; }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (vehicle == null || eventData.button != PointerEventData.InputButton.Left) return;
            GameObject pressed = eventData.pointerCurrentRaycast.gameObject;
            if (start != null && pressed == start.gameObject) m_startPressed = true;
            else if (accOn != null && pressed == accOn.gameObject) m_accOnPressed = true;
            else if (off != null && pressed == off.gameObject) m_offPressed = true;
            else m_newGearMode = GraphicToGear(pressed.GetComponentInChildren<Graphic>());
        }

        public void OnPointerUp(PointerEventData eventData) { m_keyReleased = true; }

        void StartPressed() { if (vehicle != null) vehicle.data.Set(Channel.Input, InputData.Key, 1); }
        void AccOnPressed() { if (vehicle != null) vehicle.data.Set(Channel.Input, InputData.Key, 0); }
        void OffPressed() { if (vehicle != null) vehicle.data.Set(Channel.Input, InputData.Key, -1); }
        void ReleaseKey() { if (vehicle == null) return; if (vehicle.data.Get(Channel.Input, InputData.Key) == 1) vehicle.data.Set(Channel.Input, InputData.Key, 0); }

        void HighlightGear(int gearMode) { ClearEngagedGearMode(); SetGraphicColor(GearToGraphic(gearMode), selectedColor); }
        void MoveGearSelector(int gearInput) { if (selector != null) selector.position = GearToGraphic(gearInput)?.transform.position ?? selector.position; }
        void ClearEngagedGearMode() { SetGraphicColor(gearM, unselectedColor); SetGraphicColor(gearP, unselectedColor); SetGraphicColor(gearR, unselectedColor); SetGraphicColor(gearN, unselectedColor); SetGraphicColor(gearD, unselectedColor); SetGraphicColor(gearL, unselectedColor); }
        Graphic GearToGraphic(int gearMode) { return gearMode switch { 0 => gearM, 1 => gearP, 2 => gearR, 3 => gearN, 4 => gearD, 5 => gearL, _ => null }; }
        int GraphicToGear(Graphic graphic) { return graphic == gearM ? 0 : graphic == gearP ? 1 : graphic == gearR ? 2 : graphic == gearN ? 3 : graphic == gearD ? 4 : graphic == gearL ? 5 : -1; }
        void SetHighlight(Text text, bool highlight) { if (text != null) text.color = highlight ? highlightColor : normalColor; }
        void SetGraphicColor(Graphic graphic, Color color) { if (graphic != null) graphic.color = color; }
    }
}

