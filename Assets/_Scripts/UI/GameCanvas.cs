using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class GameCanvas : MonoBehaviour, ISingleton
    {
        [SerializeField] GameObject LeftPanel, RightPanel, KeyBindingPanel;
        
        void Awake()
        {
            RegisterToContainer();
        }

        public void TurnOnKeyBindingPanel()
        {
            KeyBindingPanel.SetActive(true);
        }

        public void TurnOffkeyBindingPanel()
        {
            KeyBindingPanel.SetActive(false);
        }

        public void RegisterToContainer()
        {
            SingletonContainer.Register(this);
        }

        public void UnRegisterFromContainer()
        {
            SingletonContainer.UnRegister(this);
        }
    }    
}
