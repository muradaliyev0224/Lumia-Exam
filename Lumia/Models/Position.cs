﻿namespace Lumia.Models
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<TeamMember>? TeamMembers { get; set; }
    }
}
