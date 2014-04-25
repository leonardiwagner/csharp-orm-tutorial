﻿using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

using EntityFrameworkCodeFirst.Core.Model;
using EntityFrameworkCodeFirst.Core.Infra;

namespace EntityFrameworkCodeFirst.Test
{
    [TestClass]
    public class CourseTest
    {
        private DatabaseConnection database = new DatabaseConnection();

        [TestInitialize]
        public void Initialize()
        {
            Repository<Course> repository = new Repository<Course>(database);
            var courseList = repository.GetAll();

            foreach (var course in courseList.ToList())
                repository.Delete(course);
        }

        [TestCleanup]
        public void Dispose()
        {
            this.database.Dispose();
        }

        [TestMethod]
        public void CourseCreate()
        {
            Repository<Course> repository = new Repository<Course>(database);
            var course = new Course("Mathematics");
            repository.Save(course);

            Assert.AreNotEqual(0, course.Id);
        }

        [TestMethod]
        public void CourseEdit()
        {
            Repository<Course> repository = new Repository<Course>(database);
            var course = new Course("Mathematics");
            repository.Save(course);

            var courseEdit = repository.GetWhere(c => c.Id == course.Id).First();
            courseEdit.Name = "Physics";
            repository.Save(courseEdit);

            var checkEditedCourse = repository.GetWhere(c => c.Id == courseEdit.Id).First();
            checkEditedCourse.Should().NotBe(course.Name);
        }

        [TestMethod]
        public void CourseAddStudent()
        {
            Repository<Course> repository = new Repository<Course>(database);
            Repository<Student> repository2 = new Repository<Student>(database);

            var student = new Student("Newbie student");
            repository2.Save(student);

            var course = new Course("French");
            course.Students.Add(student);

            repository.Save(course);
        }

        [TestMethod, Ignore]
        public void CourseDeleteWithStudent()
        {
            Repository<Course> courseRepository = new Repository<Course>(database);
            Repository<Student> studentRepository = new Repository<Student>(database);

            var student = new Student("Newbie student");
            studentRepository.Save(student);

            var course = new Course("French");
            course.Students.Add(student);

            courseRepository.Delete(course);

            var checkStudentStillExists = studentRepository.GetWhere(s => s.Id == student.Id).First();

            checkStudentStillExists.Name.Should().Be(student.Name);
        }
    }
}
