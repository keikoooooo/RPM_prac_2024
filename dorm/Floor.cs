using System;
using System.Collections.Generic;

namespace dorm;

public partial class Floor
{
    public int Id { get; set; }

    public int FloorNumber { get; set; }

    public int IdCampus { get; set; }

    public virtual SarcCampus IdCampusNavigation { get; set; } = null!;

    public virtual ICollection<SarcRoom> SarcRooms { get; set; } = new List<SarcRoom>();
}
