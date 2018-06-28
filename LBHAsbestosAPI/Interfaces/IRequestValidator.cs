using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Models;

namespace LBHAsbestosAPI.Interfaces
{
    public interface IRequestValidator
    {
        ValidationResult Validate(string requestID);
    }
}