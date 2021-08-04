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
        public ECollection collection;
        public int cardID;
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
        public IEnumerator Move(Vector3 target)
        {
            while (transform.position != target)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, 8f * Time.deltaTime);
                yield return null;
            }
        }
        */
        
        public void ONClickAction()
        {
            Settings.Fsm.Invoke("SwapCard", gameObject);
        }
    }
}
