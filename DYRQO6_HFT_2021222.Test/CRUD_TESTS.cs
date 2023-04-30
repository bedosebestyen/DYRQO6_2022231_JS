using DYRQO6_HFT_2022231.Logic;
using DYRQO6_HFT_2022231.Models;
using DYRQO6_HFT_2022231.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DYRQO6_HFT_2021222.Test
{
    public class CRUD_TESTS
    {
        CarsLogic cl;
        Mock<IRepository<Cars>> mockCarsRepository;
        CustomerLogic crl;
        Mock<IRepository<Customer>> mockCustomerRepository;
        CarShopLogic csl;
        Mock<IRepository<CarShop>> mockCarShopRepository;

        Mock<IRepository<Manager>> mockManagerRepository;
        [SetUp]
        public void Init()
        {
            var inputdata = new List<Cars>()
            {
                new Cars("1#Audi#white#1#1#2019*04*15#10000000"),
                new Cars("2#Skoda#king blue#2#2#2009*04*15#5000000"),
                new Cars("3#Volkswagen#black#3#3#2022*01*30#6000000"),
                new Cars("4#Fiat#red#4#3#2000*09*19#3000000"),
                new Cars("5#BMW#black#4#1#2020*02*22#11000000"),
                new Cars("6#Peugeot#white#1#1#2014*06*08#4000000"),
            }.AsQueryable();

            mockCarsRepository = new Mock<IRepository<Cars>>();
            mockCustomerRepository = new Mock<IRepository<Customer>>();
            mockCarShopRepository = new Mock<IRepository<CarShop>>();
            mockManagerRepository = new Mock<IRepository<Manager>>();
            mockCarsRepository.Setup(x => x.ReadAll()).Returns(inputdata);
            cl = new CarsLogic(mockCarsRepository.Object, mockCustomerRepository.Object, mockCarShopRepository.Object);
            crl = new CustomerLogic(mockCustomerRepository.Object);
            csl = new CarShopLogic(mockCarShopRepository.Object, mockManagerRepository.Object);
        }
        [Test]
        public void CarsReadAllTest()
        {
            //ACT
            cl.ReadAll();
            //ASSERT
            mockCarsRepository.Verify(
                x => x.ReadAll(),
                Times.Once);
        }
        [Test]
        public void CustomersReadAllTest()
        {
            //ACT
            crl.ReadAll();
            //ASSERT
            mockCustomerRepository.Verify(
                x => x.ReadAll(),
                Times.Once);
        }
        [Test]
        public void CarsCreateTest()
        {
            var sample = new Cars("1#Audi#white#1#1#2019*04*15#10000000");
            //ACT
            cl.Create(sample);
            //ASSERT
            mockCarsRepository.Verify(
                x => x.Create(sample),
                Times.Once);
        }
        [Test]
        public void CustomersCreateTest()
        {
            var sample = new Customer() { Name = "János" , Address = "a1", Age = 33};
            //ACT
            crl.Create(sample);
            //ASSERT
            mockCustomerRepository.Verify(
                x => x.Create(sample),
                Times.Once);
        }
        [Test]
        public void CarShopCreateTest()
        {
            var sample = new CarShop() { Name = "BestCarsss", Address = "a1"};
            //ACT
            csl.Create(sample);
            //ASSERT
            mockCarShopRepository.Verify(
                x => x.Create(sample),
                Times.Once);
        }

    }
    
}
