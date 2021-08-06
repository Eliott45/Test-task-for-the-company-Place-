using System;
using System.Collections;
using AxGrid.Path;
using UnityEngine;

namespace AxGrid.Hello
{
    public class Card : MonoBehaviour, IClicked
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
        public Vector3 target;
        

        private void FixedUpdate()
        {
            if (!moving) return;
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            if (target == transform.position)
            {
                moving = false;
            }
        }

        public void SetLayer(int i)
        {
            GetComponent<SpriteRenderer>().sortingOrder = i;
            foreach (Transform child in gameObject.transform)
            {
                child.GetComponent<SpriteRenderer>().sortingOrder = i;
            }
        }
        
        /*
        public void Move(Vector3 target)
        {
            var currentPos = Vector3.zero;
            var newPos = target; 
            var a = CPath.Create().Action(() =>
            {
                currentPos = transform.position;
            }).EasingLinear(1f, 0f, 1f, value =>
            {
                transform.position = Vector3.Lerp(currentPos, newPos, value);
            });
            
        }
        */

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
