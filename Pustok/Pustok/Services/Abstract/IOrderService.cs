using Pustok.Database.DomainModels;
using System.Collections.Generic;

namespace Pustok.Services.Abstract;

public interface IOrderService
{
    string GenerateAndGetTrackingCode();
}
