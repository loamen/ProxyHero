using System;
using System.Threading;

namespace Loamen.WinControls
{
    public sealed class HandleCollector
    {
        // Events
        private static int handleTypeCount;
        private static HandleType[] handleTypes;
        private static object internalSyncObject;
        private static int suspendCount;

        static HandleCollector()
        {
            internalSyncObject = new object();
        }

        internal static event HandleChangeEventHandler HandleAdded;
        internal static event HandleChangeEventHandler HandleRemoved;

        // Methods

        public static IntPtr Add(IntPtr handle, int type)
        {
            handleTypes[type - 1].Add(handle);
            return handle;
        }

        public static int RegisterType(string typeName, int expense, int initialThreshold)
        {
            int num1;
            lock (internalSyncObject)
            {
                if ((handleTypeCount == 0) || (handleTypeCount == handleTypes.Length))
                {
                    var typeArray1 = new HandleType[handleTypeCount + 10];
                    if (handleTypes != null)
                    {
                        Array.Copy(handleTypes, 0, typeArray1, 0, handleTypeCount);
                    }
                    handleTypes = typeArray1;
                }
                handleTypes[handleTypeCount++] = new HandleType(typeName, expense, initialThreshold);
                num1 = handleTypeCount;
            }
            return num1;
        }

        public static IntPtr Remove(IntPtr handle, int type)
        {
            return handleTypes[type - 1].Remove(handle);
        }

        public static void ResumeCollect()
        {
            bool flag1 = false;
            lock (internalSyncObject)
            {
                if (suspendCount > 0)
                {
                    suspendCount--;
                }
                if (suspendCount == 0)
                {
                    for (int num1 = 0; num1 < handleTypeCount; num1++)
                    {
                        lock (handleTypes[num1])
                        {
                            if (handleTypes[num1].NeedCollection())
                            {
                                flag1 = true;
                            }
                        }
                    }
                }
            }
            if (flag1)
            {
                GC.Collect();
            }
        }

        public static void SuspendCollect()
        {
            lock (internalSyncObject)
            {
                suspendCount++;
            }
        }


        // Fields

        // Nested Types
        private class HandleType
        {
            // Methods
            private readonly int deltaPercent;
            private readonly int initialThreshHold;
            internal readonly string name;
            private int handleCount;
            private int threshHold;

            internal HandleType(string name, int expense, int initialThreshHold)
            {
                this.name = name;
                this.initialThreshHold = initialThreshHold;
                threshHold = initialThreshHold;
                deltaPercent = 100 - expense;
            }

            internal void Add(IntPtr handle)
            {
                if (handle != IntPtr.Zero)
                {
                    bool flag1 = false;
                    int num1 = 0;
                    lock (this)
                    {
                        handleCount++;
                        flag1 = NeedCollection();
                        num1 = handleCount;
                    }
                    lock (internalSyncObject)
                    {
                        if (HandleAdded != null)
                        {
                            HandleAdded(name, handle, num1);
                        }
                    }
                    if (flag1 && flag1)
                    {
                        GC.Collect();
                        int num2 = (100 - deltaPercent)/4;
                        Thread.Sleep(num2);
                    }
                }
            }

            internal int GetHandleCount()
            {
                int num1;
                lock (this)
                {
                    num1 = handleCount;
                }
                return num1;
            }

            internal bool NeedCollection()
            {
                if (suspendCount <= 0)
                {
                    if (handleCount > threshHold)
                    {
                        threshHold = handleCount + ((handleCount*deltaPercent)/100);
                        return true;
                    }
                    int num1 = (100*threshHold)/(100 + deltaPercent);
                    if ((num1 >= initialThreshHold) && (handleCount < ((int) (num1*0.9f))))
                    {
                        threshHold = num1;
                    }
                }
                return false;
            }

            internal IntPtr Remove(IntPtr handle)
            {
                if (handle != IntPtr.Zero)
                {
                    int num1 = 0;
                    lock (this)
                    {
                        handleCount--;
                        if (handleCount < 0)
                        {
                            handleCount = 0;
                        }
                        num1 = handleCount;
                    }
                    lock (internalSyncObject)
                    {
                        if (HandleRemoved != null)
                        {
                            HandleRemoved(name, handle, num1);
                        }
                    }
                }
                return handle;
            }


            // Fields
        }
    }
}