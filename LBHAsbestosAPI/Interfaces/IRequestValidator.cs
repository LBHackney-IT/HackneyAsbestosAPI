using LBHAsbestosAPI.Entities;

namespace LBHAsbestosAPI.Interfaces
{
    public interface IRequestValidator
    {
        InspectionResponse Validate(string requestID);
    }
}