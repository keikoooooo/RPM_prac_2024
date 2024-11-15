using System;
using System.Collections.Generic;

namespace dorm;

public partial class ResponsiblePerson
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public int IdPost { get; set; }

    public int IdCampus { get; set; }

    public virtual SarcCampus IdCampusNavigation { get; set; } = null!;

    public virtual Post IdPostNavigation { get; set; } = null!;
}
