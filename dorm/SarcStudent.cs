using System;

using System.Collections.Generic;

namespace dorm;

public partial class SarcStudent
{
    private (string FullName, int NumContract, string? Specialization, int? Grade, int? EarnedPoints, string? StudentCard, SarcRoom? NumRoomNavigation, int? NumCampus) value;

    public SarcStudent((string FullName, int NumContract, string? Specialization, int? Grade, int? EarnedPoints, string? StudentCard, SarcRoom? NumRoomNavigation, int? NumCampus) value)
    {
        this.value = value;
    }

    public int Id { get; set; }

    public string StudLogin { get; set; } = null!;

    public string StudPassword { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public int NumContract { get; set; }

    public int? NumCampus { get; set; }

    public int? NumGroup { get; set; }

    public int? NumRoom { get; set; }

    public string? Specialization { get; set; }

    public string? StudentCard { get; set; }

    public int? Grade { get; set; }

    public int? EarnedPoints { get; set; }

    public virtual SarcCampus? NumCampusNavigation { get; set; }

    public virtual SarcRoom? NumRoomNavigation { get; set; }

    
}
