using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIEvents
{
    public static Action OpenStartView;
    public static Action CloseStartView;
    public static Action OpenGameplayView;
    public static Action OpenEndView;
    public static Action CloseEndView;

    public static Action<int> SetEndGameScore;

}
