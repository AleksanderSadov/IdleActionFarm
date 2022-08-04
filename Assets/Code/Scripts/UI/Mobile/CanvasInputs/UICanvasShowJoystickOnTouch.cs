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
            if (Application.isEditor && eventData.button != PointerEventData.InputButton.Right)
            {
                return;
            }

            joystick.transform.position = eventData.position;
            joystick.SetActive(true);
            joystick.SendMessage("OnPointerDown", eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (joystick.activeSelf)
            {
                joystick.SendMessage("OnPointerUp", eventData);
                joystick.SetActive(false);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (joystick.activeSelf)
            {
                joystick.SendMessage("OnDrag", eventData);
            }
        }
    }
}

