using System;

namespace Source.DTOs.Request
{
    [Serializable]
    public class CheckpointDto
    {
        public int score;
        public int distance;
        public string date;
    }
}