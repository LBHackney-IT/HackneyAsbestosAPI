using System;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Repositories;

namespace LBHAsbestosAPI.Factories
{
    public static class AsbestosRepositoryFactory
    {
        public static IPsi2000Api Build() 
        {
            return new FakePsi2000Api();
        }
    }
}
