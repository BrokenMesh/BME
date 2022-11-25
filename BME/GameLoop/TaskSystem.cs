using System;
using System.Collections.Generic;
using BME.Rendering.Animation;

namespace BME.GameLoop
{
    public static class TaskSystem
    {
        // TODO: pass end time to
        public delegate void Task(float _t);
        static List<Task> tasks = new List<Task>();
        static List<float> currentTimes = new List<float>();
        static List<float> endTimes = new List<float>();
        static List<AnimationType> animationTypes = new List<AnimationType>();

        public static void Update() {
            List<int> _endedTasks = new List<int>();

            for (int i = 0; i < tasks.Count; i++) {

                currentTimes[i] += GameTime.deltaTime;
                float _funcTime = 0.0f;

                switch (animationTypes[i] & ~AnimationType.LoopFlag) {
                    case AnimationType.Linear:
                        _funcTime = currentTimes[i]; break;

                    case AnimationType.Reverse:
                        _funcTime = endTimes[i] - currentTimes[i]; break;

                    case AnimationType.PingPong:
                        if (currentTimes[i] < endTimes[i] / 2) {
                            _funcTime = currentTimes[i]*2;
                        } else {  
                            _funcTime = endTimes[i] - (currentTimes[i] - (endTimes[i]/2))*2;
                        }
                        break;

                    default:
                        break;
                }

                tasks[i](_funcTime);

                if (currentTimes[i] > endTimes[i]) {
                    if ((animationTypes[i] & AnimationType.LoopFlag) == AnimationType.LoopFlag) {
                        currentTimes[i] = 0;
                    } else {
                        _endedTasks.Add(i);
                    }
                }
            }

            foreach (int _index in _endedTasks) {
                tasks.RemoveAt(_index);
                currentTimes.RemoveAt(_index);  
                endTimes.RemoveAt(_index);
            }
        }


        public static void AddTimedTask(Task _task, float _length, AnimationType _type) {
            tasks.Add(_task);
            endTimes.Add(_length);
            currentTimes.Add(0.0f);
            animationTypes.Add(_type);

            //return tasks.Count - 1; // TODO id System
        }

    }
}
