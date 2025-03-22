using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;


namespace MsbFramework.UI
{
    /// <summary>
    /// 界面控件基类（单击、双击、长按）
    /// </summary>
    public class UISceneWidget : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, ISelectHandler, IPointerClickHandler, IPointerEnterHandler
    {
        DateTime OnClickTime;
        [Tooltip("单击的冷却时间")]
        [SerializeField]
        private float Throughtime = 0.5f;

        [Tooltip("单击生效前的等待时间")]
        private float clickDelay = 0.15f;
        [Tooltip("双击的最大事件间隔")]
        [SerializeField]
        private float DoubleClickThreshold = 0.25f;
        /// - OnHover (isOver) 悬停，悬停时传入true，移出时传入false
        //public delegate void onMouseHover(UISceneWidget eventObj, bool isOver);
        //public onMouseHover OnMouseHover = null;
        //void OnHover(bool isOver)
        //{
        //    if (OnMouseHover != null) OnMouseHover(this, isOver);
        //}
        /// - OnPress （isDown）按下时传入true，抬起时传入false
        public delegate void onMousePress(UISceneWidget eventObj, bool isDown);
        public onMousePress OnMousePress = null;
        void OnPress(bool isDown)
        {
            if (OnMousePress != null) OnMousePress(this, isDown);
        }
        /// - OnSelect 相似单击，区别在于选中一次以后再选中将不再触发OnSelect
        public delegate void onMouseSelect(UISceneWidget eventObj, bool selected);
        public onMouseSelect OnMouseSelect = null;
        void OnSelect(bool selected)
        {
            if (OnMouseSelect != null) OnMouseSelect(this, selected);
        }

        public delegate void onMouseHover(UISceneWidget eventObj);
        public onMouseHover OnHover = null;
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (OnHover != null) OnHover(this);
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (OnMouseSelect != null) OnMouseSelect(this, eventData.used);
        }
        /// - OnClick 单击 Throughtime点击间隔时间
        public delegate void onMouseClick(UISceneWidget eventObj);
        public delegate void onMouseDoubleClick(UISceneWidget eventObj);
        public onMouseDoubleClick OnMouseDoubleClick = null;
        /// <summary>
        /// 单击事件
        /// </summary>
        public onMouseClick OnMouseClick = null;
        private float _lastClickTime;      // 上一次点击的时间戳
        private float _nextAllowedClickTime; // 允许下一次单击的时间戳
        private Coroutine _pendingClickCoroutine; // 等待中的单击协程

        public void OnPointerClick(PointerEventData eventData)
        {
            float currTime = Time.time;

            if (currTime - _lastClickTime < DoubleClickThreshold)
            {
                //取消等待中的单击
                if (_pendingClickCoroutine != null)
                {
                    StopCoroutine(_pendingClickCoroutine);
                    _pendingClickCoroutine = null;
                }
                OnMouseDoubleClick?.Invoke(this);
                _nextAllowedClickTime = currTime + Throughtime;
                _lastClickTime = 0;
                return;
            }

            //Debug.Log(DateTime.UtcNow);
            //if (Throughtime > (float)(DateTime.UtcNow - OnClickTime).TotalSeconds)
            //{
            //    return;
            //}
            //OnClickTime = DateTime.UtcNow;
            //if (Input.GetMouseButtonUp(0) && OnMouseClick != null) OnMouseClick(this);
            //单击处理
            if (currTime >= _nextAllowedClickTime)
            {
                _lastClickTime = currTime;
                _pendingClickCoroutine = StartCoroutine(OnTriggerSingleClick());
            }
        }

        IEnumerator OnTriggerSingleClick()
        {
            yield return new WaitForSeconds(clickDelay);
            if (Time.time >= _nextAllowedClickTime)
            {
                OnMouseClick?.Invoke(this);
                _nextAllowedClickTime = Time.time + Throughtime;
            }
            _pendingClickCoroutine = null;
            _lastClickTime = 0;
        }

     
        public delegate void onMouseDrop(UISceneWidget eventObj, GameObject dropObject);
        public onMouseDrop OnMouseDrop = null;
        public void OnDrop(PointerEventData eventData)
        {
            if (OnMouseDrop != null) OnMouseDrop(this, eventData.selectedObject);
        }

        public delegate void onMouseDrag(UISceneWidget eventObj, Vector2 delta);      
        public onMouseDrag OnMouseBeginDrag = null;
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (OnMouseBeginDrag != null) OnMouseBeginDrag(this, eventData.delta);
        }

        public onMouseDrag OnMouseDrag = null;
        public void OnDrag(PointerEventData eventData)
        {
            if (OnMouseDrag != null) OnMouseDrag(this, eventData.delta);
        }

        public onMouseDrag OnMouseEndDrag = null;
        public void OnEndDrag(PointerEventData eventData)
        {
            if (OnMouseEndDrag != null) OnMouseEndDrag(this, eventData.delta);
        }
    }
}
