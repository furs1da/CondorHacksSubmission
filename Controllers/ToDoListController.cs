using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using CondorHacks.Entities;
using CondorHacks.Services;
using CondorHacks.Models;
using Microsoft.AspNetCore.Routing;

namespace Tasks.Controllers
{
    [AllowAnonymous]
    [Route("todolist")]
    public class ToDoListController : ControllerBase
    {
        private IUserService _userService;
        private toDoListConestogaContext db = new toDoListConestogaContext();
        private readonly ILogger<ToDoListController> _logger;
        public ToDoListController(ILogger<ToDoListController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticateModel model)
        {
            var admin = _userService.AuthenticateUser(model.Username, model.Password, db);
            if (admin != null)
                return Ok(admin);
            var user = _userService.AuthenticateAdmin(model.Username, model.Password, db);
            if (user != null)
                return Ok(user);
            else
                return BadRequest(new { message = "You have entered an incorrect email or password" });
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegistrationModel model)
        {
            if (_userService.checkEmailExistence(model.EmailUser, db))
            {
                return BadRequest(new { message = "This email is already registered!" });
            }
            else if (!_userService.checkCodeExistence(model.AccessCode, db))
            {
                return BadRequest(new { message = "Access code is invalid!" });
            }

            Users showPiece = db.Users
                      .OrderByDescending(p => p.IdUser)
                      .FirstOrDefault();

            Users tempUser = new Users();
            if (showPiece == null)
            {
                tempUser.IdUser = db.Users.Count() + 1;
            }
            else
            {
                tempUser.IdUser = (showPiece.IdUser) + 1;
            }
            tempUser.FullName = model.FullName;
            tempUser.Email = model.EmailUser;
            tempUser.DateOfBirth = Convert.ToDateTime(model.DateOfBirth);
            tempUser.DateOfRegistration = DateTime.Today;
            tempUser.ProgramId = model.ProgName;
            tempUser.LevelProg = model.Level;
            tempUser.IdRole = 2;
            tempUser.Password = model.PasswordUser;
           

            db.Users.Add(tempUser);
            db.SaveChanges();

          

            var user = _userService.AuthenticateUser(tempUser.Email, tempUser.Password, db);
            if (user != null)
            {
                return Ok(user);
            }
            else
                return BadRequest(new { message = "Server eror" });
        }


