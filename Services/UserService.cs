using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using CondorHacks.Entities;
using CondorHacks.Helpers;

namespace CondorHacks.Services
{
    public interface IUserService
    {
        Admin AuthenticateAdmin(string username, string password, toDoListConestogaContext db);
        Users AuthenticateUser(string username, string password, toDoListConestogaContext db);

        bool checkEmailExistence(string email, toDoListConestogaContext db);
        bool checkCodeExistence(string code, toDoListConestogaContext db);
        IEnumerable<Levels> GetAllLevels(toDoListConestogaContext db);
        Users FindUser(string email, toDoListConestogaContext db);
        IEnumerable<ProgramName> GetAllProgramms(toDoListConestogaContext db);
        IEnumerable<Subjects> GetAllSubjectsUser(Users user, toDoListConestogaContext db);
        IEnumerable<Length> GetAllLength(toDoListConestogaContext db);
        IEnumerable<Difficulty> GetAllDifficulties(toDoListConestogaContext db);

        IEnumerable<Assignment> GetAllAssignmentsUser(Users user, toDoListConestogaContext db);
        void DeleteUserAssignment(Users user, toDoListConestogaContext db, int idAssgn);
    }
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public Admin AuthenticateAdmin(string username, string password, toDoListConestogaContext db)
        {
            var user = db.Admin.SingleOrDefault(x => x.Email == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public Users AuthenticateUser(string username, string password, toDoListConestogaContext db)
        {
            var user = db.Users.SingleOrDefault(x => x.Email == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, "User")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public IEnumerable<Levels> GetAllLevels(toDoListConestogaContext db)
        {
            var levels = db.Levels.ToList();
            return levels;
        }
        public IEnumerable<Length> GetAllLength(toDoListConestogaContext db)
        {
            var length = db.Length.ToList();
            return length;
        }
        public IEnumerable<Difficulty> GetAllDifficulties(toDoListConestogaContext db)
        {
            var difficulties = db.Difficulty.ToList();
            return difficulties;
        }

        public IEnumerable<ProgramName> GetAllProgramms(toDoListConestogaContext db)
        {
            var programms = db.ProgramName.ToList();
            return programms;
        }
        public bool checkEmailExistence(string email, toDoListConestogaContext db)
        {
            bool checkEmail = false;
            if (db.Admin.Where(acccount => acccount.Email == email).FirstOrDefault() != null)
            {
                checkEmail = true;
            }
            else if (db.Users.Where(acccount => acccount.Email == email).FirstOrDefault() != null)
            {
                checkEmail = true;
            }
            return checkEmail;
        }
        public bool checkCodeExistence(string code, toDoListConestogaContext db)
        {
            bool checkCode = false;
            if (db.AccessCode.Where(acccount => acccount.AccessCode1 == code).FirstOrDefault() != null)
            {
                checkCode = true;
                AccessCode codeTemp = db.AccessCode.Where(acccount => acccount.AccessCode1 == code).FirstOrDefault();
                codeTemp.NumberOfUsers = codeTemp.NumberOfUsers - 1;
            }
            return checkCode;
        }
        public Users FindUser(string email, toDoListConestogaContext db)
        {
            Users user = db.Users.Where(classEntity => classEntity.Email == email).FirstOrDefault();
            return user;
        }
        public IEnumerable<Subjects> GetAllSubjectsUser(Users user, toDoListConestogaContext db)
        {
            var subject = db.SubjectProgram.Where(subjectProgramm => subjectProgramm.IdProgram == user.ProgramId).ToList();
            var result = db.Subjects.ToList().Where(subjectEntity => subject.Select(subjectProgEntity => subjectProgEntity.IdSubject).Contains(subjectEntity.IdSubject));
            return result;
        }

        public IEnumerable<Assignment> GetAllAssignmentsUser(Users user, toDoListConestogaContext db)
        {
            var assignUser = db.UserAssignment.Where(assignmentUser => assignmentUser.IdUser == user.IdUser).ToList();
            var result = db.Assignment.ToList().Where(assignmentEntity => assignUser.Select(assignUserEntity => assignUserEntity.IdAssignment).Contains(assignmentEntity.IdAssignment));
            return result;
        }
        public void DeleteUserAssignment(Users user, toDoListConestogaContext db, int idAssgn)
        {
            UserAssignment tempRelationship = db.UserAssignment.Where(uEntity => uEntity.IdAssignment == idAssgn && uEntity.IdUser == user.IdUser).SingleOrDefault();
            Assignment tempAssign = db.Assignment.Where(aEntity => aEntity.IdAssignment == idAssgn).SingleOrDefault();
            db.Assignment.Remove(tempAssign);
            db.SaveChanges();
            db.UserAssignment.Remove(tempRelationship);
            db.SaveChanges();
        }
    }
}
