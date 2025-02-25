using System;

namespace Source.DTOs.Request
{
    [Serializable]
    public class CheckpointDto
    {
        public int score;
        public float distance;
        public DateTime date;
    }
}