        [Authorize]
        [HttpPost("CreateAssignment")]
        public IActionResult CreateAssignment([FromForm] CreateAssignment model)
        {
            try
            {
                Assignment showPiece = db.Assignment
                    .OrderByDescending(p => p.IdAssignment)
                    .FirstOrDefault();

                string emailCurrentUser = User.FindFirst(ClaimTypes.Name).Value;
                Users tempUser = _userService.FindUser(emailCurrentUser, db);

                Assignment tempAssg = new Assignment();
                if (showPiece == null)
                {
                    tempAssg.IdAssignment = db.Assignment.Count() + 1;
                }
                else
                {
                    tempAssg.IdAssignment = (showPiece.IdAssignment) + 1;
                }
                tempAssg.Name = model.NameAsg;
                tempAssg.Description = model.Description;
                tempAssg.PercentOfFinalGrade = model.Percentage;
                tempAssg.Deadline = Convert.ToDateTime(model.Deadline);
                tempAssg.IdSubject = model.SubjectName;
                tempAssg.Length = model.LengthDur;
                tempAssg.Difficulty = model.DifficultyLevel;
                tempAssg.Done = false;
                db.Assignment.Add(tempAssg);
                db.SaveChanges();


                UserAssignment showPieceTemp = db.UserAssignment
                   .OrderByDescending(p => p.IdUserAssignment)
                   .FirstOrDefault();

                UserAssignment tempRel = new UserAssignment();
                if (showPieceTemp == null)
                {
                    tempRel.IdUserAssignment = db.UserAssignment.Count() + 1;
                }
                else
                {
                    tempRel.IdUserAssignment = (showPieceTemp.IdUserAssignment) + 1;
                }
                tempRel.IdAssignment = tempAssg.IdAssignment;
                tempRel.IdUser = tempUser.IdUser;
                db.UserAssignment.Add(tempRel);
                db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [AllowAnonymous]
        [HttpGet("levels")]
        public IActionResult GetLevels()
        {
            var levels = _userService.GetAllLevels(db);

            List<SelectModel> levelsSelect = new List<SelectModel>();

            if (levels != null)
            {
                foreach (Levels level in levels)
                {
                    levelsSelect.Add(new SelectModel(level.IdLevel.ToString(), level.LevelValue.ToString()));
                }
                return Ok(levelsSelect);
            }
            else
                return BadRequest(new { message = "Server error" });
        }
    
        [AllowAnonymous]
        [HttpGet("programms")]
        public IActionResult GetProgramms()
        {
            var programms = _userService.GetAllProgramms(db);

            List<SelectModelSearch> programmsSelect = new List<SelectModelSearch>();

            if (programms != null)
            {
                foreach (ProgramName programm in programms)
                {
                    programmsSelect.Add(new SelectModelSearch(programm.IdProgram.ToString(), programm.ProgramName1));
                }
                return Ok(programmsSelect);
            }
            else
                return BadRequest(new { message = "Server error" });
        }
        [Authorize]
        [HttpGet("subjects")]
        public IActionResult GetSubjects()
        {
          
            string emailCurrentUser = User.FindFirst(ClaimTypes.Name).Value;
            Users tempUser = _userService.FindUser(emailCurrentUser, db);

            var subjects = _userService.GetAllSubjectsUser(tempUser, db);
            List<SelectModelSearch> subjectSelect = new List<SelectModelSearch>();

            if (subjects != null)
            {
                foreach (Subjects subject in subjects)
                {
                    subjectSelect.Add(new SelectModelSearch(subject.IdSubject.ToString(), subject.SubjectName));
                }
                return Ok(subjectSelect);
            }
            else
                return BadRequest(new { message = "Server error" });
        }
        [Authorize]
        [HttpGet("assignments")]
        public IActionResult GetAssignments()
        {

            string emailCurrentUser = User.FindFirst(ClaimTypes.Name).Value;
            Users tempUser = _userService.FindUser(emailCurrentUser, db);

            var assignments = _userService.GetAllAssignmentsUser(tempUser, db).ToList();
           
            if (assignments != null)
            {          
                return Ok(assignments);
            }
            else
                return BadRequest(new { message = "No items." });
        }

        [AllowAnonymous]
        [HttpGet("difficulties")]
        public IActionResult GetDifficulties()
        {
            var diffs = _userService.GetAllDifficulties(db);

            List<SelectModelSearch> diffSelect = new List<SelectModelSearch>();

            if (diffs != null)
            {
                foreach (Difficulty diff in diffs)
                {
                    diffSelect.Add(new SelectModelSearch(diff.IdLevelOfDifficulty.ToString(), diff.Value.ToString()));
                }
                return Ok(diffSelect);
            }
            else
                return BadRequest(new { message = "Server error" });
        }
        [AllowAnonymous]
        [HttpGet("length")]
        public IActionResult GetLength()
        {
            var length = _userService.GetAllLength(db);

            List<SelectModelSearch> lengthSelect = new List<SelectModelSearch>();

            if (length != null)
            {
                foreach (Length lng in length)
                {
                    lengthSelect.Add(new SelectModelSearch(lng.IdLength.ToString(), lng.Length1));
                }
                return Ok(lengthSelect);
            }
            else
                return BadRequest(new { message = "Server error" });
        }

        [Authorize]
        [HttpDelete("DeleteAssignment/{id}")]
        public IActionResult DeleteAssignmentUser(int id)
        {
            try
            {
                string emailCurrentUser = User.FindFirst(ClaimTypes.Name).Value;
                Users userTemp = _userService.FindUser(emailCurrentUser, db);
                _userService.DeleteUserAssignment(userTemp, db, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
