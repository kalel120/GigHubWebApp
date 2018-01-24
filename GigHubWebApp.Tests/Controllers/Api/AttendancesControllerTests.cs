using FluentAssertions;
using GigHubWebApp.Controllers.Api;
using GigHubWebApp.Core;
using GigHubWebApp.Core.DTOs;
using GigHubWebApp.Core.Models;
using GigHubWebApp.Core.Repositories;
using GigHubWebApp.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace GigHubWebApp.Tests.Controllers.Api {
    [TestClass]
    public class AttendancesControllerTests {
        private AttendancesController _attendancesController;
        private Mock<IAttendancesRepository> _mockRepo;
        private string _userId;
        private int _gigId;
        private AttendanceDto _attendanceDto;

        [TestInitialize]
        public void TestInitialize() {
            _mockRepo = new Mock<IAttendancesRepository>();

            // Setting up mock unitOfWork.
            // When mockUoW calls AttendancesRepo -> it returns mock IAttendancesRepository
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.AttendancesRepo).Returns(_mockRepo.Object);

            // Here, attendanceController object is calling mockUnitOfWork -> mockUnitOfWork calling mock AttendanceRepository
            _attendancesController = new AttendancesController(mockUoW.Object);

            _userId = "1"; _gigId = 1;
            // Invoking extension method
            _attendancesController.MockThisUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Attend_CheckDuplicateAttendanceExists_ShouldReturnBadRequest() {
            var testAttendance = new Attendance();
            // Setting up the repository
            _mockRepo.Setup(a => a.GetAttendanceByGigId(_gigId, _userId)).Returns(testAttendance);
            // Trying to force attend the user through api
            _attendanceDto = new AttendanceDto { GigId = _gigId };
            var result = _attendancesController.Attend(_attendanceDto);

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Attend_ValidAttendaceRequest_ShouldReturnOk() {
            _attendanceDto = new AttendanceDto { GigId = _gigId };
            var result = _attendancesController.Attend(_attendanceDto);

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void Absent_CheckAttendanceExists_ShouldReturnNull() {
            // We will not set attendance repository here. That means mock user will not attend to the gig.
            // But we will try to invoke absent method for testing.
            var result = _attendancesController.Absent(_gigId);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Absent_ValidRequest_ShouldReturnIdOfDeletedAttendance() {
            var testAttendance = new Attendance();

            _mockRepo.Setup(a => a.GetAttendanceByGigId(_gigId, _userId)).Returns(testAttendance);
            var result = (OkNegotiatedContentResult<int>)_attendancesController.Absent(_gigId);

            result.Content.Should().Be(_gigId);
        }
    }
}