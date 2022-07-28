using UnityEngine;
using UnityEngine.EventSystems;

namespace StarterAssets
{
    public class UICanvasShowJoystickOnTouch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public GameObject joystick;

        void Start()
        {
            joystick.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            joystick.transform.position = eventData.position;
            joystick.SetActive(true);
            joystick.SendMessage("OnPointerDown", eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            joystick.SendMessage("OnPointerUp", eventData);
            joystick.SetActive(false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            joystick.SendMessage("OnDrag", eventData);
        }
    }
}

