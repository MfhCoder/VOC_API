﻿namespace Core.Entities;

public class Tag : BaseEntity
{
    public string Name { get; set; }
    public ICollection<FeedbackTag> FeedbackTags { get; set; }
}
