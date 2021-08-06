using System;
using System.Collections;
using AxGrid.Base;
using AxGrid.Path;
using UnityEngine;

namespace AxGrid.Hello
{
    public class Card : MonoBehaviourExt, IClicked
    {
        public enum ECollection
        {
            A,
            B
        };
        
        [Header("Set in inspector:")] [SerializeField]
        private float _speed = 9f;
        
        [Header("Set dynamical:")]
        public int cardID;
        public ECollection collection;
        
        public bool moving;

        public void SetLayer(int i)
        {
            GetComponent<SpriteRenderer>().sortingOrder = i;
            foreach (Transform child in gameObject.transform)
            {
                child.GetComponent<SpriteRenderer>().sortingOrder = i;
            }
        }
        
        
        public void Move(Vector3 target)
        {
            var currentPos = Vector3.zero;
            var newPos = target; 
            Path = CPath.Create().Action(() =>
            {
                currentPos = transform.position;
            }).EasingLinear(0.5f,0f,  1f, value =>
            {
                transform.position = Vector3.Lerp(currentPos, newPos, value);
            });
        }

        public void Delete()
        {
            Destroy(gameObject);
        }
        
        public void ONClickAction()
        {
            Settings.Fsm.Invoke("SwapCard", this);
            
        }
    }
}
