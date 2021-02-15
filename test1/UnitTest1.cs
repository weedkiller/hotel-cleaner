using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NawafizApp.Data;
using NawafizApp.Domain;

namespace test1
{
    [TestClass]
    public class UnitTest1
    {
        IUnitOfWork unitOfWork1= new UnitOfWork("DefaultConnection");
        public UnitTest1(IUnitOfWork unitOfWork)
        {
            unitOfWork1 = unitOfWork;

        }
 
            [TestMethod]
            public void TestMethod1()
            {
            


    
            }
        }
    
}
