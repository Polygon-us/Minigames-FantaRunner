using System;
using UnityEngine;
using UnityREST.Editor;

namespace Source.Utils.Date
{
    [Serializable]
    public class Date
    {
        public const string ISO8601 = "yyyy-MM-ddTHH:mm:ss.fffZ";

        public TimeSpan TimeGap { get; private set; }

        public DateTime SessionStartedAt
        {
            get => _startedAt;
            set
            {
                TimeGap = value - DateTime.UtcNow;

                _startedAt = value;

#if UNITY_EDITOR

                timeGap = (float)TimeGap.TotalMilliseconds;

                startedAt = value.ToString(ISO8601);
#endif
            }
        }

#if UNITY_EDITOR

        [SerializeField, ReadOnly]
        private string startedAt;

        [SerializeField, ReadOnly]
        private float timeGap;
#endif

        private DateTime _startedAt;

        public void SetDefault()
        {
#if UNITY_EDITOR
            
            timeGap = 0f;

            startedAt = null;
#endif
            _startedAt = default;

            TimeGap = TimeSpan.Zero;
        }

        public static implicit operator Date(DateTime dateTime)
        {
            return new Date { SessionStartedAt = dateTime };
        }

        public override string ToString()
        {
            return SessionStartedAt.ToString(ISO8601);
        }
    }
}