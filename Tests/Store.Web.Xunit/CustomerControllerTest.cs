using Microsoft.AspNetCore.Mvc;
using Moq;
using Store.ServiceLayer.Contracts;
using Store.ViewModel.Models;
using Store.Web.Controllers;


namespace Store.Web.Xunit
{
    public class CustomerControllerTest
    {
        [Fact]
        public void Test_Create_GET_ReturnsViewResultNullModel()
        {
            // Arrange
            ICustomerService _customerService = null;
            var controller = new CustomerController(_customerService);

            // Act
            var result = controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Test_Create_POST_InvalidModelState()
        {
            // Arrange
            var customer = new CustomerAddViewModel
            {
                Id = 4,
                Name = "last name",
                Email = "email555@email.com",


            };

            var mockRepo = new Mock<ICustomerService>();
            mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<CustomerAddViewModel>()));
            var controller = new CustomerController(mockRepo.Object);
            controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = await controller.Create(customer);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
            mockRepo.Verify();
        }

        [Fact]
        public async Task Test_Create_POST_ValidModelState()
        {
            // Arrange
            var customer = new CustomerAddViewModel
            {
                Id = 4,
                Name = "Name4",
                Email = "email555@email.com",


            };

            var mockRepo = new Mock<ICustomerService>();
            mockRepo.Setup(repo => repo.CreateAsync(It.IsAny<CustomerAddViewModel>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            var controller = new CustomerController(mockRepo.Object);

            // Act
            var result = await controller.Create(customer);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result); Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepo.Verify();
        }

        [Fact]
        public async Task Test_Index_GET_ReturnsViewResult_WithAListOfRegistrations()
        {
            // Arrange
            var mockRepo = new Mock<ICustomerService>();
            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestRegistrations());
            var controller = new CustomerController(mockRepo.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CustomerViewModel>>(viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
        }

        private static List<CustomerViewModel> GetTestRegistrations()
        {
            var registrations = new List<CustomerViewModel>();
            registrations.Add(new CustomerViewModel()
            {
                Id = 1,
                Name = "Name1",
                Email = "email556@email.com",
            });
            registrations.Add(new CustomerViewModel()
            {
                Id = 2,
                Name = "Name2",
                Email = "email557@email.com",

            });
            registrations.Add(new CustomerViewModel()
            {
                Id = 3,
                Name = "Name3",
                Email = "email558@email.com",
            });
            return registrations;
        }

        [Fact]
        public async Task Test_Update_GET_ReturnsViewResult_WithSingleRegistration()
        {
            // Arrange
            int testId = 2;
            string testName = "Name2";


            var mockRepo = new Mock<ICustomerService>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(GetTestRegisterRecord());
            var controller = new CustomerController(mockRepo.Object);

            // Act
            var result = await controller.Edit(testId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CustomerViewModel>(viewResult.ViewData.Model);
            Assert.Equal(testId, model.Id);
            Assert.Equal(testName, model.Name);
        }

        private CustomerViewModel GetTestRegisterRecord()
        {
            var result = new CustomerViewModel()
            {
                Id = 2,
                Name = "Name2",

            };
            return result;
        }

        [Fact]
        public async Task Test_Update_POST_ReturnsViewResult_InValidModelState()
        {
            // Arrange
            int testId = 2;
            var customer = GetTestRegisterRecord();

            var mockRepo = new Mock<ICustomerService>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(GetTestRegisterRecord());

            var controller = new CustomerController(mockRepo.Object);
            controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = await controller.Edit(customer);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CustomerViewModel>(viewResult.ViewData.Model);
            Assert.Equal(testId, model.Id);
        }

        [Fact]
        public async Task Test_Update_POST_ReturnsViewResult_ValidModelState()
        {
            // Arrange
            int testId = 2;
            var customer = new CustomerViewModel()
            {
                Id = 2,
                Name = "Name2",
            };
            var mockRepo = new Mock<ICustomerService>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testId)).ReturnsAsync(GetTestRegisterRecord());
            var controller = new CustomerController(mockRepo.Object);

            mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<CustomerViewModel>()))
                   .Returns(Task.CompletedTask)
                   .Verifiable();

            // Act
            var result = await controller.Edit(customer);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CustomerViewModel>(viewResult.ViewData.Model);
            //var model = Assert.IsAssignableFrom<RedirectToActionResult>(result);
            Assert.Equal(testId, model.Id);
            Assert.Equal(customer.Name, model.Name);


            mockRepo.Verify();
        }

        [Fact]
        public async Task Test_Delete_POST_ReturnsViewResult_InValidModelState()
        {
            // Arrange
            int testId = 1;

            var mockRepo = new Mock<ICustomerService>();
            mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<long>()))
                   .Returns(Task.CompletedTask)
                   .Verifiable();

            var controller = new CustomerController(mockRepo.Object);

            // Act
            var result = await controller.Delete(testId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepo.Verify();
        }
    }
}
