using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication2.Controllers
{
    public class StudentController : ApiController
    {
        public StudentController()
            {
            }
        public IHttpActionResult getStudent()
        {
            IList<Student> student = null;
            using (var ctx = new SCHOOLEntities())
            {
                student = ctx.Students.Include("StudentAdress").Select(s => new Student()
                {
                    id = s.id,
                    name = s.name
                }).ToList<Student>();
            }
            if(student.Count == 0)
            {
                return NotFound();
            }
            return Ok(student);
        }
       
        public IHttpActionResult PostNewStudent(Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new SCHOOLEntities())
            {
                ctx.Students.Add(new Student()
                {
                    id = student.id,
                    name = student.name,
                    
                });

                ctx.SaveChanges();
            }

            return Ok();
        }
        public IHttpActionResult Put(Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new SCHOOLEntities())
            {
                var existingStudent = ctx.Students.Where(s => s.id == student.id)
                                                        .FirstOrDefault<Student>();

                if (existingStudent != null)
                {
                    existingStudent.name = student.name;
                    
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new SCHOOLEntities())
            {
                var student = ctx.Students
                    .Where(s => s.id == id)
                    .FirstOrDefault();

                ctx.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }

    }
}
