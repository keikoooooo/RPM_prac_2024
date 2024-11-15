using System;
using System.Collections.Generic;

namespace dorm;

public partial class SarcCampus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int FloorsQuantity { get; set; }

    public virtual ICollection<Floor> Floors { get; set; } = new List<Floor>();

    public virtual ICollection<ResponsiblePerson> ResponsiblePeople { get; set; } = new List<ResponsiblePerson>();

    public virtual ICollection<SarcRoom> SarcRooms { get; set; } = new List<SarcRoom>();

    public virtual ICollection<SarcStudent> SarcStudents { get; set; } = new List<SarcStudent>();
}
