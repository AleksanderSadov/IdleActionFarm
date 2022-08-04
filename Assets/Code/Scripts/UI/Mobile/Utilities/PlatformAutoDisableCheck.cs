using UnityEngine;

namespace StarterAssets
{
    public class PlatformAutoDisableCheck : MonoBehaviour
    {
        private void Start()
        {
            if (!(Application.isMobilePlatform || Application.isEditor))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
