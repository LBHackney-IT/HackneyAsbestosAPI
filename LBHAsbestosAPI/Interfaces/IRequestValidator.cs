using LBHAsbestosAPI.Entities;
using LBHAsbestosAPI.Models;

namespace LBHAsbestosAPI.Interfaces
{
    public interface IRequestValidator
    {
       bool Validate(string requestID);
    }
}