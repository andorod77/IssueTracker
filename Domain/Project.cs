using System;

namespace Domain
{
    public class Project
    {
        public Guid Id { get; set; }
        public string ProjName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? Deadline { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }

        public int NoOfTasks { get; set; }
        public int ActiveTasks { get; set; }
    }
}