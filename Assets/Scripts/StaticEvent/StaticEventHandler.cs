using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class StaticEventHandler
{
    public static event Action<RoomChangedEventArgs> OnRoomChanged;

    public static void CallRoomChangedEvent(Room room)
    {
        OnRoomChanged?.Invoke(new RoomChangedEventArgs() {room = room});
    }

    public static event Action<RoomEnemiesDefeateArgs> OnRoomEnemiesDefeated;

    public static void CallRoomEnemiesDefeatedEvent (Room room)
    {
        OnRoomEnemiesDefeated?.Invoke(new RoomEnemiesDefeateArgs() { room = room });
    }
}

public class RoomChangedEventArgs : EventArgs
{
    public Room room;
}


public class RoomEnemiesDefeateArgs : EventArgs
{
    public Room room;
}