using System;

namespace BME.Rendering.Animation
{
    [Flags]
    public enum AnimationType {
        Linear,
        Reverse,
        PingPong,

        LoopFlag = 0b10000,
    }
}
