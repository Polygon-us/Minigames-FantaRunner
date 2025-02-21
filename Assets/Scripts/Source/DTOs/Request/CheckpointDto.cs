using System;

namespace Source.DTOs.Request
{
    [Serializable]
    public class CheckpointDto
    {
        public int score;
        public DateTime date;
    }
}