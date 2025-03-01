using System;
using System.Collections.Generic;

class Student {
    public string Name { get; }
    public Course EnrolledCourse { get; }

    public Student(string name, Course course) {
        Name = name;
        EnrolledCourse = course;
        Console.WriteLine($"Student {Name} is enrolling in {course.CourseName}.");
        course.AddStudent(this);  // <-- Circular Dependency
    }
}

class Course {
    public string CourseName { get; }
    public List<Student> Students { get; } = new List<Student>();

    public Course(string courseName) {
        CourseName = courseName;
    }

    public void AddStudent(Student student) {
        Console.WriteLine($"Adding {student.Name} to the course {CourseName}.");
        Students.Add(student);
    }
}

class Program {
    static void Main() {
        Console.WriteLine("Creating Course...");
        Course course = new Course("CS50");

        Console.WriteLine("\nCreating Student...");
        Student student = new Student("Ahmed", course); // <- Circular Dependency occurs here
    }
}