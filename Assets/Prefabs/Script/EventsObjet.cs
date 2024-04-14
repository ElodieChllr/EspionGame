using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsObjet : MonoBehaviour
{
    public static UnityEvent KeyUsedEvent = new UnityEvent();

    public static UnityEvent CarteAccesUsedEvent = new UnityEvent();

    public static UnityEvent AppareilPhotoUsedEvent = new UnityEvent();
}
