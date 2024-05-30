//#define EVENTROUTER_THROWEXCEPTIONS //event router throw exceptions事件路由器引发异常
#if EVENTROUTER_THROWEXCEPTIONS
//#define EVENTROUTER_REQUIRELISTENER // 如果希望发送事件时需要侦听器，请取消注释
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct MMGameEvent
{
    public string EventName;

    private static MMGameEvent e;

    public static void Trigger(string newName)
    {
        e.EventName = newName;
        MMEventManager.TriggerEvent(e);
    }
}

///<summary>
///这个类处理事件管理，可以用来在整个游戏中广播事件，告诉一个（或多个）发生了什么。
///事件是结构化的，您可以定义任何类型的事件。此管理器附带MMGameEvents，它们是
///基本上只是由一个字符串组成，但如果你愿意，你可以使用更复杂的字符串。
/// 
///要从任何位置触发新事件，请执行YOUR_event。触发器（YOUR_PARAMETERS）
///MMGameEvent也是如此。触发器（“保存”）；例如，将触发Save MMGameEvent
/// 
///您也可以调用MMEventManager。触发事件（YOUR_EVENT）；
///例如：MMEventManager。TriggerEvent（新MMGameEvent（“GameStart”））；将向所有听众广播名为GameStart的MMGameEvent。
///
///要开始收听任何课堂上的活动，你必须做3件事：
///
///1-告诉您的类实现了该类事件的MMEventListener接口。
///例如：公共类GUIManager:Singleton＜GUIManager＞，MMEventListener＜MMGameEvent＞
///您可以有多个（每个事件类型一个）。
///
///2-启用和禁用时，分别启动和停止侦听事件：
/// void OnEnable()
/// {
/// 	this.MMEventStartListening<MMGameEvent>();
/// }
/// void OnDisable()
/// {
/// 	this.MMEventStopListening<MMGameEvent>();
/// }
/// 
///3-实现该事件的MMEventListener接口。例如
/// public void OnMMEvent(MMGameEvent gameEvent)
/// {
/// 	if (gameEvent.EventName == "GameOver")
///		{
///			// DO SOMETHING
///		}
/// } 
///将捕获从游戏中任何地方发出的所有MMGameEvent类型的事件，并在命名为GameOver时执行某些操作
///</summary>
[ExecuteAlways] //在编辑模式和游戏模式下都执行
public class MMEventManager
{
    //订阅观察模式里面的【订阅】列表
    private static Dictionary<Type, List<IMMEventListenerBase>> _subscribersList;

    static MMEventManager()
    {
        _subscribersList = new Dictionary<Type, List<IMMEventListenerBase>>();
    }

    /// <summary>
    /// 添加订阅者
    /// </summary>
    /// <param name="listener"></param>
    /// <typeparam name="MMEvent"></typeparam>
    public static void AddListener<MMEvent>(IMMEventListener<MMEvent> listener) where MMEvent : struct
    {
        Type eventType = typeof(MMEvent);
        if (!_subscribersList.ContainsKey(eventType))
        {
            _subscribersList[eventType] = new List<IMMEventListenerBase>();
        }

        if (!SubscriptionExists(eventType, listener))
        {
            _subscribersList[eventType].Add(listener);
        }
    }

    /// <summary>
    /// 移除订阅者
    /// </summary>
    /// <param name="listener"></param>
    /// <typeparam name="MMEvent"></typeparam>
    public static void RemoveListener<MMEvent>(IMMEventListener<MMEvent> listener) where MMEvent : struct
    {
        Type eventType = typeof(MMEvent);
        if (!_subscribersList.ContainsKey(eventType))
        {
#if EVENTROUTER_THROWEXCEPTIONS
            throw new ArgumentException(string.Format(
                "正在删除侦听器 \"{0}\", 但是事件类型 \"{1}\" 未注册", listener,
                eventType.ToString()));
#else
            return;
#endif
        }

        List<IMMEventListenerBase> subscriberList = _subscribersList[eventType];
#if EVENTROUTER_THROWEXCEPTIONS
        bool listenerFound = false;
#endif
        for (int i = 0; i < subscriberList.Count; i++)
        {
            if (subscriberList[i] == listener)
            {
                subscriberList.Remove(subscriberList[i]);
#if EVENTROUTER_THROWEXCEPTIONS
                listenerFound = true;
#endif
                if (subscriberList.Count == 0)
                {
                    _subscribersList.Remove(eventType);
                }
                return;
            }
        }
#if EVENTROUTER_THROWEXCEPTIONS
        if (!listenerFound)
        {
            throw new ArgumentException(string.Format("正在删除侦听器，但提供的接收器未订阅事件类型 \"{0}\"", eventType.ToString()));
        }
#endif
    }

    public static void TriggerEvent<MMEvent>(MMEvent newEvent) where MMEvent : struct
    {
        List<IMMEventListenerBase> list;
        if (!_subscribersList.TryGetValue(typeof(MMEvent), out list))
        {
#if EVENTROUTER_REQUIRELISTENER
            throw new ArgumentException(string.Format(
                "正在尝试发送类型为 \"{0}\" 的事件, 但找不到此类型的侦听器。确保 this.Subscribe<{0}>(EventRouter) 已被调用, 或者尚未取消订阅此事件的所有侦听器",
                typeof(MMEvent).ToString()));
#else
			        return;
#endif
        }
        for (int i = 0; i < list.Count; i++)
        {
            (list[i] as IMMEventListener<MMEvent>).OnMMEvent(newEvent);
        }
    }

    /// <summary>
    /// 检查是否有特定类型事件的订阅者
    /// </summary>
    /// <param name="type"></param>
    /// <param name="receiver"></param>
    /// <returns></returns>
    private static bool SubscriptionExists(Type type, IMMEventListenerBase receiver)
    {
        List<IMMEventListenerBase> receivers;
        if (!_subscribersList.TryGetValue(type, out receivers))
        {
            return false;
        }

        bool exists = false;
        for (int i = 0; i < receivers.Count; i++)
        {
            if (receivers[i] == receiver)
            {
                exists = true;
                break;
            }
        }

        return exists;
    }
}

public static class EventRegister
{
    public static void MMEventStartListening<EventType>(this IMMEventListener<EventType> caller)
        where EventType : struct
    {
        MMEventManager.AddListener<EventType>(caller);
    }

    public static void MMEventStopListening<EventType>(this IMMEventListener<EventType> caller)
        where EventType : struct
    {
        MMEventManager.RemoveListener<EventType>(caller);
    }
}

public interface IMMEventListenerBase
{
};

public interface IMMEventListener<T> : IMMEventListenerBase
{
    void OnMMEvent(T eventType);
}
