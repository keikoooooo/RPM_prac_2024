using System;
using System.Collections.Generic;

namespace dorm;

public partial class SarcRoom
{
    public int Id { get; set; }

    public int NumRoom { get; set; }

    public int Capacity { get; set; }

    public int IdFloor { get; set; }

    public int IdCampus { get; set; }

    public virtual SarcCampus IdCampusNavigation { get; set; } = null!;

    public virtual Floor IdFloorNavigation { get; set; } = null!;

    public virtual ICollection<SarcStudent> SarcStudents { get; set; } = new List<SarcStudent>();
}
