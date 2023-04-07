using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.UI
{
    public class KeyBindingPanel : MonoBehaviour, ISingleton
    {
        [SerializeField] private TextMeshProUGUI _oldKeyTxt;
        [SerializeField] private TextMeshProUGUI _newKeyTxt;

        

        void OnEnable()
        {
            RegisterToContainer();
        }

        void OnDisable()
        {
            UnRegisterFromContainer();
        }

        public void SetReBindingText(string oldKeyString, string newKeyString)
        {
            this._oldKeyTxt.text = oldKeyString;
            this._newKeyTxt.text = newKeyString;
        }

        public void SetOldKeyText(string oldKeyString)
        {
            this._oldKeyTxt.text = oldKeyString;
        }

        public void SetNewKeyText(string newKeyString)
        {
            this._newKeyTxt.text = newKeyString;
        }

        public void RegisterToContainer()
        {
            SingletonContainer.Register(this);
        }

        public void UnRegisterFromContainer()
        {
            SingletonContainer.Register(this);
        }
    }
}
