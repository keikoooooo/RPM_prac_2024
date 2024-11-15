using System;
using System.Collections.Generic;

namespace dorm;

public partial class Post
{
    public int Id { get; set; }

    public string PostName { get; set; } = null!;

    public virtual ICollection<ResponsiblePerson> ResponsiblePeople { get; set; } = new List<ResponsiblePerson>();
}
