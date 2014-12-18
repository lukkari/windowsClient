using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.UniversalApp.Services.Interfaces
{
    public interface IApplicationStateService
    {
        bool IsLoading { get; set; }
        bool IsWeekNavigationEnabled { get; set; }
        bool IsFailure { get; set; }
        bool IsSuccess { get; set; }
    }
}
